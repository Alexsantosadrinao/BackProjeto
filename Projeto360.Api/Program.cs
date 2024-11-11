using DataAccess.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Projeto360.Aplicacao;
using Projeto360.Servicos.interfaces;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ItarefaAplicao, TarefaAplicacao>();

// Adicione serviços ao conteiner
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();

// Adicione as interfaces de banco de dados 
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// Adicione as interfaces de banco de dados 

builder.Services.AddScoped<IjasonPlaceHolderServico, JsonPlaceHolderServico>();

builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => 
    {
        builder.WithOrigins("http://localhost:3000")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyHeader()
            .AllowAnyMethod();


    });

});


// Adicionar serviço banco de dados

builder.Services.AddDbContext<Projeto360Contexto>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// saiba mais sobre configuração/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{   
    app.UseCors();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
