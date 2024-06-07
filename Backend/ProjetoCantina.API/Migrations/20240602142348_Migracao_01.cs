using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCantina.API.Migrations
{
    /// <inheritdoc />
    public partial class Migracao_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "VARCHAR(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCadastro = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeUsuario = table.Column<string>(type: "VARCHAR(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "VARCHAR(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCadastro = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ProdutoID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoriaID = table.Column<int>(type: "INT", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoBarras = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrecoVenda = table.Column<decimal>(type: "NUMERIC(10,2)", nullable: false),
                    Estoque = table.Column<int>(type: "INT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ProdutoID);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    CaixaID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioID = table.Column<int>(type: "INT", nullable: false),
                    CodigoUnico = table.Column<string>(type: "VARCHAR(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataAbertura = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.CaixaID);
                    table.ForeignKey(
                        name: "FK_Caixa_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FluxoCaixa",
                columns: table => new
                {
                    FluxoCaixaID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CaixaID = table.Column<int>(type: "INT", nullable: false),
                    UsuarioID = table.Column<int>(type: "INT", nullable: false),
                    ValorAbertura = table.Column<decimal>(type: "NUMERIC(10,2)", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP()"),
                    ValorFechamento = table.Column<decimal>(type: "NUMERIC(10,2)", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP()"),
                    CaixaFechado = table.Column<ulong>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoCaixa", x => x.FluxoCaixaID);
                    table.ForeignKey(
                        name: "FK_FluxoCaixa_Caixa_CaixaID",
                        column: x => x.CaixaID,
                        principalTable: "Caixa",
                        principalColumn: "CaixaID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    VendaID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CaixaID = table.Column<int>(type: "INT", nullable: false),
                    UsuarioID = table.Column<int>(type: "INT", nullable: false),
                    ProdutoID = table.Column<int>(type: "INT", nullable: false),
                    Quantidade = table.Column<int>(type: "INT", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.VendaID);
                    table.ForeignKey(
                        name: "FK_Venda_Caixa_CaixaID",
                        column: x => x.CaixaID,
                        principalTable: "Caixa",
                        principalColumn: "CaixaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venda_Produto_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_CodigoUnico",
                table: "Caixa",
                column: "CodigoUnico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioID",
                table: "Caixa",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoCaixa_CaixaID",
                table: "FluxoCaixa",
                column: "CaixaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaID",
                table: "Produto",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CodigoBarras",
                table: "Produto",
                column: "CodigoBarras",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NomeUsuario",
                table: "Usuario",
                column: "NomeUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venda_CaixaID",
                table: "Venda",
                column: "CaixaID");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_ProdutoID",
                table: "Venda",
                column: "ProdutoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FluxoCaixa");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
