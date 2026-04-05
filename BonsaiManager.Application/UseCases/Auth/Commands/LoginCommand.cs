using BonsaiManager.DTOs.Auth;
using BonsaiManager.DTOs.Auth.Requests;
using BonsaiManager.DTOs.Auth.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.Auth.Commands;

public record LoginCommand(LoginRequest Request) : IRequest<ApiResponse<AuthResponse>>;