namespace ProjetoCantina.API.Models;

public class FluxoCaixa
{
    public FluxoCaixa(int fluxoCaixaID, int caixaID, int usuarioID, decimal valorAbertura, 
        DateTime dataAbertura, decimal valorFechamento, DateTime dataFechamento, bool caixaFechado)
    {
        FluxoCaixaID = fluxoCaixaID;
        CaixaID = caixaID;
        UsuarioID = usuarioID;
        ValorAbertura = valorAbertura;
        DataAbertura = dataAbertura;
        ValorFechamento = valorFechamento;
        DataFechamento = dataFechamento;
        CaixaFechado = caixaFechado;
    }

    public int FluxoCaixaID { get; private set; }
    public int CaixaID { get; private set; }
    public int UsuarioID { get; private set; }
    public decimal ValorAbertura { get; private set; }
    public DateTime DataAbertura { get; private set; }
    public decimal ValorFechamento { get; private set; }
    public DateTime DataFechamento { get; private set; }
    public bool CaixaFechado { get; private set; }

    public Caixa? Caixa {  get; set; }
}
