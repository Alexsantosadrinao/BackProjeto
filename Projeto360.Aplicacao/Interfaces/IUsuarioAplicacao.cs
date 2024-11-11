using Projeto360.Dominio.Entidades;

namespace Projeto360.Aplicacao;

public interface IUsuarioAplicacao
{
    Task<int> Criar(Usuario usuario);
    Task AtualizarSenha(Usuario usuario, string novaSenha);
    Task Atualizar(Usuario usuario);
    Task Deletar(int UsuarioId);
    Task Restaurar(int UsuarioId);
    Task<List<Usuario>> Listar(bool ativo);
    Task<Usuario> Obter(int UsuarioId);
    Task<Usuario> ObterEmail(string email);
    Task<List<int>> ListaValoresTiposUsuarios();
    Task<List<string>> ListaNomesTiposUsuario();
}