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
/// Controlador responsável pelo end points referentes aos filmes
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
    public class FilmesController : ControllerBase
    {
        /// <summary>
        /// Objeto que irá receber todos os métodos definidos na interface
        /// </summary>
        private IFilmeRepository _FilmeRepository { get; set; }

        /// <summary>
        /// Instacia um objeto _FilmeRepository para que haja a referencia dos métodos no repositório
        /// </summary>
        public FilmesController()
        {
            _FilmeRepository = new FilmeRepository();
        }

        [HttpGet]
        //IActionResult = resultado de uma ação.
        //Get() = nome generico.
        public IActionResult Get()
        {
            //Lista de Filmes
            //Se conectar com o Repositorio.


            //Criar uma lista nomeada listaFilmes para receber os dados.
            List<FilmeDomain> listaFilmes = _FilmeRepository.ListarTodos();

            //Retorna os status code 200(Ok) com a lista filmes no formato JSON
            return Ok(listaFilmes);
        }

        /// <summary>
        /// Cadastra um novo filme
        /// </summary>
        /// <param name="novoFilme"></param>
        /// <returns>Um status code 201 - Created</returns>
        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            _FilmeRepository.Cadastrar(novoFilme);

            return StatusCode(201);
        }
    }
}
