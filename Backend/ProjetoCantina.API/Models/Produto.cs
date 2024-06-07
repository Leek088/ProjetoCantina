namespace ProjetoCantina.API.Models;

public class Produto
{
    public Produto(int produtoID, string nome, decimal precoVenda, int estoque,
        DateTime dataCadastro, string codigoBarras, int categoriaID)
    {
        ProdutoID = produtoID;
        CategoriaID = categoriaID;
        Nome = nome;
        CodigoBarras = codigoBarras;
        PrecoVenda = precoVenda;
        Estoque = estoque;
        DataCadastro = dataCadastro;

        this.Vendas = new HashSet<Venda>();
    }

    public int ProdutoID { get; private set; }
    public int CategoriaID { get; private set; }
    public string Nome { get; private set; }
    public string CodigoBarras { get; private set; }
    public decimal PrecoVenda { get; private set; }
    public int Estoque { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public Categoria? Categoria { get; set; }
    public virtual ICollection<Venda> Vendas { get; private set; }
}
