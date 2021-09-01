using senai_locadora_webApi.Domains;
using senai_locadora_webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Repositories
{
    /// <summary>
    /// Classe responsável pelo repositório dos veículos
    /// </summary>
    public class VeiculoRepository : IVeiculoRepository
    {
        public void AtualizarIdCorpo(VeiculoDomain veiculoAtualizado)
        {
            throw new NotImplementedException();
        }

        public VeiculoDomain BuscarPorId(int idVeiculo)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int idVeiculo)
        {
            throw new NotImplementedException();
        }

        public List<VeiculoDomain> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
