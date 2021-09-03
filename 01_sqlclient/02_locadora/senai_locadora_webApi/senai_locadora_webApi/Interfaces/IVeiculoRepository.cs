using senai_locadora_webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Interfaces
{
    interface IVeiculoRepository
    {
        /// <summary>
        /// Lista todos os veículos
        /// </summary>
        /// <returns>Uma lista de veículos</returns>
        List<VeiculoDomain> ListarTodos();

        /// <summary>
        /// Busca um veículo através do seu id
        /// </summary>
        /// <param name="idVeiculo"></param>
        /// <returns>Um veículo</returns>
        VeiculoDomain BuscarPorId(int idVeiculo);

        /// <summary>
        /// Deleta um veículo existente
        /// </summary>
        /// <param name="idVeiculo"></param>
        void Deletar(int idVeiculo);

        /// <summary>
        /// Atualiza um veículo existente
        /// </summary>
        /// <param name="veiculoAtualizado"></param>
        void AtualizarIdUrl(int IdVeiculo, VeiculoDomain veiculoAtualizado);

        /// <summary>
        /// Cadastra um novo veículo
        /// </summary>
        /// <param name="novoVeiculo"></param>
        void Cadastrar(VeiculoDomain novoVeiculo);
    }
}
