using System.ComponentModel.DataAnnotations;

namespace ProjetoCantina.API.DTOs;

public class LoginUsuarioDTO
{
    [Required, StringLength(45)]
    public string? NomeUsuario { get; set; }

    [Required, StringLength(200)]
    [DataType(DataType.Password)]
    public string? Senha { get; set; }
}
