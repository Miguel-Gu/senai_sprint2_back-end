using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        string stringConexao = @"Data Source=DESKTOP-CV21P6P\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=#Murillo1#";
        public void AtualizarId(int idJogo, JogoDomain attJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE JOGO SET nomeJogo = @nomeJogo, descricao = @descricao, dataLancamento = @dataLancamento, valor = @valor, idEstudio = @idEstudio WHERE idJogo = @idJogo";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", attJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", attJogo.descricao);
                    cmd.Parameters.AddWithValue("@dataLancamento", attJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", attJogo.valor);
                    cmd.Parameters.AddWithValue("@idEstudio", attJogo.idEstudio);
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogoDomain BuscarPorId(int idJogo)
        {
            JogoDomain Jogo = new JogoDomain();
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idJogo, nomeJogo, descricao, dataLancamento, valor, JOGO.idEstudio, nomeEstudio FROM JOGO INNER JOIN ESTUDIO ON JOGO.idEstudio = ESTUDIO.idEstudio WHERE idJogo = @idJogo";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            nomeJogo = rdr[1].ToString(),
                            descricao = rdr[2].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[3]),
                            valor = Convert.ToSingle(rdr[4]),
                            idEstudio = Convert.ToInt32(rdr[5]),
                            estudio = new EstudioDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr[5]),
                                nomeEstudio = rdr[6].ToString()
                            }
                        };                    
                    }
                    if (Jogo == null)
                    {
                        return null;
                    }
                    else
                    {
                        return Jogo;
                    }
                }
            }
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO JOGO(nomeJogo, descricao, dataLancamento, valor, idEstudio) VALUES (@nomeJogo, @descricao, @dataLancamento, @valor, @idEstudio)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM JOGO WHERE idJogo = @idJogo";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListarTodos()
        {
            List<JogoDomain> Jogo = new List<JogoDomain>();
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idJogo, nomeJogo, descricao, dataLancamento, valor, JOGO.idEstudio, nomeEstudio FROM JOGO INNER JOIN ESTUDIO ON JOGO.idEstudio = ESTUDIO.idEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain JOGO = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            nomeJogo = rdr[1].ToString(),
                            descricao = rdr[2].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[3]),
                            valor = Convert.ToSingle(rdr[4]),
                            idEstudio = Convert.ToInt32(rdr[5]),
                            estudio = new EstudioDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr[5]),
                                nomeEstudio = rdr[6].ToString()
                            }
                        };
                        Jogo.Add(JOGO);
                    }
                    if (Jogo == null)
                    {
                        return null;
                    }
                    else
                    {
                        return Jogo;
                    }
                }
            }
        }
    }
}
