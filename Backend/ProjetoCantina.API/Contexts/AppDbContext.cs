using Microsoft.EntityFrameworkCore;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

    public DbSet<Produto> Produto { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Venda> Venda { get; set; }
    public DbSet<Caixa> Caixa { get; set; }
    public DbSet<FluxoCaixa> FluxoCaixa { get; set; }
    public DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .EnableDetailedErrors(true); //Mostra os erros detalhados
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
