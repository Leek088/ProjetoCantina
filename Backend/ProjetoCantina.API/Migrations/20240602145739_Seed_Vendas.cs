using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCantina.API.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Vendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO venda (CaixaID, UsuarioID, ProdutoID, Quantidade) VALUES " +
              "((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-a37f-ec95f7bb81a7'), " +
              "(Select UsuarioID From usuario Where NomeUsuario = 'Admin'), " +
              "(Select ProdutoID From produto Where Nome = 'Coxinha'), 1);");
            
            mb.Sql("INSERT INTO venda (CaixaID, UsuarioID, ProdutoID, Quantidade) VALUES " +
              "((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-a37f-ec95f7bb81a7'), " +
              "(Select UsuarioID From usuario Where NomeUsuario = 'Admin'), " +
              "(Select ProdutoID From produto Where Nome = 'Suco de laranja'), 2);");
            
            mb.Sql("INSERT INTO venda (CaixaID, UsuarioID, ProdutoID, Quantidade) VALUES " +
              "((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-a37f-ec95f7bb81a7'), " +
              "(Select UsuarioID From usuario Where NomeUsuario = 'Admin'), " +
              "(Select ProdutoID From produto Where Nome = 'Bolo de chocolate'), 3);"); 
            
            
            mb.Sql("INSERT INTO venda (CaixaID, UsuarioID, ProdutoID, Quantidade) VALUES " +
              "((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-ec95f7bb81a7-a37f'), " +
              "(Select UsuarioID From usuario Where NomeUsuario = 'User'), " +
              "(Select ProdutoID From produto Where Nome = 'Coxinha'), 1);");
            
            mb.Sql("INSERT INTO venda (CaixaID, UsuarioID, ProdutoID, Quantidade) VALUES " +
              "((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-ec95f7bb81a7-a37f'), " +
              "(Select UsuarioID From usuario Where NomeUsuario = 'User'), " +
              "(Select ProdutoID From produto Where Nome = 'Suco de laranja'), 2);");
            
            mb.Sql("INSERT INTO venda (CaixaID, UsuarioID, ProdutoID, Quantidade) VALUES " +
              "((Select CaixaID From caixa Where CodigoUnico = '3132d8e5-522c-4cc0-ec95f7bb81a7-a37f'), " +
              "(Select UsuarioID From usuario Where NomeUsuario = 'User'), " +
              "(Select ProdutoID From produto Where Nome = 'Bolo de chocolate'), 3);");          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete From venda Where VendaID > 0");
        }
    }
}
