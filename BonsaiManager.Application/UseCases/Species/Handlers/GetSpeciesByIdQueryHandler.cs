using BonsaiManager.Application.UseCases.Species.Queries;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Species.Responses;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Application.UseCases.Species.Handlers
{
    public class GetSpeciesByIdQueryHandler : IRequestHandler<GetSpeciesByIdQuery, ApiResponse<SpeciesResponse>>
    {
        private readonly AppDbContext _context;

        public GetSpeciesByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<SpeciesResponse>> Handle(GetSpeciesByIdQuery request, CancellationToken cancellationToken)
        {
            var species = await _context.Species
                .AsNoTracking()
                .Where(s => s.Id == request.Id)
                .Select(s => new SpeciesResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (species is null)
                return ApiResponse<SpeciesResponse>.Fail("Especie no encontrada.");

            return ApiResponse<SpeciesResponse>.Ok(species);
        }
    }
}
