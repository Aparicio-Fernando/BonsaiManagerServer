using BonsaiManager.Application.UseCases.Species.Commands;
using BonsaiManager.Application.UseCases.Species.Queries;
using BonsaiManager.DTOs.Species.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BonsaiManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SpeciesController : ControllerBase
    {
        private readonly IMediator _mediador;
        public SpeciesController(IMediator mediador)
        {
            _mediador = mediador;
        }

        /// <summary>Obtiene todas las especies</summary>
        [HttpGet]
        [SwaggerOperation(Summary = "ObtenerTodasLasEspecies")]
        public async Task<IActionResult> ObtenerTodasLasEspecies()
        {
            var resultado = await _mediador.Send(new GetAllSpeciesQuery());
            return Ok(resultado);
        }

        /// <summary>Obtiene una especie por Id</summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "ObtenerEspeciePorId")]
        public async Task<IActionResult> ObtenerEspeciePorId(Guid id)
        {
            var resultado = await _mediador.Send(new GetSpeciesByIdQuery(id));
            if (!resultado.Success)
                return NotFound(resultado);
            return Ok(resultado);
        }

        /// <summary>Crea una nueva especie (Solo Admin)</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "CrearEspecie")]
        public async Task<IActionResult> CrearEspecie([FromBody] CreateSpeciesRequest request)
        {
            var resultado = await _mediador.Send(new CreateSpeciesCommand(request));
            return Ok(resultado);
        }

        /// <summary>Modifica una especie (Solo Admin)</summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "EditarEspecie")]
        public async Task<IActionResult> EditarEspecie(Guid id, [FromBody] UpdateSpeciesRequest request)
        {
            var resultado = await _mediador.Send(new UpdateSpeciesCommand(id, request));
            return Ok(resultado);
        }

        /// <summary>Elimina una especie (Solo Admin)</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "EliminarEspecie")]
        public async Task<IActionResult> EliminarEspecie(Guid id)
        {
            var resultado = await _mediador.Send(new DeleteSpeciesCommand(id));
            return Ok(resultado);
        }
    }
}
