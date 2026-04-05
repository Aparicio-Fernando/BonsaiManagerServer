using BonsaiManager.DTOs.Species;
using BonsaiManager.DTOs.Species.Requests;
using BonsaiManager.DTOs.Species.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.Species.Commands;

public record UpdateSpeciesCommand(Guid Id, UpdateSpeciesRequest Request) : IRequest<ApiResponse<SpeciesResponse>>;