using senai_locadora_webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório AluguelRepository
    /// </summary>
    interface IAluguelRepository
    {
        /// <summary>
        /// Lista todos os aluguéis
        /// </summary>
        /// <returns>Uma lista de aluguéis</returns>
        List<AluguelDomain> ListarTodos();

        /// <summary>
        /// Busca um Aluguel através do seu id
        /// </summary>
        /// <param name="idAluguel"></param>
        /// <returns>Um aluguel</returns>
        AluguelDomain BuscarPorId(int idAluguel);

        /// <summary>
        /// Deleta um aluguel existente
        /// </summary>
        /// <param name="idAluguel"></param>
        void Deletar(int idAluguel);

        /// <summary>
        /// Atualiza um aluguel existente
        /// </summary>
        /// <param name="aluguelAtualizado"></param>
        void AtualizarIdCorpo(AluguelDomain aluguelAtualizado);

        /// <summary>
        /// Cadastra um novo aluguel
        /// </summary>
        /// <param name="novoAluguel"></param>
        void Cadastrar(AluguelDomain novoAluguel);
    }
}
