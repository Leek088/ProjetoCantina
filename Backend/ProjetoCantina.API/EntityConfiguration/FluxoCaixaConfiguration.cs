using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.EntityConfiguration
{
    public class FluxoCaixaConfiguration : IEntityTypeConfiguration<FluxoCaixa>
    {
        public void Configure(EntityTypeBuilder<FluxoCaixa> builder)
        {
            builder.HasKey(fc => fc.FluxoCaixaID);

            builder
                .Property(fc => fc.FluxoCaixaID)
                .IsRequired()
                .HasColumnType("INT");

            builder
                .Property(fc => fc.CaixaID)
                .IsRequired()
                .HasColumnType("INT");

            builder
                .Property(fc => fc.UsuarioID)
                .IsRequired()
                .HasColumnType("INT");

            builder
                .Property(fc => fc.ValorAbertura)
                .IsRequired()
                .HasColumnType("NUMERIC(10,2)");

            builder
                .Property(fc => fc.DataAbertura)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP()")
                .HasColumnType("DATETIME");

            builder
               .Property(fc => fc.ValorFechamento)
               .IsRequired()
               .HasColumnType("NUMERIC(10,2)");

            builder
               .Property(fc => fc.DataFechamento)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP()")
               .HasColumnType("DATETIME");

            builder
                .Property(fc => fc.CaixaFechado)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(fc => fc.Caixa)
                .WithOne(c => c.FluxoCaixa);                
        }
    }
}
