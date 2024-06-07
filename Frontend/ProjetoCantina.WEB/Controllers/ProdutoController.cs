using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using ProjetoCantina.WEB.Hub;
using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using ProjetoCantina.WEB.Util;

namespace ProjetoCantina.WEB.Controllers;

[Authorize]
public class ProdutoController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;
    private readonly IHubContext<NotificacaoHub> _hubContext;

    public ProdutoController(IProdutoService produtoService,
        IHubContext<NotificacaoHub> hubContext,
        ICategoriaService categoriaService)
    {
        _produtoService = produtoService;
        _hubContext = hubContext;
        _categoriaService = categoriaService;
    }

    [HttpGet, AllowAnonymous]
    public async Task<ActionResult> Index()
    {
        ViewBag.EnableDataTables = true;

        var produtos = await _produtoService.GetAllProdutosAsync();

        return View(produtos);
    }

    [HttpGet]
    public async Task<IActionResult> Adicionar()
    {
        ViewBag.HabilitarNotificacao = true;
        ViewBag.SelectCategoria = await ListaDeCategoriasAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _produtoService.AdicionarProdutoAsync(produtoViewModel);

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
            InfoNotificacao.Mensagem = $"<div class='alert alert-danger'>{erros}.</div>";
        }

        await InfoNotificacao.EnviarNotificacaoAsync(_hubContext);
        return View(StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        ViewBag.HabilitarNotificacao = true;
        ViewBag.SelectCategoria = await ListaDeCategoriasAsync();

        var produto = await _produtoService.GetProdutoByIDAsync(id);

        if (produto == null)
            return View("Error");

        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _produtoService.UpdateProdutoAsync(produtoViewModel);

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
            InfoNotificacao.Mensagem = $"<div class='alert alert-danger'>{erros}.</div>";
        }

        await InfoNotificacao.EnviarNotificacaoAsync(_hubContext);
        return Ok(produtoViewModel);
    }

    [HttpGet, Authorize]
    public async Task<ActionResult<ProdutoViewModel>> Deletar(int id)
    {
        ViewBag.HabilitarNotificacao = true;
        ViewBag.SelectCategoria = await ListaDeCategoriasAsync();

        var produto = await _produtoService.GetProdutoByIDAsync(id);

        if (produto == null)
            return View("Error");

        return View(produto);
    }

    [HttpPost, Authorize]
    public async Task<IActionResult> Deletar(ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            var produto = await _produtoService.GetProdutoByIDAsync(produtoViewModel.ProdutoID);

            if (produto == null)
            {
                InfoNotificacao.Mensagem = $"<div class='alert alert-warning'>Este produto não existe!</div>";
            }
            else
            {
                var result = await _produtoService.DeleteProdutoAsync(produtoViewModel.ProdutoID);

                if (result)
                {
                    InfoNotificacao.Mensagem = "<div class='alert alert-success'>Registro apagado com sucesso.</div>";
                }
                else
                {
                    InfoNotificacao.Mensagem = "<div class='alert alert-danger'>Erro interno no servidor.</div>";
                }
            }
        }
        else
        {
            var erros = RecuperarErros();
            InfoNotificacao.Mensagem = $"<div class='alert alert-danger'>{erros}.</div>";
        }

        await InfoNotificacao.EnviarNotificacaoAsync(_hubContext);
        return Ok(produtoViewModel.CategoriaID);
    }

    private async Task<SelectList> ListaDeCategoriasAsync()
    {
        return new SelectList
              (await _categoriaService.GetAllCategoriasAsync(), "CategoriaID", "Nome");
    }

    private string RecuperarErros()
    {
        return string.Join("</br> ", ModelState.Values
                     .SelectMany(x => x.Errors)
                     .Select(x => x.ErrorMessage));
    }
}
