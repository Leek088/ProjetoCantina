using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using System.Text.Json;

namespace ProjetoCantina.WEB.Services.Service;

public class UsuarioService : IUsuarioService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string apiEndPoint;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private IEnumerable<UsuarioViewModel>? usuarios;
    private UsuarioViewModel? usuario;

    public UsuarioService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        this.apiEndPoint = configuration["ProjetoCantina.API:UsuarioEndPoint"]!; ;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; ;
    }

    public async Task<IEnumerable<UsuarioViewModel>?> GetAllUsuariosAsync()
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
        usuarios = null;

        using (var response = await httpClientFactory.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                usuarios = await JsonSerializer
                    .DeserializeAsync<IEnumerable<UsuarioViewModel>>(apiResponse, _jsonSerializerOptions);
            }
        }

        return usuarios;
    }
    public async Task<IEnumerable<UsuarioViewModel>?> GetUsuarioByNomeUsuarioAsync(string nomeUsuario)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
        usuarios = null;

        using (var response = await httpClientFactory.GetAsync($"{apiEndPoint}{nomeUsuario}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                usuarios = await JsonSerializer
                    .DeserializeAsync<IEnumerable<UsuarioViewModel>>(apiResponse, _jsonSerializerOptions);
            }
        }

        return usuarios;
    }
    public async Task<bool> AdicionarUsuarioAsync(UsuarioViewModel usuario)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.PostAsJsonAsync(apiEndPoint, usuario))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<UsuarioViewModel?> GetUsuarioByIDAsync(int usuarioID)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
        usuario = null;

        using (var response = await httpClientFactory.GetAsync(apiEndPoint + usuarioID))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                usuario = await JsonSerializer
                    .DeserializeAsync<UsuarioViewModel>(apiResponse, _jsonSerializerOptions);
            }
        }

        return usuario;
    }

    public async Task<bool> UpdateUsuarioAsync(UsuarioViewModel usuario)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.PutAsJsonAsync(apiEndPoint, usuario))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<bool> DeleteUsuarioAsync(int usuarioID)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.DeleteAsync(apiEndPoint + usuarioID))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }
}
