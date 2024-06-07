using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.EntityConfiguration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.UsuarioID);

        builder
            .Property(u => u.UsuarioID)
            .IsRequired()
            .HasColumnType("INT");

        builder
            .HasIndex(u => u.NomeUsuario)
            .IsUnique();

        builder
            .Property(u => u.NomeUsuario)
            .IsRequired()
            .HasColumnType("VARCHAR(45)");

        builder
            .Property(u => u.Senha)
            .IsRequired()
            .HasColumnType("VARCHAR(200)");

        builder
            .Property(u => u.DataCadastro)
            .HasDefaultValueSql("CURRENT_TIMESTAMP()")
            .IsRequired()
            .HasColumnType("DATETIME");

        builder
            .HasMany(usuario => usuario.Caixas)
            .WithOne(caixa => caixa.Usuario)
            .HasForeignKey(caixa => caixa.UsuarioID);
    }
}
