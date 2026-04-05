using BonsaiManager.Application.Common.Behaviors;
using BonsaiManager.Data.Context;
using BonsaiManager.DTOs.Species;
using BonsaiManager.DTOs.Species.Responses;
using BonsaiManager.Shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Application.UseCases.Species.Queries
{
    public record GetAllSpeciesQuery : IRequest<ApiResponse<List<SpeciesResponse>>>;    
    
}
