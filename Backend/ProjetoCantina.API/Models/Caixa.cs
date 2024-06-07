namespace ProjetoCantina.API.Models;

public class Caixa
{
    public Caixa(int caixaID, string codigoUnico, DateTime dataAbertura, int usuarioID)
    {
        CaixaID = caixaID;
        UsuarioID = usuarioID;
        CodigoUnico = codigoUnico;
        DataAbertura = dataAbertura;

        this.Vendas = new HashSet<Venda>();
    }

    public int CaixaID { get; private set; }
    public int UsuarioID { get; private set; }
    public string CodigoUnico { get; private set; }
    public DateTime DataAbertura { get; private set; }

    public Usuario? Usuario { get; set; }
    public virtual ICollection<Venda> Vendas { get; private set; }
    public FluxoCaixa? FluxoCaixa { get; set; }
}
