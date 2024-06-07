using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Repositories.Interface;

namespace ProjetoCantina.API.Repositories.Repository;

public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
