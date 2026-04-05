using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.Bonsais.Commands;

public record DeleteBonsaiCommand(Guid Id) : IRequest<ApiResponse<bool>>;
