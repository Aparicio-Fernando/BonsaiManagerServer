using BonsaiManager.DTOs.CareRecords;
using BonsaiManager.DTOs.CareRecords.Requests;
using BonsaiManager.DTOs.CareRecords.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.CareRecords.Commands;

public record AddCareRecordCommand(Guid BonsaiId, AddCareRecordRequest Request) : IRequest<ApiResponse<CareRecordResponse>>;
