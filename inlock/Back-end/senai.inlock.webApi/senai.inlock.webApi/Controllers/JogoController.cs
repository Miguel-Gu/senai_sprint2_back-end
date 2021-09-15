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
    [Authorize]
    public class JogoController : ControllerBase
    {
        private IJogoRepository _jogoRepository { get; set; }

        public JogoController()
        {
            _jogoRepository = new JogoRepository();
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            List<JogoDomain> listaJogo = _jogoRepository.ListarTodos();
            return Ok(listaJogo);
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult SerachById(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Id Invalido!",
                        erro = true

                    }) ;
            }
            try
            {
                JogoDomain Jogo = _jogoRepository.BuscarPorId(id);
                if (Jogo == null)
                {
                    return NotFound(
                        new
                        {
                            mensagem = "Jogo não encontrado"
                        });
                }

                return Ok(Jogo);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(JogoDomain novoJogo)
        {
            if (novoJogo.nomeJogo == null || novoJogo.descricao == null || novoJogo.valor <= 0 || novoJogo.idEstudio <= 0)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Dados incompletos",
                        erro = true
                    }) ;
            }
            try
            {
                _jogoRepository.Cadastrar(novoJogo);
                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, JogoDomain attJogo)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido",
                            erro = true
                        }
                    );
            }
            try
            {
                _jogoRepository.AtualizarId(id, attJogo);

                return StatusCode(200);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
                
            
        }




        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Id inválido",
                            erro = true
                        }
                    );
            }

            try
            {
                _jogoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }
    }
}
