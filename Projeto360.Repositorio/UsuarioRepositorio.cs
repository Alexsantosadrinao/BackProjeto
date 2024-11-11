using Microsoft.EntityFrameworkCore;
using Projeto360.Dominio.Entidades;

namespace DataAccess.Repositorio;

public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
{

    public UsuarioRepositorio(Projeto360Contexto contexto) : base(contexto) { }

    public async Task Atualizar(Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
        await _contexto.SaveChangesAsync();
    }

    public  async Task<List<Usuario>> Listar(bool ativo)
    {
        return   await _contexto.Usuarios.Where(u => u.Ativo == ativo).ToListAsync();
    }

    public async Task<Usuario> Obter(int usuarioId)
    {
        var usuarioWhere =  await _contexto.Usuarios
                                    .Where(u => u.ID == usuarioId)
                                    .Where(u => u.Ativo)
                                    .FirstOrDefaultAsync();

        var usuarioFirst = _contexto.Usuarios  
                                    .FirstOrDefaultAsync(usuario => usuario.ID == usuarioId );

        return await usuarioFirst;
    }

    public async Task<Usuario> ObterPorEmail(string email)
    {
        return  await _contexto.Usuarios  
                        .FirstOrDefaultAsync(usuario => usuario.Email == email && usuario.Ativo == true);

    }

    public async Task<int> salvar(Usuario usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
        await _contexto.SaveChangesAsync();

        return   usuario.ID;
    }

  
}