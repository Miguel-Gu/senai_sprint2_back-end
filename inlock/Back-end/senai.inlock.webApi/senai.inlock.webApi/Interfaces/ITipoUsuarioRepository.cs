using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório TipoUsuarioRepository
    /// </summary>
    interface ITipoUsuarioRepository
    {
        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="novoTipoUsuario">Novo tipo de usuário a ser cadastrado</param>
        void Cadastrar(TipoUsuarioDomain novoTipoUsuario);

        /// <summary>
        /// Lista todos os tipos de usuário
        /// </summary>
        /// <returns>Uma lista de tipos de usuário</returns>
        List<TipoUsuarioDomain> ListarTodos();

        /// <summary>
        /// Busca um tipo de usuário através do id
        /// </summary>
        /// <param name="idTipoUsuario">Id do tipo de usuário a ser procurado</param>
        /// <returns>Um tipo de usuário</returns>
        TipoUsuarioDomain BuscarPorId(int idTipoUsuario);

        /// <summary>
        /// Atualiza um tipo de usuário existente
        /// </summary>
        /// <param name="idTipoUsuario">Id do tipo de usuário a ser atualizado</param>
        void AtualizarId(int idTipoUsuario, TipoUsuarioDomain attTipoUser);

        /// <summary>
        /// Deleta um tipo de usuário existente
        /// </summary>
        /// <param name="idTipoUsuario">Id do tipo de usuário a ser deletado</param>
        void Deletar(int idTipoUsuario);
    }
}
