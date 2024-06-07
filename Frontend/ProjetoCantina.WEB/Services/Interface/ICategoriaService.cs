using ProjetoCantina.WEB.Models;

namespace ProjetoCantina.WEB.Services.Interface;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaViewModel>?> GetAllCategoriasAsync();
    Task<CategoriaViewModel?> GetCategoriaByIDAsync(int categoriaID);
    Task<bool> CreateCategoriaAsync(CategoriaViewModel categoria);
    Task<bool> UpdateCategoriaAsync(CategoriaViewModel categoria);
    Task<bool> DeleteCategoriaAsync(int categoriaID);
}
