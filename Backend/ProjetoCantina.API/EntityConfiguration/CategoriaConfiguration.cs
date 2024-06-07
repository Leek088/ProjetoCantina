using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.EntityConfiguration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.CategoriaID);

            builder
                .Property(c => c.CategoriaID)
                .IsRequired()
                .HasColumnType("INT");

            builder
                .Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(45)");

            builder
                .Property(c => c.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP()")
                .HasColumnType("DATETIME");

            builder
                .HasMany(categoria => categoria.Produtos)
                .WithOne(produto => produto.Categoria)
                .HasForeignKey(produto => produto.CategoriaID);
        }
    }
}
