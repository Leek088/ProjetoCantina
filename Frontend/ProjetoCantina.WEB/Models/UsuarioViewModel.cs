using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.WEB.Models;

public class UsuarioViewModel
{
    public int UsuarioID { get; set; }

    [Required, StringLength(45)]
    public string? NomeUsuario { get; set; }

    [Required, StringLength(200)]
    [DataType(DataType.Password)]
    public string? Senha { get; set; }

    [DataType(DataType.Password)]
    [Compare("Senha")]
    public string? ConfirmarSenha { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    //public virtual ICollection<CaixaDTO>? Caixas { get; set; }
}
