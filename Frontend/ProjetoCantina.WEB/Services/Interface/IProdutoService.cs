using ProjetoCantina.WEB.Models;

namespace ProjetoCantina.WEB.Services.Interface;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>?> GetAllProdutosAsync();
    Task<ProdutoViewModel?> GetProdutoByIDAsync(int produtoID);
    Task<bool> AdicionarProdutoAsync(ProdutoViewModel produto);
    Task<bool> UpdateProdutoAsync(ProdutoViewModel produto);
    Task<bool> DeleteProdutoAsync(int produtoID);
}
