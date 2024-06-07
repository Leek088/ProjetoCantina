using ProjetoCantina.WEB.Models;

namespace ProjetoCantina.WEB.Services.Interface
{
    public interface IFluxoCaixaService
    {
        Task<IEnumerable<FluxoCaixaViewModel>?> GetAllFluxoCaixaAsync();
        Task<FluxoCaixaViewModel?> GetFluxoCaixaByIDAsync(int fluxoCaixaID);
        Task<bool> CreateFluxoCaixaAsync(FluxoCaixaViewModel fluxoCaixa);
        Task<bool> UpdateFluxoCaixaAsync(FluxoCaixaViewModel fluxoCaixa);
        Task<bool> DeleteFluxoCaixaAsync(int fluxoCaixaID);
    }
}
