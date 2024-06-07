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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllProdutosAsync()
        {
            var produtosDto = await _produtoService.GetAllProdutosAsync();

            if (produtosDto == null)
            {
                return NotFound();
            }

            return Ok(produtosDto);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("Categoria")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllProdutosComCategoriaAsync()
        {
            var produtosDto = await _produtoService.GetAllProdutosComCategoriaAsync();

            if (produtosDto == null)
            {
                return NotFound();
            }

            return Ok(produtosDto);
        } 
        
        [HttpGet("Vendas")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllProdutosComVendasAsync()
        {
            var produtosDto = await _produtoService.GetAllProdutosComVendasAsync();

            if (produtosDto == null)
            {
                return NotFound();
            }

            return Ok(produtosDto);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{produtoID:int}")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoByID(int produtoID)
        {
            var produtoDTO = await _produtoService.GetProdutoByIdAsync(produtoID);

            if (produtoDTO == null)
            {
                return NotFound();
            }

            return Ok(produtoDTO);
        }
        
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{produtoID:int}/Categoria")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoComCategoriaByID(int produtoID)
        {
            var produtoDTO = await _produtoService.GetProdutoComCategoriaByIdAsync(produtoID);

            if (produtoDTO == null)
            {
                return NotFound();
            }

            return Ok(produtoDTO);
        } 
        
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{produtoID:int}/Vendas")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoComVendasByID(int produtoID)
        {
            var produtoDTO = await _produtoService.GetProdutoComVendasByIdAsync(produtoID);

            if (produtoDTO == null)
            {
                return NotFound();
            }

            return Ok(produtoDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{nomeProduto}")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoByNome(string nomeProduto)
        {
            var produtoDTO = await _produtoService.GetProdutoByNomeAsync(nomeProduto);

            if (produtoDTO == null)
            {
                return NotFound();
            }

            return Ok(produtoDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{nomeProduto}/Categoria")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoComCategoriaByNome(string nomeProduto)
        {
            var produtoDTO = await _produtoService.GetProdutoComCategoriaByNomeAsync(nomeProduto);

            if (produtoDTO == null)
            {
                return NotFound();
            }

            return Ok(produtoDTO);
        }
        
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{nomeProduto}/Vendas")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoComVendasByNome(string nomeProduto)
        {
            var produtoDTO = await _produtoService.GetProdutoComVendasByNomeAsync(nomeProduto);

            if (produtoDTO == null)
            {
                return NotFound();
            }

            return Ok(produtoDTO);
        }
        
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{nomeProduto}/Categoria/Vendas")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoComCategoriaVendasByNome(string nomeProduto)
        {
            var produtoDTO = await _produtoService.GetProdutoComCategoriaVendasByNomeAsync(nomeProduto);

            if (produtoDTO == null)
            {
                return NotFound();
            }

            return Ok(produtoDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> InsertProdutoAsync(ProdutoDTO produtoDTO)
        {
            var result = await _produtoService.InsertProdutoAsync(produtoDTO);

            if (result) return Ok(result);
            else return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Put))]
        [HttpPut]
        public async Task<ActionResult<ProdutoDTO>> UpdateProdutoAsync(ProdutoDTO produtoDTO)
        {
            var result = await _produtoService.UpdatePrdoutoAsync(produtoDTO);

            if (result) return Ok(result);
            else return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{produtoID:Int}")]
        public async Task<ActionResult<bool>> DeleteProdutoByIdAsync(int produtoID)
        {
            var result = await _produtoService.DeleteProdutoAsync(produtoID);

            if (result) return Ok(result);
            else return BadRequest();
        }
    }
}
