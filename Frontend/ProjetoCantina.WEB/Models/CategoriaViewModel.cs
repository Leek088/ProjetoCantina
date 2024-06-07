using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.WEB.Models;

public class CategoriaViewModel
{
    public int CategoriaID { get; set; }

    [Required, StringLength(45)]
    public string? Nome { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public virtual ICollection<ProdutoViewModel>? Produtos { get; set; }
}
