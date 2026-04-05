using BonsaiManager.Application.UseCases.Auth.Queries;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Auth;
using BonsaiManager.DTOs.Auth.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Auth.Handlers;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ApiResponse<AuthResponse>>
{
    private readonly AppDbContext _context;

    public GetCurrentUserQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<AuthResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
            return ApiResponse<AuthResponse>.Fail("Usuario no encontrado.");

        var response = new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Token = string.Empty
        };

        return ApiResponse<AuthResponse>.Ok(response);
    }
}