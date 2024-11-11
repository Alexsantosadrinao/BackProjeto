using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Projeto360.Dominio.Entidades;
using Projeto360.Servicos.interfaces;
using Projeto360.Servicos.JsonPlaceHolderServico.Models;

public class JsonPlaceHolderServico : IjasonPlaceHolderServico
{
    private readonly HttpClient _httpCliente;

    public JsonPlaceHolderServico()
    {
        _httpCliente = new HttpClient();
        _httpCliente.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

    }

  
    public async Task<List<Tarefa>> ListarTarefas()
    {
        HttpResponseMessage response = await _httpCliente.GetAsync("todos");
        response.EnsureSuccessStatusCode();

        string responsebody = await response.Content.ReadAsStringAsync();
        var todos = JsonConvert.DeserializeObject<List<Todo>>(responsebody);
        
       
        var tarefas = todos.Select(todo => new Tarefa
        {
            ID = todo.Id,
            Nome =todo.Title,
            Completa = todo.Completed
        }).ToList();

        return tarefas;
    }
}