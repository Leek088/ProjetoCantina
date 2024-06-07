using ProjetoCantina.API.DTOs;

namespace ProjetoCantina.API.Services.Interfaces
{
    public interface IFluxoCaixaService
    {
        Task<IEnumerable<FluxoCaixaDTO>?> GetAllFluxosCaixasAsync();
        Task<FluxoCaixaDTO?> GetFluxoCaixaByIdAsync(int fluxoCaixaID);
        Task<bool> InsertFluxoCaixaAsync(FluxoCaixaDTO fluxoCaixaDTO);
        Task<bool> UpdateFluxoCaixaAsync(FluxoCaixaDTO fluxoCaixaDTO);
        Task<bool> DeleteFluxoCaixaAsync(int fluxoCaixaID);
    }
}
