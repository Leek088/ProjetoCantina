using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using System.Text.Json;

namespace ProjetoCantina.WEB.Services.Service;

public class CategoriaService : ICategoriaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string apiEndPoint;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private IEnumerable<CategoriaViewModel>? categorias;
    private CategoriaViewModel? categoria;

    public CategoriaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        this.apiEndPoint = configuration["ProjetoCantina.API:CategoriaEndPoint"]!;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<CategoriaViewModel>?> GetAllCategoriasAsync()
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
        categorias = null;

        using (var response = await httpClientFactory.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                categorias = await JsonSerializer
                    .DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _jsonSerializerOptions);
            }
        }

        return categorias;
    }

    public async Task<bool> CreateCategoriaAsync(CategoriaViewModel categoriaViewModel)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.PostAsJsonAsync(apiEndPoint, categoriaViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<CategoriaViewModel?> GetCategoriaByIDAsync(int categoriaID)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
        categoria = null;

        using (var response = await httpClientFactory.GetAsync(apiEndPoint + categoriaID))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                categoria = await JsonSerializer
                    .DeserializeAsync<CategoriaViewModel>(apiResponse, _jsonSerializerOptions);
            }
        }

        return categoria;
    }

    public async Task<bool> UpdateCategoriaAsync(CategoriaViewModel categoria)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.PutAsJsonAsync(apiEndPoint, categoria))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<bool> DeleteCategoriaAsync(int categoriaID)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.DeleteAsync(apiEndPoint + categoriaID))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }
}
