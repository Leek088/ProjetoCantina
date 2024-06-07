using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.Services.Service;

namespace ProjetoCantina.API.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/V{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixaService _caixaService;

        public CaixaController(ICaixaService caixaService)
        {
            _caixaService = caixaService;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaixaDTO>>> GetAllCaixasAsync()
        {
            var caixasDTO = await _caixaService.GetAllCaixasAsync();

            if (caixasDTO == null)
            {
                return NotFound();
            }

            return Ok(caixasDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{caixaID:int}")]
        public async Task<ActionResult<CaixaDTO>> GetCaixaByID(int caixaID)
        {
            var caixaDTO = await _caixaService.GetCaixaByIdAsync(caixaID);

            if (caixaDTO == null)
            {
                return NotFound();
            }

            return Ok(caixaDTO);
        }
        
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{codigoUnico}")]
        public async Task<ActionResult<CaixaDTO>> GetCaixaByCodigoUnico(string codigoUnico)
        {
            var caixaDTO = await _caixaService.GetCaixaByCodigoUnicoAsync(codigoUnico);

            if (caixaDTO == null)
            {
                return NotFound();
            }

            return Ok(caixaDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public async Task<ActionResult<CaixaDTO>> InsertCaixaAsync(CaixaDTO caixaDTO)
        {
            var result = await _caixaService.InsertCaixaAsync(caixaDTO);    

            if (result) return Ok(result);
            else return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Put))]
        [HttpPut]
        public async Task<ActionResult<CaixaDTO>> UpdateCaixaAsync(CaixaDTO caixaDTO)
        {
            var result = await _caixaService.UpdateCaixaAsync(caixaDTO);

            if (result) return Ok(result);
            else return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{caixaID:Int}")]
        public async Task<ActionResult<bool>> DeleteCategoriaByIdAsync(int caixaID)
        {
            var result = await _caixaService.DeleteCaixaAsync(caixaID);

            if (result) return Ok(result);
            else return BadRequest();
        }
    }
}
