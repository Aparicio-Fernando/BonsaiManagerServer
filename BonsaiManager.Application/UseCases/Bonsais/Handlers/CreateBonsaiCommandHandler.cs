using BonsaiManager.Application.Interfaces;
using BonsaiManager.Application.UseCases.Bonsais.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Bonsais.Responses;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DomainModels = BonsaiManager.Domain.Models;

namespace BonsaiManager.Application.UseCases.Bonsais.Handlers;

public class CreateBonsaiCommandHandler : IRequestHandler<CreateBonsaiCommand, ApiResponse<BonsaiResponse>>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextService _httpContextService;

    public CreateBonsaiCommandHandler(AppDbContext context, IHttpContextService httpContextService)
    {
        _context = context;
        _httpContextService = httpContextService;
    }

    public async Task<ApiResponse<BonsaiResponse>> Handle(CreateBonsaiCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserId();

        var speciesExists = await _context.Species
            .AnyAsync(s => s.Id == request.Request.SpeciesId, cancellationToken);
        if (!speciesExists)
            return ApiResponse<BonsaiResponse>.Fail("La especie indicada no existe.");

        var bonsai = new DomainModels.Bonsai
        {
            Id = Guid.NewGuid(),
            Name = request.Request.Name,
            Style = request.Request.Style,
            AcquisitionDate = request.Request.AcquisitionDate,
            Notes = request.Request.Notes,
            ImageUrl = request.Request.ImageUrl,
            SpeciesId = request.Request.SpeciesId,
            UserId = userId
        };

        await _context.Bonsais.AddAsync(bonsai, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var speciesName = await _context.Species
            .Where(s => s.Id == bonsai.SpeciesId)
            .Select(s => s.Name)
            .FirstAsync(cancellationToken);

        var response = new BonsaiResponse
        {
            Id = bonsai.Id,
            Name = bonsai.Name,
            Style = bonsai.Style,
            AcquisitionDate = bonsai.AcquisitionDate,
            Notes = bonsai.Notes,
            ImageUrl = bonsai.ImageUrl,
            SpeciesId = bonsai.SpeciesId,
            SpeciesName = speciesName,
            UserId = bonsai.UserId,
            CreatedAt = bonsai.CreatedAt
        };

        return ApiResponse<BonsaiResponse>.Ok(response, "Bonsai creado correctamente.");
    }
}