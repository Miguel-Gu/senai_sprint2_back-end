using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        string stringConexao = @"Data Source=DESKTOP-CV21P6P\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=#Murillo1#";


        public void AtualizarId(int idEstudio, EstudioDomain attEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE ESTUDIO SET nomeEstudio = @nomeEstudio WHERE idEstudio = @idEstudio";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@nomeEstudio", attEstudio.nomeEstudio);
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public EstudioDomain BuscarPorId(int idEstudio)
        {
            EstudioDomain buscaEstudio = new EstudioDomain();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT idEstudio, nomeEstudio FROM ESTUDIO WHERE idEstudio = @idEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        buscaEstudio = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr[0]),
                            nomeEstudio = rdr[1].ToString()
                        };


                    }
                    if (buscaEstudio == null)
                    {
                        return null;
                    }
                }
                return buscaEstudio;
            }

        }

        public void Cadastrar(EstudioDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO ESTUDIO (nomeEstudio) VALUES (@nomeEstudio)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeEstudio", novoEstudio.nomeEstudio);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM ESTUDIO WHERE idEstudio = @idEstudio";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> listaEstudio = new List<EstudioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT idEstudio, nomeEstudio FROM ESTUDIO";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain ESTUDIO = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr[0]),
                            nomeEstudio = rdr[1].ToString()
                        };

                        listaEstudio.Add(ESTUDIO);
                    }
                }
            }

            return listaEstudio;
        }

        public List<EstudioDomain> ListarEmpresasJogos()
        {
            List<EstudioDomain> listaEstudio = new List<EstudioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT ESTUDIO.idEstudio, nomeEstudio, ISNULL(descricao, 'Não Cadastrado') descricao, ISNULL(nomeJogo, 'Não cadastrado') nomeJogo, ISNULL(idJogo, 0) idJogo, ISNULL(valor, 0) valor, ISNULL(dataLancamento, '') dataLancamento FROM ESTUDIO LEFT JOIN JOGO ON JOGO.idEstudio = ESTUDIO.idEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain ESTUDIO = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                            nomeEstudio = rdr["nomeEstudio"].ToString(),
                            jogo = new JogoDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                                idJogo = Convert.ToInt32(rdr["idJogo"]),
                                nomeJogo = rdr["nomeJogo"].ToString(),
                                descricao = rdr["descricao"].ToString(),
                                valor = Convert.ToSingle(rdr["valor"]),
                                dataLancamento = Convert.ToDateTime(rdr["dataLancamento"])
                            }
                        };

                        listaEstudio.Add(ESTUDIO);
                    }
                }
            }

            return listaEstudio;
        }
    }
}
