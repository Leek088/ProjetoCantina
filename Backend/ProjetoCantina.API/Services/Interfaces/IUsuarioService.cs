using ProjetoCantina.API.DTOs;

namespace ProjetoCantina.API.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>?> GetAllUsuariosAsync();
    Task<IEnumerable<UsuarioDTO>?> GetAllUsuariosComCaixasAsync();
    Task<UsuarioDTO?> GetUsuarioByIdAsync(int usuarioID);
    Task<UsuarioDTO?> GetUsuarioComCaixaByIdAsync(int usuarioID);
    Task<IEnumerable<UsuarioDTO>?> GetUsuarioByNomeUsuarioAsync(string nomeUsuario);
    Task<IEnumerable<UsuarioDTO>?> GetUsuarioComCaixasByNomeUsuarioAsync(string nomeUsuario);
    Task<bool> ValidarLoginUsuarioAsync(LoginUsuarioDTO loginUsuarioDTO);
    Task<bool> InsertUsuarioaAsync(UsuarioDTO usuarioDTO);
    Task<bool> UpdateUsuarioAsync(UsuarioDTO usuarioDTO);
    Task<bool> DeleteUsuarioAsync(int UsuarioID);
}
