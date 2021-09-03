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
        public void AtualizarIdUrl(int id, AluguelDomain aluguelAtualizado)
        {
            if (aluguelAtualizado != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE ALUGUEL SET idVeiculo = @idVeiculo, idCliente = @idCliente, dataRetirada = @dataRetirada, dataDevolucao = @dataDevolucao WHERE idAluguel = @idAluguel";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@idAluguel", id);
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
                string querySelectById = "SELECT * FROM ALUGUEL A LEFT JOIN CLIENTE ON A.idcliente = CLIENTE.idCliente LEFT JOIN VEICULO ON A.idVeiculo = VEICULO.idVeiculo LEFT JOIN MODELO M ON VEICULO.idModelo = M.idModelo LEFT JOIN EMPRESA ON VEICULO.idEmpresa = EMPRESA.idEmpresa LEFT JOIN MARCA ON M.idMarca = MARCA.idMarca WHERE idAluguel = @idAluguel";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        AluguelDomain aluguelBuscado = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(rdr[0]),
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            veiculo = new VeiculoDomain()
                            {
                                idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                                placa = rdr["placa"].ToString(),
                                idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                empresa = new EmpresaDomain()
                                {
                                    idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                    nomeEmpresa = rdr["nomeEmpresa"].ToString()
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
                                        nomeMarca = rdr["nomeMarca"].ToString()
                                    }
                                }
                            },

                            idCliente = Convert.ToInt32(rdr["idCliente"]),
                            cliente = new ClienteDomain()
                            {
                                idCliente = Convert.ToInt32(rdr["idCliente"]),
                                nomeCliente = rdr["nomeCliente"].ToString(),
                                sobrenomeCliente = rdr["sobrenomeCliente"].ToString(),
                                CNH = rdr["CNH"].ToString(),

                            },

                            dataRetirada = Convert.ToDateTime(rdr["dataRetirada"]),
                            dataDevolucao = Convert.ToDateTime(rdr["dataDevolucao"])

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
                string queryInsert = "INSERT INTO ALUGUEL (idVeiculo, idCliente, dataRetirada, dataDevolucao) VALUES (@idVeiculo, @idCliente, @dataRetirada, @dataDevolucao)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", novoAluguel.idVeiculo);
                    cmd.Parameters.AddWithValue("@idCliente", novoAluguel.idCliente);
                    cmd.Parameters.AddWithValue("@dataRetirada", novoAluguel.dataRetirada);
                    cmd.Parameters.AddWithValue("@dataDevolucao", novoAluguel.dataDevolucao);


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
                string querySelectAll = "SELECT * FROM ALUGUEL A LEFT JOIN CLIENTE ON A.idcliente = CLIENTE.idCliente LEFT JOIN VEICULO ON A.idVeiculo = VEICULO.idVeiculo LEFT JOIN MODELO M ON VEICULO.idModelo = M.idModelo LEFT JOIN EMPRESA ON VEICULO.idEmpresa = EMPRESA.idEmpresa LEFT JOIN MARCA ON M.idMarca = MARCA.idMarca";

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
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            veiculo = new VeiculoDomain()
                            {
                                idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                                placa = rdr["placa"].ToString(),
                                idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                empresa = new EmpresaDomain()
                                {
                                    idEmpresa = Convert.ToInt32(rdr["idEmpresa"]),
                                    nomeEmpresa = rdr["nomeEmpresa"].ToString()
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
                                        nomeMarca = rdr["nomeMarca"].ToString()
                                    }
                                }
                            },

                            idCliente = Convert.ToInt32(rdr["idCliente"]),
                            cliente = new ClienteDomain()
                            {
                                idCliente = Convert.ToInt32(rdr["idCliente"]),
                                nomeCliente = rdr["nomeCliente"].ToString(),
                                sobrenomeCliente = rdr["sobrenomeCliente"].ToString(),
                                CNH = rdr["CNH"].ToString(),

                            },

                            dataRetirada = Convert.ToDateTime(rdr["dataRetirada"]),
                            dataDevolucao = Convert.ToDateTime(rdr["dataDevolucao"])

                        };

                        listaAlugueis.Add(aluguel);
                    }
                }
            }

            return listaAlugueis;
        }
    }
}
