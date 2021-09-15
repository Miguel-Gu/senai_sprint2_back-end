using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório JogoRepository
    /// </summary>
    interface IJogoRepository
    {
        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo">Novo jogo a ser cadastrado</param>
        void Cadastrar(JogoDomain novoJogo);

        /// <summary>
        /// Lista todos os jogos
        /// </summary>
        /// <returns>Uma lista de jogos</returns>
        List<JogoDomain> ListarTodos();

        /// <summary>
        /// Busca um jogo através do id
        /// </summary>
        /// <param name="idJogo">Id do jogo a ser procurado</param>
        /// <returns>Um jogo</returns>
        JogoDomain BuscarPorId(int idJogo);

        /// <summary>
        /// Atualiza um jogo existente
        /// </summary>
        /// <param name="idJogo">Id do jogo a ser atualizado</param>
        void AtualizarId(int idJogo, JogoDomain attJogo);

        /// <summary>
        /// Deleta um jogo existente
        /// </summary>
        /// <param name="idJogo">Id do jogo a ser deletado</param>
        void Deletar(int idJogo);
    }
}
