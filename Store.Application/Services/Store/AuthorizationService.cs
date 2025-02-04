using FluentValidation;
using Store.Domain.Dtos.Authorization;
using Store.Domain.Models.Authorization;
using Store.Domain.Repositories.Authorization;
using Store.Domain.Responses;
using System.Net;

namespace Store.Application.Services.Authorization
{
    public class AuthorizationService
    {
        private readonly IValidator<AuthorizationModel> _validator;
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthorizationService(
            IValidator<AuthorizationModel> validator,
            IAuthorizationRepository authorizationRepository)
        {
            _validator = validator;
            _authorizationRepository = authorizationRepository;
        }
        
        public async Task<Result<List<AuthorizationDto>>> CreateAuthorization(AuthorizationQueryDto createDto)
        {
            var authorizationModel = new AuthorizationModel(createDto.Date, createDto.State, createDto.Description);

            var validationResult = await _validator.ValidateAsync(authorizationModel);
            if (!validationResult.IsValid)
            {
                return Result<List<AuthorizationDto>>.Failure(
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    HttpStatusCode.BadRequest
                );
            }

            var authorizationDto = new AuthorizationDto(0, createDto.Date, createDto.State, createDto.Description);
            var createdAuthorization = await _authorizationRepository.CreateAsync(authorizationDto);

            return Result<List<AuthorizationDto>>.Success(
                new List<AuthorizationDto> { createdAuthorization },
                HttpStatusCode.Created,
                "Autorización creada con éxito."
            );
        }
        
        public async Task<Result<List<AuthorizationDto>>> GetAllAsync()
        {
            var authorizations = await _authorizationRepository.GetAllAsync();

            return Result<List<AuthorizationDto>>.Success(
                authorizations ?? new List<AuthorizationDto>(),
                HttpStatusCode.OK,
                authorizations != null && authorizations.Any() ? "Autorizaciones obtenidas con éxito." : 
                    "No se encontraron autorizaciones."
            );
        }
        
        public async Task<Result<List<AuthorizationDto>>> GetByIdAsync(int id)
        {
            var authorization = await _authorizationRepository.GetByIdAsync(id);

            if (authorization is null)
            {
                return Result<List<AuthorizationDto>>.Failure(
                    new List<string> { "No se encontró la autorización con el ID especificado." },
                    HttpStatusCode.NotFound
                );
            }

            return Result<List<AuthorizationDto>>.Success(
                new List<AuthorizationDto> { authorization },
                HttpStatusCode.OK,
                "Autorización obtenida con éxito."
            );
        }
        
        public async Task<Result<List<AuthorizationDto>>> UpdateAsync(int id, AuthorizationQueryDto updateDto)
        {
            var authorizationModel = new AuthorizationModel(updateDto.Date, updateDto.State, updateDto.Description)
            {
                Id = id
            };
            
            var validationResult = await _validator.ValidateAsync(authorizationModel);
            if (!validationResult.IsValid)
            {
                return Result<List<AuthorizationDto>>.Failure(
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    HttpStatusCode.BadRequest
                );
            }
            
            var authorizationDto = new AuthorizationDto(id, updateDto.Date, updateDto.State, updateDto.Description);
            var updatedAuthorization = await _authorizationRepository.UpdateAsync(authorizationDto);

            return Result<List<AuthorizationDto>>.Success(
                new List<AuthorizationDto> { updatedAuthorization },
                HttpStatusCode.OK,
                "Autorización actualizada con éxito."
            );
        }
        
        public async Task<Result<List<bool>>> DeleteAsync(int id)
        {
            var result = await _authorizationRepository.DeleteAsync(id);

            return result
                ? Result<List<bool>>.Success(new List<bool> { true }, HttpStatusCode.OK,
                    "Autorización eliminada con éxito.")
                : Result<List<bool>>.Failure(new List<string> { "No se encontró la autorización especificada." },
                    HttpStatusCode.NotFound);
        }
    }
}