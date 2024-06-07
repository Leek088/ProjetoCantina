﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjetoCantina.WEB.Hub;
using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using ProjetoCantina.WEB.Util;

namespace ProjetoCantina.WEB.Controllers;

[Authorize]
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly IHubContext<NotificacaoHub> _hubContext;

    public UsuarioController(IUsuarioService usuarioService,
       IHubContext<NotificacaoHub> hubContext)
    {
        _usuarioService = usuarioService;
        _hubContext = hubContext;
    }

    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<IEnumerable<UsuarioViewModel>?>> Index()
    {
        ViewBag.EnableDataTables = true;

        var usuarios = await _usuarioService.GetAllUsuariosAsync();

        return View(usuarios);
    }

    [HttpGet]
    public IActionResult Adicionar()
    {
        ViewBag.HabilitarNotificacao = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(UsuarioViewModel usuarioViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _usuarioService.AdicionarUsuarioAsync(usuarioViewModel);

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
    public async Task<ActionResult<UsuarioViewModel>> Editar(int id)
    {
        ViewBag.HabilitarNotificacao = true;
        var usuario = await _usuarioService.GetUsuarioByIDAsync(id);

        if (usuario == null)
            return View("Error");

        return View(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(UsuarioViewModel usuarioViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _usuarioService.UpdateUsuarioAsync(usuarioViewModel);

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
        return Ok(usuarioViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<UsuarioViewModel>> Deletar(int id)
    {
        ViewBag.HabilitarNotificacao = true;
        var usuario = await _usuarioService.GetUsuarioByIDAsync(id);

        if (usuario == null)
            return View("Error");

        return View(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Deletar(UsuarioViewModel usuarioViewModel)
    {
        var ususario = await _usuarioService.GetUsuarioByIDAsync(usuarioViewModel.UsuarioID);

        if (ususario == null)
        {
            InfoNotificacao.Mensagem = $"<div class='alert alert-warning'>Este usuario não existe!</div>";
        }
        else
        {
            var result = await _usuarioService.DeleteUsuarioAsync(usuarioViewModel.UsuarioID);

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
        return Ok(usuarioViewModel.UsuarioID);
    }

    private string RecuperarErros()
    {
        return string.Join("</br> ", ModelState.Values
                     .SelectMany(x => x.Errors)
                     .Select(x => x.ErrorMessage));
    }
}
