using ProjetoCantina.API.DTOs;

namespace ProjetoCantina.API.Services.Interfaces;

public interface IVendaService
{
    Task<IEnumerable<VendaDTO>?> GetAllVendasAsync();
    Task<VendaDTO?> GetVendaByIdAsync(int vendaID);
    Task<bool> InsertVendaAsync(VendaDTO vendaDTO);
    Task<bool> UpdateVendaAsync(VendaDTO vendaDTO);
    Task<bool> DeleteVendaAsync(int vendaID);
}
