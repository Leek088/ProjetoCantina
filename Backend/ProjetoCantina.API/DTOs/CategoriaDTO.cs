using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.API.DTOs;

public class CategoriaDTO
{
    public int CategoriaID { get; set; }
    
    [Required, StringLength(45)]
    public string? Nome { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public virtual ICollection<ProdutoDTO>? Produtos { get; set; }
}
