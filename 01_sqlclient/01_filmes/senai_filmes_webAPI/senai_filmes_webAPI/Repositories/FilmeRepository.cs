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
    /// Classe responsável pelo repositório dos filmes
    /// </summary>
    public class FilmeRepository : IFilmeRepository
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
        public void AtualizarIdCorpo(FilmeDomain filmeAtualizado)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int idFilme, FilmeDomain filmeAtualizado)
        {
            throw new NotImplementedException();
        }

        public FilmeDomain BuscarPorId(int idFilme)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(FilmeDomain novoFilme)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO FILME (tituloFilme) VALUES (@tituloFilme)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idFilme)
        {
            throw new NotImplementedException();
        }

        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            //Declara a SqlConnection con passando a string de conexão como Parametro.
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idFilme, idGenero, tituloFilme FROM FILME";

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
                        FilmeDomain filme = new FilmeDomain()
                        {
                            //atribui a propriedade idFilme o valor da primeira coluna da tabela do banco de dados.
                            idFilme = Convert.ToInt32(rdr[0]),

                            //atribui a propriedade idGenero o valor da segunda coluna da tabela do banco de dados.
                            idGenero = Convert.ToInt32(rdr[1]),

                            //atribui a propriedade tituloFilme o valor da terceira coluna da tabela do banco de dados
                            tituloFilme = rdr[2].ToString()
                        };

                        //adiciona o objeto genero criado a lista listaGeneros
                        listaFilmes.Add(filme);
                    }
                }
            };

            return listaFilmes;
        }
    }
}
