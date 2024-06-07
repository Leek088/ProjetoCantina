using ProjetoCantina.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.API.DTOs;

public class VendaDTO
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

    public ProdutoDTO? Produto { get; set; }
    public CaixaDTO? Caixa { get; set; }
}
