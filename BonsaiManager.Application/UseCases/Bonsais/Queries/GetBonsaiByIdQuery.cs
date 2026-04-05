using BonsaiManager.DTOs.Bonsais;
using BonsaiManager.DTOs.Bonsais.Responses;
using BonsaiManager.Shared;
using BonsaiManager.Shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Application.UseCases.Bonsais.Queries
{
    public record GetBonsaiByIdQuery(Guid Id) : IRequest<ApiResponse<BonsaiResponse>>;

}
