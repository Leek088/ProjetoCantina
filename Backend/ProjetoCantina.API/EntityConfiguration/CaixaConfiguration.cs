using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.EntityConfiguration;

public class CaixaConfiguration : IEntityTypeConfiguration<Caixa>
{
    public void Configure(EntityTypeBuilder<Caixa> builder)
    {
        builder.HasKey(c => c.CaixaID);

        builder
            .Property(c => c.CaixaID)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .Property(c => c.UsuarioID)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .HasIndex(c => c.CodigoUnico)
            .IsUnique();

        builder
            .Property(c => c.CodigoUnico)
            .IsRequired()
            .HasColumnType("VARCHAR(45)");

        builder
            .Property(c => c.DataAbertura)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP()")
            .HasColumnType("DATETIME");

        builder
            .HasMany(caixa => caixa.Vendas)
            .WithOne(venda => venda.Caixa)
            .HasForeignKey(venda => venda.CaixaID);
    }
}
