using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCantina.API.Migrations
{
    /// <inheritdoc />
    public partial class Seed_FluxosCaixas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert Into fluxocaixa (CaixaID, UsuarioID, ValorAbertura, ValorFechamento, CaixaFechado) " +
                "VALUES((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-a37f-ec95f7bb81a7'), " +
                "(Select UsuarioID from usuario Where NomeUsuario = 'Admin'), 10.0, 0, 0);");
            
            mb.Sql("Insert Into fluxocaixa (CaixaID, UsuarioID, ValorAbertura, ValorFechamento, CaixaFechado) " +
                "VALUES((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-ec95f7bb81a7-a37f'), " +
                "(Select UsuarioID from usuario Where NomeUsuario = 'User'), 15.0, 0, 0);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete From fluxoCaixa Where FluxoCaixaID > 0");
        }
    }
}
