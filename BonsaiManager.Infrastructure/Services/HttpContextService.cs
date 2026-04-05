using BonsaiManager.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BonsaiManager.Infrastructure.Services;

/// <summary>
/// Servicio que encapsula el acceso al contexto HTTP actual.
/// Permite obtener información del usuario autenticado (claims del JWT)
/// sin necesidad de inyectar IHttpContextAccessor en cada handler o controller.
/// </summary>
public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Obtiene el Id del usuario autenticado desde los claims del token JWT.
    /// Busca primero el claim 'nameidentifier' y como fallback 'sub'.
    /// Lanza UnauthorizedAccessException si no hay usuario autenticado o el Id no es válido.
    /// </summary>
    public Guid GetCurrentUserId()
    {
        var claim = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)
            ?? _httpContextAccessor.HttpContext?.User
            .FindFirst("sub");

        if (claim is null || !Guid.TryParse(claim.Value, out var userId))
            throw new UnauthorizedAccessException("Usuario no autenticado.");

        return userId;
    }
}