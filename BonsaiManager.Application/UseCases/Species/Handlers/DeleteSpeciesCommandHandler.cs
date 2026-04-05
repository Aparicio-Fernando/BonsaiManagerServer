using BonsaiManager.Application.UseCases.Species.Commands;
using BonsaiManager.Data.Context;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Species.Handlers;

public class DeleteSpeciesCommandHandler : IRequestHandler<DeleteSpeciesCommand, ApiResponse<bool>>
{
    private readonly AppDbContext _context;

    public DeleteSpeciesCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<bool>> Handle(
        DeleteSpeciesCommand request,
        CancellationToken cancellationToken)
    {
        var species = await _context.Species
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (species is null)
            return ApiResponse<bool>.Fail("Especie no encontrada.");

        var hasBonsais = await _context.Bonsais
            .AnyAsync(b => b.SpeciesId == request.Id, cancellationToken);

        if (hasBonsais)
            return ApiResponse<bool>.Fail("No se puede eliminar una especie que tiene bonsais asociados.");

        _context.Species.Remove(species);
        await _context.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.Ok(true, "Especie eliminada correctamente.");
    }
}