using BonsaiManager.Application.Interfaces;
using BonsaiManager.Application.UseCases.CareRecords.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.Domain.Enums;
using BonsaiManager.DTOs.CareRecords;
using BonsaiManager.DTOs.CareRecords.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DomainModels = BonsaiManager.Domain.Models;

namespace BonsaiManager.Application.UseCases.CareRecords.Handlers;

public class AddCareRecordCommandHandler : IRequestHandler<AddCareRecordCommand, ApiResponse<CareRecordResponse>>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextService _httpContextService;

    public AddCareRecordCommandHandler(AppDbContext context, IHttpContextService httpContextService)
    {
        _context = context;
        _httpContextService = httpContextService;
    }

    public async Task<ApiResponse<CareRecordResponse>> Handle(AddCareRecordCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserId();

        var bonsaiExists = await _context.Bonsais
           .AnyAsync(b => b.Id == request.BonsaiId && b.UserId == userId, cancellationToken);

        if (!bonsaiExists)
            return ApiResponse<CareRecordResponse>.Fail("Bonsai no encontrado.");        

        var record = new DomainModels.CareRecord
        {
            Id = Guid.NewGuid(),
            CareType = request.Request.CareType,
            Date = request.Request.Date,
            Notes = request.Request.Notes,
            BonsaiId = request.BonsaiId
        };

        await _context.CareRecords.AddAsync(record, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = new CareRecordResponse
        {
            Id = record.Id,
            CareType = record.CareType,
            CareTypeName = record.CareType.ToString(),
            Date = record.Date,
            Notes = record.Notes,
            BonsaiId = record.BonsaiId,
            CreatedAt = record.CreatedAt
        };

        return ApiResponse<CareRecordResponse>.Ok(response, "Registro de cuidado agregado correctamente.");
    }
}