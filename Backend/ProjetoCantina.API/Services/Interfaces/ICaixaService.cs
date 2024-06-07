using ProjetoCantina.API.DTOs;

namespace ProjetoCantina.API.Services.Interfaces;

public interface ICaixaService
{
    Task<IEnumerable<CaixaDTO>?> GetAllCaixasAsync();
    Task<CaixaDTO?> GetCaixaByIdAsync(int caixaID);
    Task<CaixaDTO?> GetCaixaByCodigoUnicoAsync(string codigoUnico);
    Task<bool> InsertCaixaAsync(CaixaDTO caixaDTO);
    Task<bool> UpdateCaixaAsync(CaixaDTO caixaDTO);
    Task<bool> DeleteCaixaAsync(int caixaID);
}
