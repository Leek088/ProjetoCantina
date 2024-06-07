using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using System.Text.Json;

namespace ProjetoCantina.WEB.Services.Service;

public class ProdutoService : IProdutoService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string apiEndPoint;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private IEnumerable<ProdutoViewModel>? produtos;
    private ProdutoViewModel? produto;

    public ProdutoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        apiEndPoint = configuration["ProjetoCantina.API:ProdutoEndPoint"]!;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<IEnumerable<ProdutoViewModel>?> GetAllProdutosAsync()
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
        produtos = null;

        using (var response = await httpClientFactory.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtos = await JsonSerializer
                    .DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _jsonSerializerOptions);
            }
        }

        return produtos;
    }
    public async Task<ProdutoViewModel?> GetProdutoByIDAsync(int produtoID)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
        produto = null;

        using (var response = await httpClientFactory.GetAsync(apiEndPoint + produtoID))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produto = await JsonSerializer
                    .DeserializeAsync<ProdutoViewModel>(apiResponse, _jsonSerializerOptions);
            }
        }

        return produto;
    }

    public async Task<bool> AdicionarProdutoAsync(ProdutoViewModel produto)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.PostAsJsonAsync(apiEndPoint, produto))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<bool> UpdateProdutoAsync(ProdutoViewModel produto)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.PutAsJsonAsync(apiEndPoint, produto))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<bool> DeleteProdutoAsync(int produtoID)
    {
        var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

        using (var response = await httpClientFactory.DeleteAsync(apiEndPoint + produtoID))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }
}
