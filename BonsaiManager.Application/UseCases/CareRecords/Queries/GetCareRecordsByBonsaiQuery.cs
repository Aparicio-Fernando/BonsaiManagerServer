using BonsaiManager.DTOs.CareRecords;
using BonsaiManager.DTOs.CareRecords.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.CareRecords.Queries;

public record GetCareRecordsByBonsaiQuery(Guid BonsaiId, Guid UserId) : IRequest<ApiResponse<List<CareRecordResponse>>>;