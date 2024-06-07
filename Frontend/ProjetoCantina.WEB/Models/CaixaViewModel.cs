using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.WEB.Models;

public class CaixaViewModel
{
    public int CaixaID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required, StringLength(45)]
    public string? CodigoUnico { get; set; }

    public DateTime DataAbertura { get; set; } = DateTime.Now;

    public UsuarioViewModel? Usuario { get; set; }
    public virtual ICollection<VendaViewModel>? Vendas { get; set; }
    public FluxoCaixaViewModel? FluxoCaixa { get; set; }
}
