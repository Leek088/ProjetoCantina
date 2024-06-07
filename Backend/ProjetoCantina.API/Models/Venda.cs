namespace ProjetoCantina.API.Models;

public class Venda
{
    public Venda(int vendaID, int caixaID, int produtoID, int quantidade, DateTime dataVenda, int usuarioID)
    {
        VendaID = vendaID;
        CaixaID = caixaID;
        UsuarioID = usuarioID;
        ProdutoID = produtoID;
        Quantidade = quantidade;
        DataVenda = dataVenda;
    }

    public int VendaID { get; private set; }
    public int CaixaID { get; private set; }
    public int UsuarioID { get; private set; }
    public int ProdutoID { get; private set; }
    public int Quantidade { get; private set; }
    public DateTime DataVenda { get; private set; }

    public Produto? Produto { get; set; }
    public Caixa? Caixa { get; set; }
}
