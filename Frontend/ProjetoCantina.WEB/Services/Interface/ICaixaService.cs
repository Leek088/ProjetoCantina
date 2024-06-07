using ProjetoCantina.WEB.Models;

namespace ProjetoCantina.WEB.Services.Interface;

public interface ICaixaService
{
    Task<IEnumerable<CaixaViewModel>?> GetAllCaixasComUsuarioAsync();
    Task<CaixaViewModel?> GetcaixaByIDAsync(int caixaID);
    Task<CaixaViewModel?> GetcaixaByCodigoUnicoAsync(string codigoUnico);
    Task<bool> CreateCaixaAsync(CaixaViewModel caixa);
    Task<bool> UpdateCaixaAsync(CaixaViewModel caixa);
    Task<bool> DeleteCaixaAsync(int caixaID);
}
