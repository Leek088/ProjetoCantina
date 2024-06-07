namespace ProjetoCantina.API.Models;

public class Usuario
{
    public Usuario(int usuarioID, string nomeUsuario, string senha, DateTime dataCadastro)
    {
        UsuarioID = usuarioID;
        NomeUsuario = nomeUsuario;
        Senha = senha;
        DataCadastro = dataCadastro;

        this.Caixas = new HashSet<Caixa>();
    }

    public int UsuarioID { get; private set; }
    public string NomeUsuario { get; private set; }
    public string Senha { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public ICollection<Caixa> Caixas { get; private set; }
}
