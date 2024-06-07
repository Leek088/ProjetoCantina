using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.API.DTOs;

public class CaixaDTO
{
    public int CaixaID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required, StringLength(45)]
    public string? CodigoUnico { get; set; }

    public DateTime DataAbertura { get; set; } = DateTime.Now;

    public UsuarioDTO? Usuario { get; set; }
    public virtual ICollection<VendaDTO>? Vendas { get; set; }
    public FluxoCaixaDTO? FluxoCaixa { get; set; }
}
