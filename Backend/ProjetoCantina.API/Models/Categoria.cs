namespace ProjetoCantina.API.Models;

public class Categoria
{
    public Categoria(int categoriaID, string nome, DateTime dataCadastro)
    {
        CategoriaID = categoriaID;
        Nome = nome;
        DataCadastro = dataCadastro;

        this.Produtos = new HashSet<Produto>();
    }

    public int CategoriaID { get; private set; }
    public string Nome { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public virtual ICollection<Produto> Produtos { get; private set; }
}
