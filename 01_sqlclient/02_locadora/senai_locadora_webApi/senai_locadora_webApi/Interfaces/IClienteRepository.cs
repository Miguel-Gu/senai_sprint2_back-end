using senai_locadora_webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Interfaces
{
    interface IClienteRepository
    {
        /// <summary>
        /// Lista todos os cliente
        /// </summary>
        /// <returns>Uma lista de clientes</returns>
        List<ClienteDomain> ListarTodos();

        /// <summary>
        /// Busca um cliente através do seu id
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns>Um cliente</returns>
        ClienteDomain BuscarPorId(int idCliente);

        /// <summary>
        /// Deleta um cliente existente
        /// </summary>
        /// <param name="idCliente"></param>
        void Deletar(int idCliente);

        /// <summary>
        /// Atualiza um cliente existente
        /// </summary>
        /// <param name="clienteAtualizado"></param>
        void AtualizarIdUrl(int idCliente, ClienteDomain clienteAtualizado);

        /// <summary>
        /// Cadastra um novo cliente
        /// </summary>
        /// <param name="novoCliente"></param>
        void Cadastrar(ClienteDomain novoCliente);
    }
}
