using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Repositories.Interface;

namespace ProjetoCantina.API.Repositories.Repository;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
