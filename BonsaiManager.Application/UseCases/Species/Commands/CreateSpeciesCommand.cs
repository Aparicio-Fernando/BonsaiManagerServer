using BonsaiManager.DTOs.Species;
using BonsaiManager.DTOs.Species.Requests;
using BonsaiManager.DTOs.Species.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Application.UseCases.Species.Commands
{
    public record CreateSpeciesCommand (CreateSpeciesRequest Request) : IRequest<ApiResponse<SpeciesResponse>>;
        
}
