using System.Reflection.Metadata;
using Projeto360.Dominio.Entidades;
using Projeto360.Dominio.Enumeradores;

namespace Projeto360.Aplicacao;

public class UsuarioAplicacao : IUsuarioAplicacao
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public    UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    public async Task<int> Criar(Usuario usuario)
    {
        if (usuario == null)
            throw new Exception("Usuario nao pode ser vazio");

        if (string.IsNullOrEmpty(usuario.Senha))
            throw new Exception("A senha nao pode ser vazia");

        ValidarInformacoesUsuario(usuario);

        
        return await _usuarioRepositorio.salvar(usuario);
    }
    public async Task Atualizar(Usuario usuario)
    {
        var usuarioDominio = await _usuarioRepositorio.Obter(usuario.ID);

        if (usuarioDominio == null)
            throw new Exception("Usuario nao econtrado");

         ValidarInformacoesUsuario(usuario);

         usuarioDominio.Nome = usuario.Nome;
         usuarioDominio.Email = usuario.Email;

        await _usuarioRepositorio.Atualizar(usuarioDominio);
    }
    public async Task AtualizarSenha(Usuario usuario, string novaSenha)
    {
        var usuarioDominio =  await _usuarioRepositorio.Obter(usuario.ID);

        if (usuarioDominio == null)
            throw new Exception("Usuario nao econtrado");

        if (usuarioDominio.Senha != usuario.Senha)
            throw new Exception("Senha antiga invalida");

        if (string.IsNullOrEmpty(novaSenha))
            throw new Exception("A senha nao pode ser vazia");

        usuarioDominio.Senha = novaSenha;

         await _usuarioRepositorio.Atualizar(usuarioDominio);
    }
    public async Task<Usuario> Obter(int UsuarioId)
    {
        var usuarioDominio =  await _usuarioRepositorio.Obter(UsuarioId);

        if (usuarioDominio == null)
            throw new Exception("Usuario nao econtrado");

        return  usuarioDominio;
    }
    public async Task<Usuario> ObterEmail(string email)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterPorEmail(email);

        if (usuarioDominio == null)
            throw new Exception("Usuario nao econtrado");

        return usuarioDominio;
    }
    public async Task Deletar(int UsuarioId)
    {
        var usuarioDominio = await _usuarioRepositorio.Obter(UsuarioId);

        if (usuarioDominio == null)
            throw new Exception("Usuario nao encontrado");

        if (usuarioDominio.Ativo == false)
        {
            throw new Exception("O Usuário já está desativado!");
        }

        usuarioDominio.Deletar();

        await _usuarioRepositorio.Atualizar(usuarioDominio);
    }
    public async Task Restaurar(int UsuarioId)
    {
        var usuarioDominio = await _usuarioRepositorio.Obter(UsuarioId);

        if (usuarioDominio == null)
            throw new Exception("Usuario nao encontrado");

        if (usuarioDominio.Ativo == true)
        {
            throw new Exception("O Usuário já está ativo!");
        }

        usuarioDominio.Restaurar();

        await _usuarioRepositorio.Atualizar(usuarioDominio);
    }
    public async Task<List<Usuario>>  Listar(bool ativo)
    {
        return  await _usuarioRepositorio.Listar(ativo);
    }

    public async Task<List<string>> ListaNomesTiposUsuario()
    {
         var nomes = Enum.GetNames<TipoUsuario>().ToList();
         return  await Task.FromResult(nomes);

    }

    public async Task <List<int>> ListaValoresTiposUsuarios()
    {
        var valores = Enum.GetValues<TipoUsuario>().Cast<int>().ToList();
        return await Task.FromResult(valores);
    }
    #region  Util
    private static void ValidarInformacoesUsuario(Usuario usuario)
    {
        if (string.IsNullOrEmpty(usuario.Email))

            throw new Exception("O email nao pode ser vazaio");

        if (string.IsNullOrEmpty(usuario.Nome))

            throw new Exception("O nome nao pode ser vazio");
    }
    #endregion


}