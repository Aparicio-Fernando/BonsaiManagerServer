using BonsaiManager.DTOs.Species.Responses;
using BonsaiManager.Shared.Common;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Species;
using BonsaiManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BonsaiManager.Application.UseCases.Species.Queries
{
    public record GetSpeciesByIdQuery(Guid Id) : IRequest<ApiResponse<SpeciesResponse>>;
    
}
