using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.EntityConfiguration;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.HasKey(v => v.VendaID);

        builder
            .Property(v => v.VendaID)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .Property(v => v.CaixaID)
            .IsRequired()
            .HasColumnType("INT");
        
        builder
            .Property(v => v.UsuarioID)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .Property(v => v.ProdutoID)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .Property(v => v.Quantidade)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .Property(v => v.DataVenda)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP()")
            .HasColumnType("DATETIME");
    }
}
