using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        string stringConexao = @"Data Source=DESKTOP-CV21P6P\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=#Murillo1#";
        public void AtualizarId(int idTipoUsuario, TipoUsuarioDomain attTipoUser)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateAll = "UPDATE TIPOUSUARIO SET titulo = @titulo WHERE idTipoUsuario = @idTipoUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdateAll, con))
                {
                    cmd.Parameters.AddWithValue("@idTipoUsuario", idTipoUsuario);
                    cmd.Parameters.AddWithValue("@titulo", attTipoUser.titulo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TipoUsuarioDomain BuscarPorId(int idTipoUsuario)
        {
            TipoUsuarioDomain TipoUser = new TipoUsuarioDomain();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idTipoUsuario, titulo FROM TIPOUSUARIO WHERE idTipoUsuario = @idTipoUsuario";

                con.Open();

                SqlDataReader rdr;


                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    cmd.Parameters.AddWithValue("@idTipoUsuario", idTipoUsuario);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TipoUser = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr[0]),
                            titulo = rdr[1].ToString()
                        };

                        

                    }
                }
            }
            if (TipoUser == null)
            {

                return null;
            }



            return TipoUser;
        }

        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = @"INSERT INTO TIPOUSUARIO(titulo) VALUES (@titulo)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@titulo", novoTipoUsuario.titulo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM TIPOUSUARIO WHERE idTipoUsuario = @idTipoUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idTipoUsuario", idTipoUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TipoUsuarioDomain> ListarTodos()
        {
            List<TipoUsuarioDomain> listarTipoUser = new List<TipoUsuarioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idTipoUsuario, titulo FROM TIPOUSUARIO";



                con.Open();

                SqlDataReader rdr;


                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TipoUsuarioDomain TipoUser = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr[0]),
                            titulo = rdr[1].ToString()
                        };

                        listarTipoUser.Add(TipoUser);

                    }
                }
            }
            if (listarTipoUser == null)
            {

                return null;
            }



            return listarTipoUser;
        }
    }
}
