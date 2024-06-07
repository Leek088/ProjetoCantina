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
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;
        private readonly IProdutoService _produtoService;

        public VendaController(IVendaService vendaService, IProdutoService produtoService)
        {
            _vendaService = vendaService;
            _produtoService = produtoService;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaDTO>>> GetAllVendasAsync()
        {
            var vendasDTO = await _vendaService.GetAllVendasAsync();

            if (vendasDTO == null)
            {
                return NotFound();
            }

            return Ok(vendasDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{vendaID:int}")]
        public async Task<ActionResult<VendaDTO>> GetVendaByID(int vendaID)
        {
            var vendaDTO = await _vendaService.GetVendaByIdAsync(vendaID);

            if (vendaDTO == null)
            {
                return NotFound();
            }

            return Ok(vendaDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public async Task<ActionResult<VendaDTO>> InsertVendaAsync(VendaDTO vendaDTO)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(vendaDTO.ProdutoID);

            if (produto == null)
                return BadRequest("Produto não encontrado!");

            if (vendaDTO.Quantidade == 0 || vendaDTO.Quantidade > produto.Estoque)
                return BadRequest("Quantidade não permitida!");

            produto.Estoque -= vendaDTO.Quantidade;
            var result = await _produtoService.UpdatePrdoutoAsync(produto);

            if (result)
            {
                result = await _vendaService.InsertVendaAsync(vendaDTO);

                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created, "Venda efetuada!");
                }
                else
                {
                    produto.Estoque += vendaDTO.Quantidade;
                    await _produtoService.UpdatePrdoutoAsync(produto);

                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao gravar a venda!");
                }
            }
            else
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar o estoque!");
            }
        }
    }
}
