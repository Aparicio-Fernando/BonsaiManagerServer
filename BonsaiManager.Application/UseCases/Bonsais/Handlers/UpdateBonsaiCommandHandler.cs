using BonsaiManager.Application.UseCases.Bonsais.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Bonsais.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Bonsais.Handlers;

public class UpdateBonsaiCommandHandler : IRequestHandler<UpdateBonsaiCommand, ApiResponse<BonsaiResponse>>
{
    private readonly AppDbContext _context;

    public UpdateBonsaiCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<BonsaiResponse>> Handle(UpdateBonsaiCommand request, CancellationToken cancellationToken)
    {
        var bonsai = await _context.Bonsais
            .FirstOrDefaultAsync(b => b.Id == request.Id && b.UserId == request.UserId, cancellationToken);

        if (bonsai is null)
            return ApiResponse<BonsaiResponse>.Fail("Bonsai no encontrado.");

        var speciesExists = await _context.Species
            .AnyAsync(s => s.Id == request.Request.SpeciesId, cancellationToken);

        if (!speciesExists)
            return ApiResponse<BonsaiResponse>.Fail("La especie indicada no existe.");

        bonsai.Name = request.Request.Name;
        bonsai.Style = request.Request.Style;
        bonsai.AcquisitionDate = request.Request.AcquisitionDate;
        bonsai.Notes = request.Request.Notes;
        bonsai.ImageUrl = request.Request.ImageUrl;
        bonsai.SpeciesId = request.Request.SpeciesId;

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

        return ApiResponse<BonsaiResponse>.Ok(response, "Bonsai actualizado correctamente.");
    }
}