using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using System.Text.Json;

namespace ProjetoCantina.WEB.Services.Service;

public class FluxoCaixaService : IFluxoCaixaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string apiEndPoint;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private IEnumerable<FluxoCaixaViewModel>? fluxoCaixas;
    private FluxoCaixaViewModel? fluxoCaixa;

    public FluxoCaixaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        this.apiEndPoint = configuration["ProjetoCantina.API:FluxoCaixaEndPoint"]!;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public Task<IEnumerable<FluxoCaixaViewModel>?> GetAllFluxoCaixaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateFluxoCaixaAsync(FluxoCaixaViewModel fluxoCaixa)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFluxoCaixaAsync(int fluxoCaixaID)
    {
        throw new NotImplementedException();
    }

    public Task<FluxoCaixaViewModel?> GetFluxoCaixaByIDAsync(int fluxoCaixaID)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateFluxoCaixaAsync(FluxoCaixaViewModel fluxoCaixa)
    {
        throw new NotImplementedException();
    }
}
