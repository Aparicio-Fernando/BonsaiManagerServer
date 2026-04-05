using BonsaiManager.Application.Interfaces;
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
    private readonly IHttpContextService _httpContextService;

    public GetCareRecordsByBonsaiQueryHandler(AppDbContext context, IHttpContextService httpContextService)
    {
        _context = context;
        _httpContextService = httpContextService;
    }

    public async Task<ApiResponse<List<CareRecordResponse>>> Handle(GetCareRecordsByBonsaiQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserId();

        var bonsaiExists = await _context.Bonsais
            .AnyAsync(b => b.Id == request.BonsaiId && b.UserId == userId, cancellationToken);

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