using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Repositories.Interface;

namespace ProjetoCantina.API.Repositories.Repository;

public class FluxoCaixaRepository : GenericRepository<FluxoCaixa>, IFluxoCaixaRepository
{
    public FluxoCaixaRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
