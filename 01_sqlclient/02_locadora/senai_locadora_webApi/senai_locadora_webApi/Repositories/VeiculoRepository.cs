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
        private string stringConexao = "Data Source=LAPTOP-GBJVH1HS\\SQLEXPRESS; initial catalog=LOCADORA; user Id=sa; pwd=senai@132";
        public void AtualizarIdUrl(int idVeiculo, VeiculoDomain veiculoAtualizado)
        {
            if (veiculoAtualizado.placa != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE VEICULO SET placa = @placa, idEmpresa = @idEmpresa, idModelo= @idModelo WHERE idVeiculo = @idVeiculo";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);
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
                string querySelectById = "SELECT * FROM VEICULO LEFT JOIN EMPRESA ON EMPRESA.idEmpresa = VEICULO.idEmpresa LEFT JOIN MODELO ON MODELO.idModelo = VEICULO.idEmpresa LEFT JOIN MARCA ON MARCA.idMarca = modelo.idMarca";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        VeiculoDomain veiculoBuscado = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),

                            empresa = new EmpresaDomain()
                            {
                                idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                nomeEmpresa = rdr["nomeEmpresa"].ToString(),
                            },

                            idModelo = Convert.ToInt32(rdr["idModelo"]),
                            modelo = new ModeloDomain()
                            {
                                idModelo = Convert.ToInt32(rdr["idModelo"]),
                                nomeModelo = rdr["nomeModelo"].ToString(),
                                idMarca = Convert.ToInt32(rdr["idMarca"]),

                                marca = new MarcaDomain()
                                {
                                    idMarca = Convert.ToInt32(rdr["idMarca"]),
                                    nomeMarca = rdr["nomeMarca"].ToString(),
                                }
                            },
                            placa = rdr["placa"].ToString()
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
                string queryInsert = "INSERT INTO VEICULO (placa, idEmpresa, idModelo) VALUES (@placa, @idEmpresa, @idModelo)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@placa", novoVeiculo.placa);
                    cmd.Parameters.AddWithValue("@idEmpresa", novoVeiculo.idEmpresa);
                    cmd.Parameters.AddWithValue("@idModelo", novoVeiculo.idModelo);

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
                string querySelectAll = "SELECT * FROM VEICULO LEFT JOIN EMPRESA ON EMPRESA.idEmpresa = VEICULO.idEmpresa LEFT JOIN MODELO ON MODELO.idModelo = VEICULO.idEmpresa LEFT JOIN MARCA ON MARCA.idMarca = modelo.idMarca";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),

                            empresa = new EmpresaDomain()
                            {
                                idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                nomeEmpresa = rdr["nomeEmpresa"].ToString(),
                            },

                            idModelo = Convert.ToInt32(rdr["idModelo"]),
                            modelo = new ModeloDomain()
                            {
                                idModelo = Convert.ToInt32(rdr["idModelo"]),
                                nomeModelo = rdr["nomeModelo"].ToString(),
                                idMarca = Convert.ToInt32(rdr["idMarca"]),

                                marca = new MarcaDomain()
                                {
                                    idMarca = Convert.ToInt32(rdr["idMarca"]),
                                    nomeMarca = rdr["nomeMarca"].ToString(),
                                }
                            },
                            placa = rdr["placa"].ToString()
                        };

                        listaVeiculos.Add(veiculo);
                    }
                }
            };

            return listaVeiculos;
        }
    }
}
