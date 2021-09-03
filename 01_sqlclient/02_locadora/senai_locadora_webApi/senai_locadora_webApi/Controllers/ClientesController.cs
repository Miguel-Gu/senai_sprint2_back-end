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
    public class ClientesController : ControllerBase
    {
        private IClienteRepository _ClienteRepository { get; set; }

        public ClientesController()
        {
            _ClienteRepository = new ClienteRepository();
        }

        [HttpGet]

        public IActionResult Get()
        {
            List<ClienteDomain> listaClientes = _ClienteRepository.ListarTodos();

            return Ok(listaClientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ClienteDomain clienteBuscado = _ClienteRepository.BuscarPorId(id);

            if (clienteBuscado == null)
            {
                return NotFound("Nenhum cliente encontrado!");
            }

            return Ok(clienteBuscado);
        }

        [HttpPost]
        public IActionResult Post(ClienteDomain novoCliente)
        {
            //Faz a chamada para o método .cadastrar
            _ClienteRepository.Cadastrar(novoCliente);

            //Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, ClienteDomain clienteAtualizado)
        {
            if (clienteAtualizado.nomeCliente == null || clienteAtualizado.idCliente <= 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome ou o id do cliente não foi informado!"
                    });
            }

            ClienteDomain clienteBuscado = _ClienteRepository.BuscarPorId(clienteAtualizado.idCliente);

            if (clienteBuscado != null)
            {
                try
                {
                    _ClienteRepository.AtualizarIdUrl(id, clienteAtualizado);

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
                        mensagem = "Cliente não encontrado!",
                        errorStatus = true
                    }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ClienteRepository.Deletar(id);

            return NoContent();
        }
    }
}
