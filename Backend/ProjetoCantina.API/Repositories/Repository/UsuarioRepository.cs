using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Repositories.Interface;

namespace ProjetoCantina.API.Repositories.Repository;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
