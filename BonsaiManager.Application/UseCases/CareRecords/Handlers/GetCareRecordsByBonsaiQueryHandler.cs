using BonsaiManager.Application.UseCases.CareRecords.Queries;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.CareRecords;
using BonsaiManager.DTOs.CareRecords.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.CareRecords.Handlers;

public class GetCareRecordsByBonsaiQueryHandler : IRequestHandler<GetCareRecordsByBonsaiQuery, ApiResponse<List<CareRecordResponse>>>
{
    private readonly AppDbContext _context;

    public GetCareRecordsByBonsaiQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<CareRecordResponse>>> Handle(GetCareRecordsByBonsaiQuery request, CancellationToken cancellationToken)
    {
        var bonsaiExists = await _context.Bonsais
            .AnyAsync(b => b.Id == request.BonsaiId && b.UserId == request.UserId, cancellationToken);

        if (!bonsaiExists)
            return ApiResponse<List<CareRecordResponse>>.Fail("Bonsai no encontrado.");

        var records = await _context.CareRecords
            .AsNoTracking()
            .Where(c => c.BonsaiId == request.BonsaiId)
            .OrderByDescending(c => c.Date)
            .ToListAsync(cancellationToken);

        var response = records.Select(c => new CareRecordResponse
        {
            Id = c.Id,
            CareType = c.CareType,
            CareTypeName = c.CareType.ToString(),
            Date = c.Date,
            Notes = c.Notes,
            BonsaiId = c.BonsaiId,
            CreatedAt = c.CreatedAt
        }).ToList();

        return ApiResponse<List<CareRecordResponse>>.Ok(response);
    }
}