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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAllUsuariosAsync()
        {
            var usuariosDTO = await _usuarioService.GetAllUsuariosAsync();

            if (usuariosDTO == null)
            {
                return NotFound();
            }

            return Ok(usuariosDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("Caixas")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAllUsuariosComCaixasAsync()
        {
            var usuariosDTO = await _usuarioService.GetAllUsuariosComCaixasAsync();

            if (usuariosDTO == null)
            {
                return NotFound();
            }

            return Ok(usuariosDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{usuarioID:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioByID(int usuarioID)
        {
            var usuarioDTO = await _usuarioService.GetUsuarioByIdAsync(usuarioID);

            if (usuarioDTO == null)
            {
                return NotFound();
            }

            return Ok(usuarioDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{usuarioID:int}/Caixas")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioComCaixasByID(int usuarioID)
        {
            var usuarioDTO = await _usuarioService.GetUsuarioComCaixaByIdAsync(usuarioID);

            if (usuarioDTO == null)
            {
                return NotFound();
            }

            return Ok(usuarioDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{nomeUsuario}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioByNomeUsuario(string nomeUsuario)
        {
            var usuarioDTO = await _usuarioService.GetUsuarioByNomeUsuarioAsync(nomeUsuario);

            if (usuarioDTO == null)
            {
                return NotFound();
            }

            return Ok(usuarioDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{nomeUsuario}/Caixas")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioComCaixasByNomeUsuario(string nomeUsuario)
        {
            var usuarioDTO = await _usuarioService.GetUsuarioComCaixasByNomeUsuarioAsync(nomeUsuario);

            if (usuarioDTO == null)
            {
                return NotFound();
            }

            return Ok(usuarioDTO);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Post))]
        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioDTO>> ValidarLoginUsuarioAsync(LoginUsuarioDTO loginUsuarioDTO)
        {
            var result = await _usuarioService.ValidarLoginUsuarioAsync(loginUsuarioDTO);

            if (result)
                return Ok(loginUsuarioDTO);
            else
                return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> InsertUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            var result = await _usuarioService.InsertUsuarioaAsync(usuarioDTO);

            if (result)
                return Ok(result);
            else
                return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
             nameof(DefaultApiConventions.Put))]
        [HttpPut]
        public async Task<ActionResult<UsuarioDTO>> UpdateCategoriaAsync(UsuarioDTO usuarioDTO)
        {
            var result = await _usuarioService.UpdateUsuarioAsync(usuarioDTO);

            if (result) return Ok(result);
            else return BadRequest();
        }

        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{usuarioID:Int}")]
        public async Task<ActionResult<bool>> DeleteCategoriaByIdAsync(int usuarioID)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(usuarioID);

            if (result) return Ok(result);
            else return BadRequest();
        }
    }
}
