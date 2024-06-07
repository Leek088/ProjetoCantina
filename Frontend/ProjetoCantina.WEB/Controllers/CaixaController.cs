using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using ProjetoCantina.WEB.Hub;
using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using ProjetoCantina.WEB.Util;
using System.Security.Claims;

namespace ProjetoCantina.WEB.Controllers;

[Authorize]
public class CaixaController : Controller
{
    private readonly ICaixaService _caixaService;
    private readonly IUsuarioService _usuarioService;
    private readonly IFluxoCaixaService _fluxoCaixaService;
    private readonly IHubContext<NotificacaoHub> _hubContext;

    public CaixaController(ICaixaService caixaService,
        IHubContext<NotificacaoHub> hubContext,
        IUsuarioService usuarioService,
        IFluxoCaixaService fluxoCaixaService)
    {
        _caixaService = caixaService;
        _hubContext = hubContext;
        _usuarioService = usuarioService;
        _fluxoCaixaService = fluxoCaixaService;
    }

    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CaixaViewModel>?>> Index()
    {
        ViewBag.EnableDataTables = true;

        var caixs = await _caixaService.GetAllCaixasComUsuarioAsync();

        return View(caixs);
    }

    [HttpGet]
    public async Task<IActionResult> Adicionar()
    {
        ViewBag.HabilitarNotificacao = true;
        ViewBag.UsuarioID = User.FindFirst("UsuarioID")?.Value;
        ViewBag.NomeUsuario = User.FindFirst(ClaimTypes.Name)?.Value;
        ViewBag.SelectUsuario = await ListaDeUsuariosAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(CaixaViewModel caixaViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _caixaService.CreateCaixaAsync(caixaViewModel);

            if (result)
            {
                InfoNotificacao.Mensagem = "<div class='alert alert-success'>Caixa aberto com sucesso.</div>";
            }
            else
            {
                InfoNotificacao.Mensagem = "<div class='alert alert-danger'>Erro interno no servidor.</div>";
            }
        }
        else
        {
            var erros = RecuperarErros();
            InfoNotificacao.Mensagem = $"<div class='alert alert-danger'>{erros}</div>";
        }

        await InfoNotificacao.EnviarNotificacaoAsync(_hubContext);
        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<CaixaViewModel>> View(int id)
    {
        ViewBag.HabilitarNotificacao = true;
        var categoria = await _caixaService.GetcaixaByIDAsync(id);

        if (categoria == null)
            return View("Error");

        return View(categoria);
    }

    private string RecuperarErros()
    {
        return string.Join("</br> ", ModelState.Values
                     .SelectMany(x => x.Errors)
                     .Select(x => x.ErrorMessage));
    }

    private async Task<SelectList> ListaDeUsuariosAsync()
    {
        return new SelectList
              (await _usuarioService.GetAllUsuariosAsync(), "UsuarioID", "NomeUsuario");
    }
}
