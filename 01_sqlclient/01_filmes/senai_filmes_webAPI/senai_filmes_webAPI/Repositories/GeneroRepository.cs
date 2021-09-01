using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Repositories
{
    /// <summary>
    /// Classe responsável pelo repositório dos gêneros
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros.
        /// Data Source = Nome do Servidor
        /// initial catalog = Nome do Banco de Dados
        /// user ID = sa; pwd = senai@132 = Faz autenticação com o SQL SERVER passando o Login e Senha
        /// integrated security = true = Faz autenticação com o usuário do sistema (Windows)
        /// </summary>
        private string stringConexao = "Data Source=LAPTOP-GBJVH1HS\\SQLEXPRESS; initial catalog=CATALOGO_T; user Id=sa; pwd=senai@132";

        //private string stringConexao = @"Data Source=LAPTOP-GBJVH1HS\SQLEXPRESS; initial catalog=catalogo_tarde; integrated security=true";
        public void AtualizarIdCorpo(GeneroDomain generoAtualizado)
        {
            if (generoAtualizado.nomeGenero != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE GENERO SET nomeGenero = @nomeGenero WHERE idGenero = @idGenero";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@nomeGenero", generoAtualizado.nomeGenero);
                        cmd.Parameters.AddWithValue("@idGenero", generoAtualizado.idGenero);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AtualizarIdUrl(int idGenero, GeneroDomain generoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE GENERO SET nomeGenero = @nomeGenero WHERE idGenero = @idGenero";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@nomeGenero", generoAtualizado.nomeGenero);
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public GeneroDomain BuscarPorId(int idGenero)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeGenero, idGenero FROM GENERO WHERE idGenero = @idGenero";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        GeneroDomain generoBuscado = new GeneroDomain
                        {
                            idGenero = Convert.ToInt32(reader["idGenero"]),

                            nomeGenero = reader["nomeGenero"].ToString()
                        };

                        return generoBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(GeneroDomain novoGenero)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                //"INSERT INTO GENERO (nomeGenero) VALUES ('teste')"
                //"INSERT INTO GENERO (nomeGenero) VALUES ('Joana D'Arc')"              // erro de sintaxe, com o efeito Joana DÁrc
                //"INSERT INTO GENERO (nomeGenero) VALUES ('')DROP TABLE FILME--')"     // permite SQL Injection
                //string queryInsert = "INSERT INTO GENERO (nomeGenero) VALUES ('" + novoGenero.nomeGenero + "')";

                string queryInsert = "INSERT INTO GENERO (nomeGenero) VALUES (@nomeGenero)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeGenero", novoGenero.nomeGenero);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idGenero)
        {
           using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM GENERO WHERE idGenero = @idGenero";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<GeneroDomain> ListarTodos()
        {
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            //Declara a SqlConnection con passando a string de conexão como Parametro.
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idGenero, nomeGenero FROM GENERO";

                //Abre a conexão com o banco de dados.
                con.Open();

                //Declarando SqlDataReader rdr percorrer a tabela do nosso banco de dados.
                SqlDataReader rdr;

                //Declara o SqlCommand passando da query que será executada e a conexão com parametros.
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //executa a query e armazena os dados no rdr.
                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registros para serem lidos no rdr, o laço se repete.
                    while (rdr.Read())
                    {
                        //instacia um objeto genero do tipo GeneroDomain
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //atribui a propriedade idGenero o valor da primeira coluna da tabela do banco de dados.
                            idGenero = Convert.ToInt32(rdr[0]),

                            //atribui a propriedade nomeGenero o valor da segunda coluna da tabela do banco de dados
                            nomeGenero = rdr[1].ToString()
                        };

                        //adiciona o objeto genero criado a lista listaGeneros
                        listaGeneros.Add(genero);
                    }
                }
            };

            return listaGeneros;
        }
    }
}
