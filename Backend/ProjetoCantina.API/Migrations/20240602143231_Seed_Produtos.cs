using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCantina.API.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Produtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO produto (CategoriaID, Nome, CodigoBarras, PrecoVenda, Estoque) " +
                "VALUES ((Select CategoriaID From categoria Where Nome = 'Salgados'),'Coxinha', '10203040', 6.0, 5);");
            
            mb.Sql("INSERT INTO produto (CategoriaID, Nome, CodigoBarras, PrecoVenda, Estoque) " +
                "VALUES ((Select CategoriaID From categoria Where Nome = 'Bebidas'), 'Suco de laranja', '50607080', 7.0, 6);");
            
            mb.Sql("INSERT INTO produto (CategoriaID, Nome, CodigoBarras, PrecoVenda, Estoque) " +
                "VALUES ((Select CategoriaID From categoria Where Nome = 'Doces'), 'Bolo de chocolate', '50609010', 6.5, 7);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM produto WHERE ProdutoID > 0");
        }
    }
}
