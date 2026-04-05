using BonsaiManager.Application.UseCases.Bonsais.Queries;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Bonsais;
using BonsaiManager.DTOs.Bonsais.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Bonsais.Handlers;

public class GetBonsaiByIdQueryHandler : IRequestHandler<GetBonsaiByIdQuery, ApiResponse<BonsaiResponse>>
{
    private readonly AppDbContext _context;

    public GetBonsaiByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<BonsaiResponse>> Handle(GetBonsaiByIdQuery request, CancellationToken cancellationToken)
    {
        var bonsai = await _context.Bonsais
            .AsNoTracking()
            .Include(b => b.Species)
            .Where(b => b.Id == request.Id)
            .Select(b => new BonsaiResponse
            {
                Id = b.Id,
                Name = b.Name,
                Style = b.Style,
                AcquisitionDate = b.AcquisitionDate,
                Notes = b.Notes,
                ImageUrl = b.ImageUrl,
                SpeciesId = b.SpeciesId,
                SpeciesName = b.Species.Name,
                UserId = b.UserId
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (bonsai is null)
            return ApiResponse<BonsaiResponse>.Fail("Bonsai no encontrado.");

        return ApiResponse<BonsaiResponse>.Ok(bonsai);
    }
}