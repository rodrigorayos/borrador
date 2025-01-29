using FluentValidation;
using Store.Domain.Dtos;
using Store.Domain.Exceptions.Store;
using Store.Domain.Repositories.Store;

namespace Store.Application.Services.Store;

public class StoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly IValidator<StoreDto> _storeValidator;

    public StoreService(IStoreRepository storeRepository, IValidator<StoreDto> storeValidator)
    {
        _storeRepository = storeRepository;
        _storeValidator = storeValidator;
    }

    public async Task<IEnumerable<StoreDto>> GetAllStoresAsync()
    {
        return await _storeRepository.GetAllAsync<StoreDto>();
    }

    public async Task<StoreDto> GetStoreByIdAsync(int id)
    {
        var store = await _storeRepository.GetByIdAsync<StoreDto>(id);
        if (store == null)
        {
            throw new StoreNotFoundException(id);
        }
        return store;
    }

    public async Task<StoreDto> AddStoreAsync(StoreDto storeDto)
    {
        // Validar el DTO antes de continuar
        var validationResult = await _storeValidator.ValidateAsync(storeDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Verificar si el nombre de la tienda ya existe
        var isNameUnique = await _storeRepository.IsStoreNameUniqueAsync(storeDto.Name);
        if (!isNameUnique)
        {
            throw new StoreAlreadyExistsException(storeDto.Name);
        }

        return await _storeRepository.AddAsync(storeDto);
    }

    public async Task<StoreDto> UpdateStoreAsync(StoreDto storeDto)
    {
        // Validar el DTO antes de continuar
        var validationResult = await _storeValidator.ValidateAsync(storeDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingStore = await _storeRepository.GetByIdAsync<StoreDto>(storeDto.Id);
        if (existingStore == null)
        {
            throw new StoreNotFoundException(storeDto.Id);
        }

        // Verificar si el nombre de la tienda ya existe (excepto para la tienda actual)
        var isNameUnique = await _storeRepository.IsStoreNameUniqueAsync(storeDto.Name);
        if (!isNameUnique && existingStore.Name != storeDto.Name)
        {
            throw new StoreAlreadyExistsException(storeDto.Name);
        }

        return await _storeRepository.UpdateAsync(storeDto);
    }

    public async Task<StoreDto> DeleteStoreAsync(int id)
    {
        var store = await _storeRepository.GetByIdAsync<StoreDto>(id);
        if (store == null)
        {
            throw new StoreNotFoundException(id);
        }

        return await _storeRepository.DeleteAsync<StoreDto>(id);
    }
}