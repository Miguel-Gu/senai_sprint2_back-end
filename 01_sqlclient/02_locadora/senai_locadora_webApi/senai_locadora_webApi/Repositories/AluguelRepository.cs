using senai_locadora_webApi.Domains;
using senai_locadora_webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private string stringConexao = "Data Source=LAPTOP-GBJVH1HS\\SQLEXPRESS; initial catalog=LOCADORA; user Id=sa; pwd=senai@132";
        public void AtualizarIdCorpo(AluguelDomain aluguelAtualizado)
        {
            if (aluguelAtualizado.dataDevolucao != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE ALUGUEL SET dataDevolucao = @dataDevolucao WHERE idAluguel = @idAluguel";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@idVeiculo", aluguelAtualizado.idVeiculo);
                        cmd.Parameters.AddWithValue("@idCliente", aluguelAtualizado.idCliente);
                        cmd.Parameters.AddWithValue("@dataRetirada", aluguelAtualizado.dataRetirada);
                        cmd.Parameters.AddWithValue("@dataDevolucao", aluguelAtualizado.dataDevolucao);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public AluguelDomain BuscarPorId(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT * FROM ALUGUEL WHERE idAluguel = @idAluguel";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        AluguelDomain aluguelBuscado = new AluguelDomain
                        {
                            idAluguel = Convert.ToInt32(reader["idAluguel"]),

                            idVeiculo = Convert.ToInt32(reader["idVeiculo"]),

                            idCliente = Convert.ToInt32(reader["idCliente"]),

                            dataRetirada = reader["dataRetirada"],

                            dataDevolucao = reader["dataDevolucao"]

                        };

                        return aluguelBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(AluguelDomain novoAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO ALUGUEL (idVeiculo) VALUES (@idVeiculo)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", novoAluguel.idVeiculo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM ALUGUEL WHERE idAluguel = @idAluguel";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AluguelDomain> ListarTodos()
        {
            List<AluguelDomain> listaAlugueis = new List<AluguelDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT * FROM ALUGUEL";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        AluguelDomain aluguel = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(rdr[0]),

                            idVeiculo = Convert.ToInt32(rdr[1]),

                            idCliente = Convert.ToInt32(rdr[2]),

                            dataRetirada = rdr[3],

                            dataDevolucao = rdr[4]
                        };

                        listaAlugueis.Add(aluguel);
                    }
                }
            };

            return listaAlugueis;
        }
    }
}
