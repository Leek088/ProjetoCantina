using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCantina.API.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Categorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO categoria (Nome) VALUES ('Bebidas')");
            mb.Sql("INSERT INTO categoria (Nome) VALUES ('Salgados')");
            mb.Sql("INSERT INTO categoria (Nome) VALUES ('Doces')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM categoria WHERE CategoriaID > 0");
        }
    }
}
