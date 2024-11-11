using Projeto360.Dominio.Entidades;
using Projeto360.Servicos.interfaces;

namespace Projeto360.Aplicacao;

public class TarefaAplicacao : ItarefaAplicao
{
    private readonly IjasonPlaceHolderServico _jasonPlaceHolderServico;

    public TarefaAplicacao(IjasonPlaceHolderServico jasonPlaceHolderServico)
    {
        _jasonPlaceHolderServico = jasonPlaceHolderServico;
    }

    public List<Tarefa> ListarTarefas()
    {
        return _jasonPlaceHolderServico.ListarTarefas().Result;
    }
}