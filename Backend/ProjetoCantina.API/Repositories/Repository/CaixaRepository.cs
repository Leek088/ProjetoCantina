using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Repositories.Interface;

namespace ProjetoCantina.API.Repositories.Repository;

public class CaixaRepository : GenericRepository<Caixa>, ICaixaRepository
{
    public CaixaRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
