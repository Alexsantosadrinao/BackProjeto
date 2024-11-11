using Microsoft.AspNetCore.Mvc;
using Projeto360.Dominio.Entidades;
using Projeto360.Api.Models.Respostas;
using Projeto360.Aplicacao;
using Projeto360.Api.Models.Requisicao;
using System.Formats.Asn1;

namespace Projeto360.Api;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : Controller
{
    private readonly IUsuarioAplicacao _usuarioAplicao;

    public UsuarioController(IUsuarioAplicacao usuarioAplicao)
    {
        _usuarioAplicao = usuarioAplicao;
    }

    [HttpPost]
    [Route("Criar")]
    public async Task<ActionResult> Criar([FromBody] UsuarioCriar usuarioCriar)
    {
        try
        {
            var usuarioDominio = new Usuario()
            {
                Nome = usuarioCriar.Nome,
                Email = usuarioCriar.Email,
                Senha = usuarioCriar.Senha,
                TipoUsuario = usuarioCriar.TipoUsuario
            };

            var UsuarioId =   await _usuarioAplicao.Criar(usuarioDominio);

            return Ok(UsuarioId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("Obter/{usuarioId}")]
    public async Task<ActionResult> Obter([FromRoute] int usuarioId)
    {
        try
        {
            var usuarioDominio = await _usuarioAplicao.Obter(usuarioId);


            var usuarioResposta = new UsuarioResposta()
            {
                id = usuarioDominio.ID,
                Nome = usuarioDominio.Nome,
                Email = usuarioDominio.Email,
                TipoUsuario = usuarioDominio.TipoUsuario
            };
            return Ok(usuarioResposta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("ObterEmail")]
    public async Task<ActionResult>  ObterPoEmail([FromBody] string email)
    {
        try
        {
            var usuarioDominio = await _usuarioAplicao.ObterEmail(email);

            var usuarioResposta = new UsuarioResposta()
            {
                id = usuarioDominio.ID,
                Nome = usuarioDominio.Nome,
                Email = usuarioDominio.Email,
                TipoUsuario = usuarioDominio.TipoUsuario
            };

            return Ok(usuarioResposta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Atualizar")]
    public async Task<ActionResult>  Atualizar([FromBody] UsuarioAtualizar usuario)
    {
        try
        {
            var usuarioDominio = new Usuario()
            {
                ID = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            await _usuarioAplicao.Atualizar(usuarioDominio);

            return Ok("Usuário atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Alterarsenha")]
    public async Task <IActionResult> AlterarSenha([FromBody] UsuarioAlterarSenha usuario)
    {
        try
        {
            var usuarioDominio = new Usuario()
            {
                ID = usuario.ID,
                Senha = usuario.SenhaAntiga
            };

            string novaSenha = usuario.NovaSenha;

            await _usuarioAplicao.AtualizarSenha(usuarioDominio, novaSenha);

            return Ok("Senha alterada com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("Deletar/{usuarioId}")]
    public async Task <IActionResult> Deletar([FromRoute] int usuarioId)
    {
        try
        {
            await _usuarioAplicao.Deletar(usuarioId);

            return Ok("Usuário deletado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Restaurar/{usuarioId}")]
    public async Task <IActionResult> Restaurar([FromRoute] int usuarioId)
    {
        try
        {
            await _usuarioAplicao.Restaurar(usuarioId);

            return Ok("Usuário Restaurado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("Listar")]
    public async Task <IActionResult> List([FromQuery] bool ativos)
    {
        try
        {
            var usuariosDominio =  await _usuarioAplicao.Listar(ativos);

            var usuarios = usuariosDominio.Select(usuario => new UsuarioResposta()
            {
                id = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario

            }).ToList();

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("ListarTipoUsuario")]
    public async Task <IActionResult> ListarTipoUsuario()
    {
        try
        {
            var nomes =  await _usuarioAplicao.ListaNomesTiposUsuario();
            var valores = await _usuarioAplicao.ListaValoresTiposUsuarios();

            List<Object> Usuarios = new List<object>();

            for (int i = 0; i < valores.Count(); i++)
            {
                var usuario = new
                {
                    Nome = nomes[i],
                    Valor = valores[i]
                };
                Usuarios.Add(usuario);
            }
            return Ok(Usuarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}