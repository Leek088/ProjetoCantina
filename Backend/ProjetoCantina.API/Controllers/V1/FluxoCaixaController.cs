using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.Services.Service;
using System.Diagnostics.Metrics;

namespace ProjetoCantina.API.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/V{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FluxoCaixaController : ControllerBase
    {
        private readonly IFluxoCaixaService _fluxoCaixaService;

        public FluxoCaixaController(IFluxoCaixaService fluxoCaixaService)
        {
            _fluxoCaixaService = fluxoCaixaService;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FluxoCaixaDTO>>> GetAllFluxosCaixasAsync()
        {
            var fluxosCaixasDto = await _fluxoCaixaService.GetAllFluxosCaixasAsync();

            if (fluxosCaixasDto == null)
            {
                return NotFound();
            }

            return Ok(fluxosCaixasDto);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public async Task<ActionResult<FluxoCaixaDTO>> InsertFluxoCaixaAsync(FluxoCaixaDTO fluxoCaixaDTO)
        {
            var result = await _fluxoCaixaService.InsertFluxoCaixaAsync(fluxoCaixaDTO);

            if (result)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
