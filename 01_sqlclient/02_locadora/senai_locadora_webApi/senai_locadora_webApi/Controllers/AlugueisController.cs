using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_locadora_webApi.Domains;
using senai_locadora_webApi.Interfaces;
using senai_locadora_webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlugueisController : ControllerBase
    {
        private IAluguelRepository _AluguelRepository { get; set; }

        public AlugueisController()
        {
            _AluguelRepository = new AluguelRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<AluguelDomain> listaAluguel = _AluguelRepository.ListarTodos();

            return Ok(listaAluguel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AluguelDomain aluguelBuscado = _AluguelRepository.BuscarPorId(id);

            if (aluguelBuscado == null)
            {
                return NotFound("Nenhum aluguel encontrado!");
            }

            return Ok(aluguelBuscado);
        }

        [HttpPost]
        public IActionResult Post(AluguelDomain novoAluguel)
        {
            //Faz a chamada para o método .cadastrar
            _AluguelRepository.Cadastrar(novoAluguel);

            //Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [HttpPut("{idAluguel}")]
        public IActionResult PutIUrl(int idAluguel, AluguelDomain aluguelAtualizado)
        {
            if (aluguelAtualizado == null || aluguelAtualizado.idAluguel <= 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome ou o id do aluguel não foi informado!"
                    });
            }

            AluguelDomain aluguelBuscado = _AluguelRepository.BuscarPorId(aluguelAtualizado.idAluguel);

            if (aluguelBuscado != null)
            {
                try
                {
                    _AluguelRepository.AtualizarIdUrl(idAluguel, aluguelAtualizado);

                    return NoContent();
                }
                catch (Exception codErro)
                {
                    return BadRequest(codErro);
                }
            }

            return NotFound(
                    new
                    {
                        mensagem = "Veiculo não encontrado!",
                        errorStatus = true
                    }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _AluguelRepository.Deletar(id);

            return NoContent();
        }
    }
}
