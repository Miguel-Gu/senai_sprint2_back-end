using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "1")]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuario;

        public TipoUsuarioController()
        {
            _tipoUsuario = new TipoUsuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<TipoUsuarioDomain> listaTipo = _tipoUsuario.ListarTodos();
            return Ok(listaTipo);
        }

        [HttpPost]
        public IActionResult Post(TipoUsuarioDomain novoTipoUser)
        {
            if (novoTipoUser.titulo == null)
            {
                return NotFound(
                   new
                   {
                       mensagem = "Dados incompletos",
                       erro = true
                   });
            }
            try
            {
                _tipoUsuario.Cadastrar(novoTipoUser);
                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult SearchById(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Id Invalido!",
                        erro = true

                    });
            }
            try
            {
                TipoUsuarioDomain Tipo = _tipoUsuario.BuscarPorId(id);
                if (Tipo == null)
                {
                    return NotFound(
                        new
                        {
                            mensagem = "Tipo Usuário não encontrado"
                        });
                }

                return Ok(Tipo);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TipoUsuarioDomain attTipoUser)
        {
            if (id <= 0)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Id Invalido!",
                        erro = true

                    });
            }
            try
            {
                _tipoUsuario.AtualizarId(id, attTipoUser);
                return StatusCode(200);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Id Invalido!",
                        erro = true

                    });
            }
            try
            {
                _tipoUsuario.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

    }
}
