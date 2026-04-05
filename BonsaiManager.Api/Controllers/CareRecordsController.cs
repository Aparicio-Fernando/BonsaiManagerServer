using BonsaiManager.Application.UseCases.CareRecords.Commands;
using BonsaiManager.Application.UseCases.CareRecords.Queries;
using BonsaiManager.DTOs.CareRecords.Requests;
using BonsaiManager.Shared.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BonsaiManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CareRecordsController : ControllerBase
    {
        private readonly IMediator _mediador;

        public CareRecordsController(IMediator mediador)
        {
            _mediador = mediador;
        }

        /// <summary>Obtiene los registros de cuidado de un bonsai</summary>
        [HttpGet("{bonsaiId}")]
        [SwaggerOperation(Summary = "ObtenerRegistrosPorBonsai")]
        public async Task<IActionResult> ObtenerRegistrosPorBonsai(Guid bonsaiId)
        {
            var resultado = await _mediador.Send(new GetCareRecordsByBonsaiQuery(bonsaiId));
            if (!resultado.Success)
                return NotFound(resultado);
            return Ok(resultado);
        }

        /// <summary>Agrega un registro de cuidado a un bonsai</summary>
        [HttpPost("{bonsaiId}")]
        [SwaggerOperation(Summary = "AgregarRegistroDeCuidado")]
        public async Task<IActionResult> AgregarRegistroDeCuidado(Guid bonsaiId, [FromBody] AddCareRecordRequest request)
        {
            var resultado = await _mediador.Send(new AddCareRecordCommand(bonsaiId, request));
            if (!resultado.Success)
                return NotFound(resultado);
            return Ok(resultado);
        }

        /// <summary>Elimina un registro de cuidado</summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "EliminarRegistroDeCuidado")]
        public async Task<IActionResult> EliminarRegistroDeCuidado(Guid id)
        {
            var resultado = await _mediador.Send(new DeleteCareRecordCommand(id));
            if (!resultado.Success)
                return NotFound(resultado);
            return Ok(resultado);
        }
    }
}