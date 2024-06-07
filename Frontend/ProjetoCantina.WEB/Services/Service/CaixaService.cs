using ProjetoCantina.WEB.Models;
using ProjetoCantina.WEB.Services.Interface;
using System.Text.Json;

namespace ProjetoCantina.WEB.Services.Service
{
    public class CaixaService : ICaixaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiEndPoint;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private IEnumerable<CaixaViewModel>? caixas;
        private CaixaViewModel? caixa;

        public CaixaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            this.apiEndPoint = configuration["ProjetoCantina.API:CaixaEndPoint"]!;
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<CaixaViewModel>?> GetAllCaixasComUsuarioAsync()
        {
            var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
            caixas = null;

            using (var response = await httpClientFactory.GetAsync(apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    caixas = await JsonSerializer
                        .DeserializeAsync<IEnumerable<CaixaViewModel>>(apiResponse, _jsonSerializerOptions);
                }
            }

            return caixas;
        }

        public async Task<CaixaViewModel?> GetcaixaByIDAsync(int caixaID)
        {
            var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
            caixa = null;

            using (var response = await httpClientFactory.GetAsync(apiEndPoint + caixaID))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    caixa = await JsonSerializer
                        .DeserializeAsync<CaixaViewModel>(apiResponse, _jsonSerializerOptions);
                }
            }

            return caixa;
        }

        public async Task<CaixaViewModel?> GetcaixaByCodigoUnicoAsync(string codigoUnico)
        {
            var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");
            caixa = null;

            using (var response = await httpClientFactory.GetAsync(apiEndPoint + codigoUnico))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    caixa = await JsonSerializer
                        .DeserializeAsync<CaixaViewModel>(apiResponse, _jsonSerializerOptions);
                }
            }

            return caixa;
        }

        public async Task<bool> CreateCaixaAsync(CaixaViewModel caixa)
        {
            var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

            using (var response = await httpClientFactory.PostAsJsonAsync(apiEndPoint, caixa))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateCaixaAsync(CaixaViewModel caixa)
        {
            var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

            using (var response = await httpClientFactory.PutAsJsonAsync(apiEndPoint, caixa))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> DeleteCaixaAsync(int caixaID)
        {
            var httpClientFactory = _httpClientFactory.CreateClient("ProjetoCantina.API");

            using (var response = await httpClientFactory.DeleteAsync(apiEndPoint + caixaID))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }        
    }
}
