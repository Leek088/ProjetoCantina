using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCantina.API.DTOs;

public class FluxoCaixaDTO
{
    public int FluxoCaixaID { get; set; }

    [Required]
    public int CaixaID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required, Column(TypeName = "decimal(10,2)"), Range(0.1, 9999)]
    public decimal ValorAbertura { get; set; }

    public DateTime DataAbertura { get; set; } = DateTime.Now;

    [Required, Column(TypeName = "decimal(10,2)"), Range(0, 9999)]
    public decimal ValorFechamento { get; set; }

    public DateTime DataFechamento { get; set; } = DateTime.Now;

    public bool CaixaFechado { get; set; } = false;

    public CaixaDTO? Caixa { get; set; }
}
