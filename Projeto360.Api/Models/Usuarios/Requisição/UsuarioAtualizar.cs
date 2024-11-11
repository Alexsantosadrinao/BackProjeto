namespace Projeto360.Api.Models.Requisicao;
using Projeto360.Dominio.Enumeradores;
public class UsuarioAtualizar
{
    public int ID {get; set;}
    public string Nome{get; set;}
    public string Email{get; set;}
    public TipoUsuario tipoUsuario {get; set;}

}