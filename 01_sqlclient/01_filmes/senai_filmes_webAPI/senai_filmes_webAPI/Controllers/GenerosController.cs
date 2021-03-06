using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using senai_filmes_webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Controlador responsável pelo end points referentes aos gêneros
/// </summary>
namespace senai_filmes_webAPI.Controllers
{
    //Define que o tipo de resposta da API será no Formato JSON
    [Produces("application/json")]

    //Define que a rota de uma requisição será no formato dominio/api/nomeController.
    // ex: http://localhost:5000/api/generos
    [Route("api/[controller]")]
    //Define que é um controlador de API
    [ApiController]
    [Authorize]
    public class GenerosController : ControllerBase
    {
        /// <summary>
        /// Objeto que irá receber todos os métodos definidos na interface
        /// </summary>
        private IGeneroRepository _GeneroRepository { get; set; }

        /// <summary>
        /// Instacia um objeto _GeneroRepository para que haja a referencia dos métodos no repositório
        /// </summary>
        public GenerosController()
        {
            _GeneroRepository = new GeneroRepository();
        }

        [HttpGet]
        //IActionResult = resultado de uma ação.
        //Get() = nome generico.
        public IActionResult Get()
        {
            //Lista de Generos
            //Se conectar com o Repositorio.


            //Criar uma lista nomeada listaGeneros para receber os dados.
            List<GeneroDomain> listaGeneros = _GeneroRepository.ListarTodos();

            //Retorna os status code 200(Ok) com a lista generos no formato JSON
            return Ok(listaGeneros);
        }

        [Authorize(Roles = "administrador, comum")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GeneroDomain generoBuscado = _GeneroRepository.BuscarPorId(id);

            if (generoBuscado == null)
            {
                return NotFound("Nenhum gênero encontrado!");
            }

            return Ok(generoBuscado);
        }

        /// <summary>
        /// Cadastra um novo gênero
        /// </summary>
        /// <param name="novoGenero"></param>
        /// <returns>Um status code 201 - Created</returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            //Faz a chamada para o método .cadastrar
            _GeneroRepository.Cadastrar(novoGenero);

            //Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, GeneroDomain generoAtualizado)
        {
            GeneroDomain generoBuscado = _GeneroRepository.BuscarPorId(id);

            if (generoBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Gênero não encontrado!",
                            erro = true
                        }
                    );
            }

            try
            {
                _GeneroRepository.AtualizarIdUrl(id, generoAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult PutIdBody(GeneroDomain generoAtualizado)
        {
            if (generoAtualizado.nomeGenero == null || generoAtualizado.idGenero <= 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome ou o id do gênero não foi informado!"
                    });
            }

            GeneroDomain generoBuscado = _GeneroRepository.BuscarPorId(generoAtualizado.idGenero);

            if (generoBuscado != null)
            {
                try
                {
                    _GeneroRepository.AtualizarIdCorpo(generoAtualizado);

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
                        mensagem = "Gênero não encontrado!",
                        errorStatus = true
                    }
                );
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _GeneroRepository.Deletar(id);

            return NoContent();
        }
    }
}
