using ProjetoCantina.WEB.Models;

namespace ProjetoCantina.WEB.Services.Interface;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioViewModel>?> GetAllUsuariosAsync();
    Task<UsuarioViewModel?> GetUsuarioByIDAsync(int usuarioID);
    Task<IEnumerable<UsuarioViewModel>?> GetUsuarioByNomeUsuarioAsync(string nomeUsuario);
    Task<bool> AdicionarUsuarioAsync(UsuarioViewModel Usuario);
    Task<bool> UpdateUsuarioAsync(UsuarioViewModel Usuario);
    Task<bool> DeleteUsuarioAsync(int usuarioID);
}
