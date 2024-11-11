

namespace Projeto360.Api.Models.Respostas;
using Projeto360.Dominio.Enumeradores;


public class UsuarioResposta
{
    public int id {get; set;}
    public string Nome {get; set;}
    public string Email{get; set;}
    public TipoUsuario TipoUsuario { get; set; }
}