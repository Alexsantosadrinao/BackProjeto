using Microsoft.AspNetCore.Mvc;
using Projeto360.Dominio.Entidades;
using Projeto360.Api.Models.Respostas;
using Projeto360.Aplicacao;
using Projeto360.Api.Models.Requisicao;
using System.Formats.Asn1;
using Projeto360.Api.Models.Tarefas.Reposta;

namespace Projeto360.Api;

[ApiController]
[Route("[Controller]")]
public class TarefaControler : Controller
{
    private readonly ItarefaAplicao _tarefaAaplicao;

    public TarefaControler(ItarefaAplicao tarefaAplicacao)
    {
        _tarefaAaplicao = tarefaAplicacao;
    }

    [HttpGet]
    [Route("listar")]
    public ActionResult Listar() 
    {
        try
        {
            var tarefas = _tarefaAaplicao.ListarTarefas();

            var TarefaRespostas = tarefas.Select(tarefa => new TarefaRespota
            {
                ID = tarefa.ID,
                Nome = tarefa.Nome,
                Completa = tarefa.Completa
            });

            return Ok(TarefaRespostas);       
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
}