using BonsaiManager.DTOs.Bonsais;
using BonsaiManager.DTOs.Bonsais.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.Bonsais.Queries;

public record GetBonsaisByUserQuery(Guid UserId, PaginatedRequest Pagination) : IRequest<ApiResponse<PaginatedResponse<BonsaiResponse>>>;