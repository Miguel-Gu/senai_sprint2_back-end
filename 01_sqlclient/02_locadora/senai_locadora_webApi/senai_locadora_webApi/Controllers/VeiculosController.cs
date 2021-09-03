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
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository _VeiculoRepository { get; set; }

        public VeiculosController()
        {
            _VeiculoRepository = new VeiculoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<VeiculoDomain> listaVeiculos = _VeiculoRepository.ListarTodos();

            return Ok(listaVeiculos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            VeiculoDomain veiculoBuscado = _VeiculoRepository.BuscarPorId(id);

            if (veiculoBuscado == null)
            {
                return NotFound("Nenhum veiculo encontrado!");
            }

            return Ok(veiculoBuscado);
        }

        [HttpPost]
        public IActionResult Post(VeiculoDomain novoVeiculo)
        {
            //Faz a chamada para o método .cadastrar
            _VeiculoRepository.Cadastrar(novoVeiculo);

            //Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [HttpPut("{idVeiculo}")]
        public IActionResult PutIdUrl(int idVeiculo, VeiculoDomain veiculoAtualizado)
        {
            if (veiculoAtualizado == null || veiculoAtualizado.idVeiculo <= 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome ou o id do veiculo não foi informado!"
                    });
            }

            VeiculoDomain veiculoBuscado = _VeiculoRepository.BuscarPorId(idVeiculo);

            if (veiculoBuscado != null)
            {
                try
                {
                    _VeiculoRepository.AtualizarIdUrl(idVeiculo, veiculoAtualizado);

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
            try
            {
                _VeiculoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception codErro)
            {
                return BadRequest(
                    
                    new
                    {
                        mensagem = "Não é possível excluir esse veículo pois existe um aluguel vinculado a ele"
                    });
            }
        }
    }
}
