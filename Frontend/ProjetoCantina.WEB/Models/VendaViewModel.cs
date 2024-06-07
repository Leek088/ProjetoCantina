using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.WEB.Models;

public class VendaViewModel
{
    public int VendaID { get; set; }

    [Required]
    public int CaixaID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required]
    public int ProdutoID { get; set; }

    [Required]
    public int Quantidade { get; set; }

    public DateTime DataVenda { get; set; } = DateTime.Now;

    public ProdutoViewModel? Produto { get; set; }
    public CaixaViewModel? Caixa { get; set; }
}
