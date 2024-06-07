using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjetoCantina.WEB.Hub;
using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using ProjetoCantina.WEB.Util;

namespace ProjetoCantina.WEB.Controllers;

[Authorize]
public class CategoriaController : Controller
{
    private readonly ICategoriaService _categoriaService;
    private readonly IHubContext<NotificacaoHub> _hubContext;

    public CategoriaController(ICategoriaService categoriaService,
        IHubContext<NotificacaoHub> hubContext)
    {
        _categoriaService = categoriaService;
        _hubContext = hubContext;
    }
    
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CategoriaViewModel>?>> Index()
    {
        ViewBag.EnableDataTables = true;

        var categorias = await _categoriaService.GetAllCategoriasAsync();

        return View(categorias);
    }

    [HttpGet]
    public IActionResult Adicionar()
    {
        ViewBag.HabilitarNotificacao = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(CategoriaViewModel categoriaViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoriaService.CreateCategoriaAsync(categoriaViewModel);

            if (result)
            {
                InfoNotificacao.Mensagem = "<div class='alert alert-success'>Cadastro efetuado com sucesso.</div>";
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
        return View(StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ActionResult<CategoriaViewModel>> Editar(int id)
    {
        ViewBag.HabilitarNotificacao = true;
        var categoria = await _categoriaService.GetCategoriaByIDAsync(id);

        if (categoria == null)
            return View("Error");

        return View(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(CategoriaViewModel categoriaViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoriaService.UpdateCategoriaAsync(categoriaViewModel);

            if (result)
            {
                InfoNotificacao.Mensagem = "<div class='alert alert-success'>Cadastro atualizado com sucesso.</div>";
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
        return Ok(categoriaViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<CategoriaViewModel>> Deletar(int id)
    {
        ViewBag.HabilitarNotificacao = true;
        var categoria = await _categoriaService.GetCategoriaByIDAsync(id);

        if (categoria == null)
            return View("Error");

        return View(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Deletar(CategoriaViewModel categoriaViewModel)
    {
        var categoria = await _categoriaService.GetCategoriaByIDAsync(categoriaViewModel.CategoriaID);

        if (categoria == null)
        {
            InfoNotificacao.Mensagem = $"<div class='alert alert-warning'>Esta categoria não existe!</div>";
        }
        else
        {
            var result = await _categoriaService.DeleteCategoriaAsync(categoriaViewModel.CategoriaID);

            if (result)
            {
                InfoNotificacao.Mensagem = "<div class='alert alert-success'>Registro apagado com sucesso.</div>";
            }
            else
            {
                InfoNotificacao.Mensagem = "<div class='alert alert-danger'>Erro interno no servidor.</div>";
            }
        }

        await InfoNotificacao.EnviarNotificacaoAsync(_hubContext);
        return Ok(categoriaViewModel.CategoriaID);
    }

    private string RecuperarErros()
    {
        return string.Join("</br> ", ModelState.Values
                     .SelectMany(x => x.Errors)
                     .Select(x => x.ErrorMessage));
    }
}
