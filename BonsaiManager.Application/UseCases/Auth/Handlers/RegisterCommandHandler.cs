using BonsaiManager.Application.UseCases.Auth.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Auth;
using BonsaiManager.DTOs.Auth.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DomainModels = BonsaiManager.Domain.Models;

namespace BonsaiManager.Application.UseCases.Auth.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<AuthResponse>>
{
    private readonly AppDbContext _context;

    public RegisterCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<AuthResponse>> Handle(RegisterCommand request,  CancellationToken cancellationToken)
    {
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == request.Request.Email, cancellationToken);

        if (emailExists)
            return ApiResponse<AuthResponse>.Fail("Ya existe un usuario con ese email.");

        var user = new DomainModels.User
        {
            Id = Guid.NewGuid(),
            Name = request.Request.Name,
            Email = request.Request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Request.Password),
            Role = "User"
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Token = string.Empty
        };

        return ApiResponse<AuthResponse>.Ok(response, "Usuario registrado correctamente.");
    }
}