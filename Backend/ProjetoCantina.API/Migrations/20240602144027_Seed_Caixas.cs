using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCantina.API.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Caixas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert Into caixa (UsuarioID, CodigoUnico) " +
                "Value ((Select UsuarioID From usuario Where NomeUsuario = 'Admin'), '3132d8e5-522c-4cc0-a37f-ec95f7bb81a7')");

            mb.Sql("Insert Into caixa (UsuarioID, CodigoUnico) " +
                "Value ((Select UsuarioID From usuario Where NomeUsuario = 'User'), '3132d8e5-522c-4cc0-ec95f7bb81a7-a37f')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete From caixa Where CaixaID > 0");
        }
    }
}
