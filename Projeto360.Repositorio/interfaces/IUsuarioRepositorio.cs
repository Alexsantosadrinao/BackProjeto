using System.Reflection.Metadata;
using Projeto360.Dominio.Entidades;

public interface IUsuarioRepositorio
{
    Task<int> salvar(Usuario usuario);
    Task Atualizar(Usuario usuario);
    Task<Usuario> Obter(int UsuarioId);
    Task<Usuario> ObterPorEmail(string email);
    Task<List<Usuario>> Listar(bool ativo);
    
}