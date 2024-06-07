using ProjetoCantina.WEB.Models;

namespace ProjetoCantina.WEB.Services.Interface;

public interface ILoginService
{
    Task<bool> LogarNoSite(LoginViewModel loginViewModel);
}
