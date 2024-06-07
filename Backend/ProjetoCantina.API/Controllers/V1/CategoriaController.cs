using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Services.Interfaces;

namespace ProjetoCantina.API.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/V{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAllCategoriasAsync()
        {
            var categoriasDto = await _categoriaService.GetAllCategoriasAsync();

            if (categoriasDto == null)
            {
                return NotFound();
            }

            return Ok(categoriasDto);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("Produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAllCategoriasComProdutosAsync()
        {
            var categoriasDto = await _categoriaService.GetAllCategoriasComProdutosAsync();

            if (categoriasDto == null)
            {
                return NotFound();
            }

            return Ok(categoriasDto);
        }


        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{categoriaID:int}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoriaByID(int categoriaID)
        {
            var categoriaDto = await _categoriaService.GetCategoriaByIdAsync(categoriaID);

            if (categoriaDto == null)
            {
                return NotFound();
            }

            return Ok(categoriaDto);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{categoriaID:int}/Produtos")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoriaComProdutoByID(int categoriaID)
        {
            var categoriaDto = await _categoriaService.GetCategoriaComProdutoByIdAsync(categoriaID);

            if (categoriaDto == null)
            {
                return NotFound();
            }

            return Ok(categoriaDto);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> InsertCategoriaAsync(CategoriaDTO categoriaDTO)
        {
            var result = await _categoriaService.InsertCategoriaAsync(categoriaDTO);

            if (result)
                return Ok(result);
            else
                return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Put))]
        [HttpPut]
        public async Task<ActionResult<CategoriaDTO>> UpdateCategoriaAsync(CategoriaDTO categoriaDTO)
        {
            var result = await _categoriaService.UpdateCategoriaAsync(categoriaDTO);

            if (result) return Ok(result);
            else return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{categoriaID:Int}")]
        public async Task<ActionResult<bool>> DeleteCategoriaByIdAsync(int categoriaID)
        {
            var result = await _categoriaService.DeleteCategoriaAsync(categoriaID);

            if (result) return Ok(result);
            else return BadRequest();
        }
    }
}
