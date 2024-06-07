using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Repositories.Interface;

namespace ProjetoCantina.API.Repositories.Repository;

public class VendaRepository : GenericRepository<Venda>, IVendaRepository
{
    public VendaRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
