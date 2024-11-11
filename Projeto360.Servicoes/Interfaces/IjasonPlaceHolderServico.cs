using Projeto360.Dominio.Entidades;

namespace Projeto360.Servicos.interfaces;

public interface IjasonPlaceHolderServico
{
    Task<List<Tarefa>> ListarTarefas();
}