using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;

namespace BonsaiManager.Application.UseCases.CareRecords.Commands;

public record DeleteCareRecordCommand(Guid Id) : IRequest<ApiResponse<bool>>;