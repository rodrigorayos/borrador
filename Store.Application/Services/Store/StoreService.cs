using FluentValidation;
using Store.Domain.Dtos.Store;
using Store.Domain.Models.Store;
using Store.Domain.Repositories.Store;
using Store.Domain.Responses;
using System.Net;

namespace Store.Application.Services.Store
{
    public class StoreService
    {
        private readonly IValidator<StoreModel> _validator;
        private readonly IStoreRepository<StoreDto> _storeRepository;
        
        public StoreService(
            IValidator<StoreModel> validator,
            IStoreRepository<StoreDto> storeRepository)
        {
            _validator = validator;
            _storeRepository = storeRepository;
        }
        
        public async Task<Result<List<StoreDto>>> CreateStore(StoreQueryDto createDto)
        {
            var storeModel = new StoreModel(createDto.Name, createDto.Ubication, createDto.Capacity);

            var validationResult = await _validator.ValidateAsync(storeModel);
            if (!validationResult.IsValid)
            {
                return Result<List<StoreDto>>.Failure(
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    HttpStatusCode.BadRequest
                );
            }

            var storeDto = new StoreDto(0, createDto.Name, createDto.Ubication, createDto.Capacity);
            var createdStore = await _storeRepository.CreateAsync(storeDto);

            return Result<List<StoreDto>>.Success(
                new List<StoreDto> { createdStore }, // Devuelve la lista
                HttpStatusCode.Created,
                "Almacén creado con éxito."
            );
        }
        
        public async Task<Result<List<StoreDto>>> GetAllAsync()
        {
            var stores = await _storeRepository.GetAllAsync();

            return Result<List<StoreDto>>.Success(
                stores ?? new List<StoreDto>(), // 👈 Si es null, se devuelve []
                HttpStatusCode.OK,
                stores != null && stores.Any() ? "Almacenes obtenidos con éxito." : "No se encontraron almacenes."
            );
        }
        
        public async Task<Result<List<StoreDto>>> GetByIdAsync(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            if (store is null)
            {
                return Result<List<StoreDto>>.Failure(
                    new List<string> { "No se encontró el almacén con el ID especificado." },
                    HttpStatusCode.NotFound
                );
            }

            return Result<List<StoreDto>>.Success(
                new List<StoreDto> { store }, 
                HttpStatusCode.OK,
                "Almacén obtenido con éxito."
            );
        }
        
        public async Task<Result<List<StoreDto>>> GetByNameAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
            {
                return Result<List<StoreDto>>.Failure(
                    new List<string> { "El término de búsqueda debe tener al menos 3 caracteres." },
                    HttpStatusCode.BadRequest
                );
            }

            var stores = await _storeRepository.SearchByNameAsync(searchTerm);

            return Result<List<StoreDto>>.Success(
                stores?.ToList() ?? new List<StoreDto>(), // 👈 Si es null, se devuelve []
                HttpStatusCode.OK,
                stores != null && stores.Any() ? $"Se encontraron {stores.Count()} almacenes." : "No se encontraron almacenes con el nombre especificado."
            );
        }

        public async Task<Result<List<StoreDto>>> UpdateAsync(int id, StoreQueryDto updateDto)
        {
            var storeModel = new StoreModel(updateDto.Name, updateDto.Ubication, updateDto.Capacity)
            {
                Id = id
            };

            var validationResult = await _validator.ValidateAsync(storeModel);
            if (!validationResult.IsValid)
            {
                return Result<List<StoreDto>>.Failure(
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    HttpStatusCode.BadRequest
                );
            }

            var storeDto = new StoreDto(id, updateDto.Name, updateDto.Ubication, updateDto.Capacity);
            var updatedStore = await _storeRepository.UpdateAsync(storeDto);

            return Result<List<StoreDto>>.Success(
                new List<StoreDto> { updatedStore }, // Devuelve una lista
                HttpStatusCode.OK,
                "Almacén actualizado con éxito."
            );
        }
        
        public async Task<Result<List<bool>>> DeleteAsync(int id)
        {
            var result = await _storeRepository.DeleteAsync(id);

            return result
                ? Result<List<bool>>.Success(new List<bool> { true }, HttpStatusCode.OK, "Almacén eliminado con éxito.")
                : Result<List<bool>>.Failure(new List<string> { "No se encontró el almacén especificado." }, HttpStatusCode.NotFound);
        }
    }
}