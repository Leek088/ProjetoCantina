using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using System.Text.Json;

namespace ProjetoCantina.WEB.Services.Service;

public class LoginService : ILoginService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string apiEndPoint;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public LoginService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        this.apiEndPoint = configuration["ProjetoCantina.API:LoginEndPoint"]!;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<bool> LogarNoSite(LoginViewModel loginViewModel)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.PostAsJsonAsync(apiEndPoint, loginViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }
}
