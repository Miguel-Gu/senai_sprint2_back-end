using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        string stringConexao = @"Data Source=DESKTOP-CV21P6P\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=#Murillo1#";
        public void AtualizarId(int idUsuario, UsuarioDomain attUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateIdURL = "UPDATE USUARIO SET senha = @novaSenha WHERE idUsuario = @idUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdURL, con))
                {
                    cmd.Parameters.AddWithValue("@novaSenha", attUsuario.senha);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain BuscarPorId(int idUsuario)
        {
            UsuarioDomain User = new UsuarioDomain();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idUsuario, email, U.idTipoUsuario, titulo FROM USUARIO U INNER JOIN TIPOUSUARIO TU ON U.idTipoUsuario = TU.idTipoUsuario WHERE idUsuario = @idUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    rdr = cmd.ExecuteReader();


                    if (rdr.Read())
                    {
                        User = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            email = rdr[1].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr[2]),
                            tipoUsuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(rdr[2]),
                                titulo = rdr[3].ToString()
                            }
                        };
                        return User;
                    }
                    else
                    {

                        return null;
                    }



                    

                }
            }
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = @$"INSERT INTO USUARIO (email, senha, idTipoUsuario)
                                        VALUES(@email, @senha, @idTipoUsuario)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senha", novoUsuario.senha);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
            
           
        }

        public void Deletar(int idUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM USUARIO WHERE idUsuario = @idUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<UsuarioDomain> ListarTodos()
        {
            List<UsuarioDomain> listarUser = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idUsuario, email, U.idTipoUsuario, titulo FROM USUARIO U INNER JOIN TIPOUSUARIO TU ON U.idTipoUsuario = TU.idTipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        UsuarioDomain USUARIO = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            email = rdr[1].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr[2]),
                            tipoUsuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(rdr[2]),
                                titulo = rdr[3].ToString()
                            }
                        };
                        listarUser.Add(USUARIO);
                    }
                    if (listarUser == null)
                    {

                        return null;
                    }



                    return listarUser;

                }
            }
        }

        public UsuarioDomain Login(string email, string senha)
        {
            UsuarioDomain usuarioBuscado = new UsuarioDomain();
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryLogin = "SELECT * FROM USUARIO INNER JOIN TIPOUSUARIO ON USUARIO.idTipoUsuario = TIPOUSUARIO.idTipoUsuario WHERE email = @email and senha = @senha";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryLogin, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        usuarioBuscado = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            email = rdr[1].ToString(),
                            senha = rdr[2].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr[3]),
                            tipoUsuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(rdr[3]),
                                titulo = rdr[5].ToString()
                            }
                        };
                        return usuarioBuscado;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
