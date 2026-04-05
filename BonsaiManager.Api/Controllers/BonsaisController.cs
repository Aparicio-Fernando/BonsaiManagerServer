using BonsaiManager.Application.UseCases.Bonsais.Commands;
using BonsaiManager.Application.UseCases.Bonsais.Queries;
using BonsaiManager.DTOs.Bonsais.Requests;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace BonsaiManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BonsaisController : ControllerBase
    {
        private readonly IMediator _mediador;

        public BonsaisController(IMediator mediador)
        {
            _mediador = mediador;
        }

        /// <summary>Obtiene una lista paginada de Bonsais</summary>
        [HttpGet]
        [SwaggerOperation(Summary = "ObtenerBonsaisPorUser")]
        public async Task<IActionResult> ObtenerBonsaisPorUser([FromQuery] PaginatedRequest pagination)
        {
            var resultado = await _mediador.Send(new GetBonsaisByUserQuery(pagination));
            return Ok(resultado);
        }

        /// <summary>Obtiene Bonsai por Id</summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "ObtenerBonsaisPorId")]
        public async Task<IActionResult> ObtenerBonsaisPorId(Guid id)
        {
            var resultado = await _mediador.Send(new GetBonsaiByIdQuery(id));
            if (!resultado.Success)
                return NotFound(resultado);

            return Ok(resultado);
        }

        ///<summary>Cargar Bonsai</summary>
        [HttpPost]
        [SwaggerOperation(Summary = "CrearBonsai")]
        public async Task<IActionResult> CrearBonsai([FromBody] CreateBonsaiRequest request)
        {
            var resultado = await _mediador.Send(new CreateBonsaiCommand(request));
            return Ok(resultado);
        }

        /// <summary>Modificar Bonsai</summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "EditarBonsai")]
        public async Task<IActionResult> EditarBonsai([FromBody] UpdateBonsaiRequest request, Guid id)
        {
            var resultado = await _mediador.Send(new UpdateBonsaiCommand(id, request));
            return Ok(resultado);
        }

        ///<summary>Eliminar Bonsai</summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "EliminarBonsai")]
        public async Task<IActionResult> EliminarBonsai(Guid id)
        {
            var resultado = await _mediador.Send(new DeleteBonsaiCommand(id));
            return Ok(resultado);
        }
               
    }
}
