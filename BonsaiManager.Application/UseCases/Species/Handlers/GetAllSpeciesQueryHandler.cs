using BonsaiManager.Application.UseCases.Species.Queries;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Species;
using BonsaiManager.DTOs.Species.Responses;
using BonsaiManager.Shared;
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
    public class GetAllSpeciesQueryHandler : IRequestHandler<GetAllSpeciesQuery, ApiResponse<List<SpeciesResponse>>>
    {
        private readonly AppDbContext _context;
        public GetAllSpeciesQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<List<SpeciesResponse>>> Handle(GetAllSpeciesQuery request, CancellationToken cancellationToken)
        {
            var species = await _context.Species
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new SpeciesResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                })
                .ToListAsync(cancellationToken);

            return ApiResponse<List<SpeciesResponse>>.Ok(species);
        }
    }
}
