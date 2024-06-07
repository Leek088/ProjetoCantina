using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using System.Security.Claims;

namespace ProjetoCantina.WEB.Controllers;

public class AccountController : Controller
{
    private readonly ILoginService _loginService;
    private readonly IUsuarioService _usuarioService;

    public AccountController(ILoginService loginService, IUsuarioService usuarioService)
    {
        _loginService = loginService;
        _usuarioService = usuarioService;
    }


    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (!string.IsNullOrEmpty(returnUrl))
        {
            ViewData["ReturnUrl"] = returnUrl;
        }

        if (HttpContext.User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("NotificacaoLogado");
        }

        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel loginViewModel, string? returnUrl = null)
    {
        if (!string.IsNullOrEmpty(returnUrl))
        {
            ViewData["ReturnUrl"] = returnUrl;
        }

        if (ModelState.IsValid)
        {
            var restult = await _loginService.LogarNoSite(loginViewModel);

            if (!restult)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha invalidos.");
            }
            else
            {
                var usuarios = await _usuarioService.GetUsuarioByNomeUsuarioAsync(loginViewModel.NomeUsuario!);

                var claims = new List<Claim>
                {
                    new Claim("UsuarioID", usuarios!.FirstOrDefault()!.UsuarioID!.ToString()),
                    new Claim(ClaimTypes.Name, loginViewModel.NomeUsuario!)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                { };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("NotificacaoLogado");
            }
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return View("Login");
    }

    [HttpGet]
    public IActionResult NotificacaoLogado()
    {
        return View();
    }
}
