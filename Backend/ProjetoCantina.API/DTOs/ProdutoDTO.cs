using ProjetoCantina.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCantina.API.DTOs;

public class ProdutoDTO
{
    public int ProdutoID { get; set; }

    [Required]
    public int CategoriaID { get; set; }

    [Required, StringLength(50)]
    public string? Nome { get; set; }

    [Required, StringLength(50)]
    public string? CodigoBarras { get; set; }

    [Required, Column(TypeName = "decimal(10,2)"), Range(0.1, 9999)]
    public decimal PrecoVenda { get; set; }

    [Required, Range(1, 9999)]
    public int Estoque { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public CategoriaDTO? Categoria { get; set; }
    public virtual ICollection<VendaDTO>? Vendas { get; set; }
}
