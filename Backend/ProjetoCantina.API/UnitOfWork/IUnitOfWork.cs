using ProjetoCantina.API.Repositories.Interface;

namespace ProjetoCantina.API.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangeAsync();

    ICategoriaRepository CategoriaRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    ICaixaRepository CaixaRepository { get; }
    IFluxoCaixaRepository FluxoCaixaRepository { get; }
    IVendaRepository VendaRepository { get; }
}
