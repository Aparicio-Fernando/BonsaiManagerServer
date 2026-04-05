using BonsaiManager.Application.UseCases.Species.Commands;
using BonsaiManager.Data.Context;
using DomainModels = BonsaiManager.Domain.Models;
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
    public class CreateSpeciesCommandHandler : IRequestHandler<CreateSpeciesCommand, ApiResponse<SpeciesResponse>>
    {
        private readonly AppDbContext _context;

        public CreateSpeciesCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<SpeciesResponse>> Handle(CreateSpeciesCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.Species
                .AnyAsync(s => s.Name == request.Request.Name, cancellationToken);

            if (exists)
                return ApiResponse<SpeciesResponse>.Fail("Ya existe una especie con ese nombre.");

            var species = new DomainModels.Species
            {
                Id = Guid.NewGuid(),
                Name = request.Request.Name,
                Description = request.Request.Description
            };

            await _context.Species.AddAsync(species, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new SpeciesResponse
            {
                Id = species.Id,
                Name = species.Name,
                Description = species.Description
            };

            return ApiResponse<SpeciesResponse>.Ok(response, "Especie creada correctamente.");
        }
    }
}
