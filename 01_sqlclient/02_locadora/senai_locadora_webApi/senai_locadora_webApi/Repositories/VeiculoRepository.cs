using senai_locadora_webApi.Domains;
using senai_locadora_webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Repositories
{
    /// <summary>
    /// Classe responsável pelo repositório dos veículos
    /// </summary>
    public class VeiculoRepository : IVeiculoRepository
    {
        private string stringConexao = "Data Source=NOTE0113A4\\SQLEXPRESS; initial catalog=LOCADORA; user Id=sa; pwd=Senai@132";
        public void AtualizarIdCorpo(VeiculoDomain veiculoAtualizado)
        {
            if (veiculoAtualizado.placa != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE VEICULO SET placa = @placa WHERE idVeiculo = @idVeiculo";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@idVeiculo", veiculoAtualizado.idVeiculo);
                        cmd.Parameters.AddWithValue("@idEmpresa", veiculoAtualizado.idEmpresa);
                        cmd.Parameters.AddWithValue("@idModelo", veiculoAtualizado.idModelo);
                        cmd.Parameters.AddWithValue("@placa", veiculoAtualizado.placa);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public VeiculoDomain BuscarPorId(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeEmpresa, nomeModelo, placa FROM VEICULO LEFT JOIN EMPRESA ON EMPRESA.idEmpresa = VEICULO.idEmpresa LEFT JOIN MODELO ON MODELO.idModelo = VEICULO.idEmpresa WHERE idVeiculo = @idVeiculo;";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        VeiculoDomain veiculoBuscado = new VeiculoDomain
                        {
                            empresa = new EmpresaDomain()
                            {
                                nomeEmpresa = reader["nomeEmpresa"].ToString()
                            },
                            modelo = new ModeloDomain()
                            {
                                nomeModelo = reader["nomeModelo"].ToString()
                            },
                            placa = reader["placa"].ToString(),

                        };

                        return veiculoBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO VEICULO (placa) VALUES (@placa)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@placa", novoVeiculo.placa);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM VEICULO WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> ListarTodos()
        {
            List<VeiculoDomain> listaVeiculos = new List<VeiculoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT nomeEmpresa, nomeModelo, placa FROM VEICULO LEFT JOIN EMPRESA ON EMPRESA.idEmpresa = VEICULO.idEmpresa LEFT JOIN MODELO ON MODELO.idModelo = VEICULO.idEmpresa";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            empresa = new EmpresaDomain()
                            {
                                nomeEmpresa = rdr[0].ToString(),
                            },
                            modelo = new ModeloDomain()
                            {
                                nomeModelo = rdr[1].ToString(),
                            },
                            placa = rdr[2].ToString()
                        };

                        listaVeiculos.Add(veiculo);
                    }
                }
            };

            return listaVeiculos;
        }
    }
}
