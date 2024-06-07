using ProjetoCantina.API.DTOs;

namespace ProjetoCantina.API.Services.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>?> GetAllCategoriasAsync();
    Task<IEnumerable<CategoriaDTO>?> GetAllCategoriasComProdutosAsync();
    Task<CategoriaDTO?> GetCategoriaByIdAsync(int categoriaID);
    Task<CategoriaDTO?> GetCategoriaComProdutoByIdAsync(int categoriaID);
    Task<bool> InsertCategoriaAsync(CategoriaDTO categoriaDTO);
    Task<bool> UpdateCategoriaAsync(CategoriaDTO categoriaDTO);
    Task<bool> DeleteCategoriaAsync(int categoriaID);
}
