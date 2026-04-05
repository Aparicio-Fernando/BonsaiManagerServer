using BonsaiManager.Application.Interfaces;
using BonsaiManager.Application.UseCases.Auth.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Auth.Responses;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResponse>>
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(AppDbContext context, IJwtService jwtService, IPasswordHasher passwordHasher)
    {
        _context = context;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<ApiResponse<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Request.Email, cancellationToken);

        if (user is null || !_passwordHasher.Verify(request.Request.Password, user.PasswordHash))
            return ApiResponse<AuthResponse>.Fail("Email o contraseña incorrectos.");

        var token = _jwtService.GenerateToken(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Token = token
        };

        return ApiResponse<AuthResponse>.Ok(response, "Login exitoso.");
    }
}