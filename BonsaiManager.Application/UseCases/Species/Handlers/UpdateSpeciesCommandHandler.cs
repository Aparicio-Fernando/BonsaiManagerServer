using BonsaiManager.Application.UseCases.Species.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Species;
using BonsaiManager.DTOs.Species.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Species.Handlers;

public class UpdateSpeciesCommandHandler : IRequestHandler<UpdateSpeciesCommand, ApiResponse<SpeciesResponse>>
{
    private readonly AppDbContext _context;

    public UpdateSpeciesCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<SpeciesResponse>> Handle(UpdateSpeciesCommand request, CancellationToken cancellationToken)
    {
        var species = await _context.Species
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (species is null)
            return ApiResponse<SpeciesResponse>.Fail("Especie no encontrada.");

        var nameExists = await _context.Species
            .AnyAsync(s => s.Name == request.Request.Name && s.Id != request.Id, cancellationToken);

        if (nameExists)
            return ApiResponse<SpeciesResponse>.Fail("Ya existe una especie con ese nombre.");

        species.Name = request.Request.Name;
        species.Description = request.Request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        var response = new SpeciesResponse
        {
            Id = species.Id,
            Name = species.Name,
            Description = species.Description
        };

        return ApiResponse<SpeciesResponse>.Ok(response, "Especie actualizada correctamente.");
    }
}