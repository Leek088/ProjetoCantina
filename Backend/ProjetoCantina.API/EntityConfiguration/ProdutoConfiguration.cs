using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.EntityConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.ProdutoID);

        builder
            .Property(p => p.ProdutoID)
            .IsRequired()
            .HasColumnType("INT");
        
        builder
            .Property(p => p.CategoriaID)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");

        builder
            .HasIndex(c => c.CodigoBarras)
            .IsUnique();

        builder
            .Property(c => c.CodigoBarras)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");

        builder
            .Property(p => p.PrecoVenda)
            .IsRequired()
            .HasColumnType("NUMERIC(10,2)");

        builder
            .Property(p => p.Estoque)
            .IsRequired()            
            .HasColumnType("INT");
        
        builder
            .Property(p => p.DataCadastro)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP()")
            .HasColumnType("DATETIME");

        builder
            .HasMany(produto => produto.Vendas)
            .WithOne(venda => venda.Produto)
            .HasForeignKey(venda => venda.ProdutoID);
    }
}
