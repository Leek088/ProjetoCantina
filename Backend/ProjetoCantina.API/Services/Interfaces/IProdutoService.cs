using ProjetoCantina.API.DTOs;

namespace ProjetoCantina.API.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDTO>> GetAllProdutosAsync();
    Task<IEnumerable<ProdutoDTO>> GetAllProdutosComCategoriaAsync();
    Task<IEnumerable<ProdutoDTO>> GetAllProdutosComVendasAsync();
    Task<IEnumerable<ProdutoDTO>?> GetAllProdutosComCategoriaVendasAsync();
    Task<ProdutoDTO?> GetProdutoByIdAsync(int produtoID);
    Task<ProdutoDTO?> GetProdutoComCategoriaByIdAsync(int produtoID);
    Task<ProdutoDTO?> GetProdutoComVendasByIdAsync(int produtoID);
    Task<ProdutoDTO?> GetProdutoComCategoriaVendasByIdAsync(int produtoID);
    Task<IEnumerable<ProdutoDTO>?> GetProdutoByNomeAsync(string nomeProduto);
    Task<IEnumerable<ProdutoDTO>?> GetProdutoComCategoriaByNomeAsync(string nomeProduto);
    Task<IEnumerable<ProdutoDTO>?> GetProdutoComVendasByNomeAsync(string nomeProduto);
    Task<IEnumerable<ProdutoDTO>?> GetProdutoComCategoriaVendasByNomeAsync(string nomeProduto);
    Task<bool> InsertProdutoAsync(ProdutoDTO produtoDTO);
    Task<bool> UpdatePrdoutoAsync(ProdutoDTO produtoDTO);
    Task<bool> DeleteProdutoAsync(int produtoID);
}
