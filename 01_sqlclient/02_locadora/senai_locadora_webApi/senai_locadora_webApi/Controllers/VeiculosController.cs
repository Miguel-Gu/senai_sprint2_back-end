﻿using Microsoft.AspNetCore.Http;
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

        [HttpPut]
        public IActionResult PutIdBody(VeiculoDomain veiculoAtualizado)
        {
            if (veiculoAtualizado != null || veiculoAtualizado.idVeiculo <= 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome ou o id do veiculo não foi informado!"
                    });
            }

            VeiculoDomain veiculoBuscado = _VeiculoRepository.BuscarPorId(veiculoAtualizado.idVeiculo);

            if (veiculoBuscado != null)
            {
                try
                {
                    _VeiculoRepository.AtualizarIdCorpo(veiculoAtualizado);

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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _VeiculoRepository.Deletar(id);

            return NoContent();
        }
    }
}