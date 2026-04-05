using BonsaiManager.DTOs.Bonsais;
using BonsaiManager.DTOs.Bonsais.Requests;
using BonsaiManager.DTOs.Bonsais.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.Bonsais.Commands;

public record CreateBonsaiCommand(Guid UserId, CreateBonsaiRequest Request) : IRequest<ApiResponse<BonsaiResponse>>;