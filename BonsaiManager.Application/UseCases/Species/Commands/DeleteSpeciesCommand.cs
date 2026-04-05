using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.Species.Commands;

public record DeleteSpeciesCommand(Guid Id) : IRequest<ApiResponse<bool>>;