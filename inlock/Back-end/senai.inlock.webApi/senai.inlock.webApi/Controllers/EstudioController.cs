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
    public class EstudioController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }

        public EstudioController()
        {
            _estudioRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<EstudioDomain> listaEstudio = _estudioRepository.ListarTodos();
            return Ok(listaEstudio);
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
                EstudioDomain estudioBuscado = _estudioRepository.BuscarPorId(id);
                if (estudioBuscado == null)
                {
                    return NotFound(
                        new
                        {
                            mensagem = "Estudio não encontrado"
                        });
                }

                return Ok(estudioBuscado);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }



        [HttpPost]
        public IActionResult Post(EstudioDomain novoEstudio)
        {
            if (novoEstudio.nomeEstudio == null)
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
                _estudioRepository.Cadastrar(novoEstudio);
                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
            
        }



        [HttpPut("{id}")]
        public IActionResult Put(int id, EstudioDomain attEstudio)
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
                _estudioRepository.AtualizarId(id, attEstudio);
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
                            mensagem = "Id inválido",
                            erro = true
                        }
                    );
            }
            try
            {
                _estudioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        [HttpGet("ListarJogosEmpresas")]
        public IActionResult GetEmpresa()
        {
            List<EstudioDomain> listaEstudio = _estudioRepository.ListarEmpresasJogos();
            return Ok(listaEstudio);
        }
    }
}
