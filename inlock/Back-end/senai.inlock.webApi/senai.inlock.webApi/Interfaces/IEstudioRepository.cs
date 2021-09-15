using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{   
    /// <summary>
    /// Interface responsável pelo repositório EstudioRepository
    /// </summary>
    interface IEstudioRepository
    {
        /// <summary>
        /// Cadastra um novo estúdio
        /// </summary>
        /// <param name="novoEstudio">Novo estúdio a ser cadastrado</param>
        void Cadastrar(EstudioDomain novoEstudio);

        /// <summary>
        /// Lista todos os estúdios
        /// </summary>
        /// <returns>Uma lista de estúdios</returns>
        List<EstudioDomain> ListarTodos();

        /// <summary>
        /// Busca um estúdio através do id
        /// </summary>
        /// <param name="idEstudio">Id do estúdio a ser procurado</param>
        /// <returns>Um estúdio</returns>
        EstudioDomain BuscarPorId(int idEstudio);

        /// <summary>
        /// Atualiza um estúdio existente
        /// </summary>
        /// <param name="idEstudio">Id do estúdio a ser atualizado</param>
        void AtualizarId(int idEstudio, EstudioDomain attEstudio);

        /// <summary>
        /// Deleta um estúdio existente
        /// </summary>
        /// <param name="idEstudio">Id do estúdio a ser deletado</param>
        void Deletar(int idEstudio);

        /// <summary>
        /// Lista os estudios e suas empresas
        /// </summary>
        /// <returns>Retorna a lista de empresas e seus jogos</returns>
        List<EstudioDomain> ListarEmpresasJogos();
    }
}
