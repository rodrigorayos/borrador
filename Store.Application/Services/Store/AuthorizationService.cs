using System.Net;
using FluentValidation;
using Store.Domain.Dtos.Store;
using Store.Domain.Repositories.Store;
using Store.Domain.Responses;

namespace Store.Application.Services.Store
{
    public class AuthorizationService
    {
        private readonly IValidator<AuthorizationQueryDto> _validator;
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthorizationService(
            IValidator<AuthorizationQueryDto> validator,
            IAuthorizationRepository authorizationRepository)
        {
            _validator = validator;
            _authorizationRepository = authorizationRepository;
        }

        public async Task<Result<List<AuthorizationDto>>> CreateAuthorization(AuthorizationQueryDto createDto)
        {
            var validationResult = await _validator.ValidateAsync(createDto);
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
                authorizations?.ToList() ?? new List<AuthorizationDto>(),
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
                    new List<string> { $"No se encontró una autorización con el ID {id}." },
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
            var existingAuthorization = await _authorizationRepository.GetByIdAsync(id);
            if (existingAuthorization == null)
            {
                return Result<List<AuthorizationDto>>.Failure(
                    new List<string> { $"No se encontró una autorización con el ID {id}." },
                    HttpStatusCode.NotFound
                );
            }

            var validationResult = await _validator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                return Result<List<AuthorizationDto>>.Failure(
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    HttpStatusCode.BadRequest
                );
            }

            var authorizationDto = new AuthorizationDto(id, updateDto.Date, updateDto.State, updateDto.Description);

            try
            {
                var updatedAuthorization = await _authorizationRepository.UpdateAsync(authorizationDto);
                return Result<List<AuthorizationDto>>.Success(
                    new List<AuthorizationDto> { updatedAuthorization },
                    HttpStatusCode.OK,
                    "Autorización actualizada con éxito."
                );
            }
            catch (Exception ex)
            {
                return Result<List<AuthorizationDto>>.Failure(
                    new List<string> { $"Error al actualizar la autorización: {ex.Message}" },
                    HttpStatusCode.InternalServerError
                );
            }
        }

        public async Task<Result<List<bool>>> DeleteAsync(int id)
        {
            var result = await _authorizationRepository.DeleteAsync(id);

            return result
                ? Result<List<bool>>.Success(new List<bool> { true }, HttpStatusCode.OK, "Autorización eliminada con éxito.")
                : Result<List<bool>>.Failure(new List<string> { "No se encontró la autorización especificada." }, HttpStatusCode.NotFound);
        }
    }
}
