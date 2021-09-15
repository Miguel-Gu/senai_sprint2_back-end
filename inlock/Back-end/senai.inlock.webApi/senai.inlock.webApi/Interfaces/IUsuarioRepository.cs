using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório UsuarioRepository
    /// </summary>
    interface IUsuarioRepository
    {
        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Novo usuário a ser cadastrado</param>
        void Cadastrar(UsuarioDomain novoUsuario);

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Uma lista de usuários</returns>
        List<UsuarioDomain> ListarTodos();

        /// <summary>
        /// Busca um usuário através do id
        /// </summary>
        /// <param name="idUsuario">Id do usuário a ser procurado</param>
        /// <returns>Um usuário</returns>
        UsuarioDomain BuscarPorId(int idUsuario);

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="idUsuario">Id do usuário a ser atualizado</param>
        void AtualizarId(int idUsuario, UsuarioDomain attUser);

        /// <summary>
        /// Deleta um usuário existente
        /// </summary>
        /// <param name="idUsuario">Id do usuário a ser deletado</param>
        void Deletar(int idUsuario);

        /// <summary>
        /// Faz login de um usuario
        /// </summary>
        /// <param name="email">email do usuário para fazer login</param>
        /// <param name="senha">senha do usuário para fazer login</param>
        UsuarioDomain Login(string email, string senha);
    }
}
