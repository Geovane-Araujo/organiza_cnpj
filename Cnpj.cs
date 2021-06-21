using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cnpj_v2
{
    class Cnpj
    {



        public int id { get; set; }

        public String cnpj { get; set; }

        public int tipo { get; set; }

        public String fantasia { get; set; }

        public String situacao { get; set; }

        public DateTime datasituacao { get; set; }

        public String motivosituacao { get; set; }

        public String nomecidadeexterior { get; set; }

        public String pais { get; set; }

        public DateTime inicioatividade { get; set; }

        public String cnaeprincipal { get; set; }

        public String tipologradouro { get; set; }

        public String logradouro { get; set; }

        public String numero { get; set; }

        public String complemento { get; set; }

        public String bairro { get; set; }

        public String cep { get; set; }

        public String uf { get; set; }

        public int municipio { get; set; }

        public String telefoneprincipal { get; set; }

        public String telefonesecundario { get; set; }

        public String email { get; set; }

        public String situacaoespecial { get; set; }

        public DateTime datasituacaoespecial { get; set; }
        public void decodeCnpj(string caminho, string log)
        {
            MySql.Data.MySqlClient.MySqlConnection con;
            MySql.Data.MySqlClient.MySqlCommand command;
            string sql = "";
            try
            {

                con = AdConnection();
                String linha = "";
                sql = "INSERT INTO empresas( cnpj, tipo, fantasia, situacao, datasituacao, motivosituacao, nomecidadeexterior, pais, inicioatividade, cnaeprincipal, tipologradouro, logradouro, numero, complemento, bairro, cep, uf, municipio, telefoneprincipal, telefonesecundario, email, situacaoespecial, datasituacaoespecial) VALUES(@cnpj,@tipo,@fantasia,@situacao,@datasituacao,@motivosituacao,@nomecidadeexterior,@pais,@inicioatividade,@cnaeprincipal,@tipologradouro,@logradouro,@numero,@complemento,@bairro,@cep,@uf,@municipio,@telefoneprincipal,@telefonesecundario,@email,@situacaoespecial,@datasituacaoespecial);";

                System.IO.StreamReader file = new System.IO.StreamReader(caminho);
                while ((linha = file.ReadLine()) != null)
                {
                    String[] empresa = linha.Split("\";");
                    cnpj = empresa[0];
                    command = new MySql.Data.MySqlClient.MySqlCommand(sql, con);
                    command.Parameters.AddWithValue("@cnpj", cnpj);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@fantasia", fantasia);
                    command.Parameters.AddWithValue("@situacao", situacao);
                    command.Parameters.AddWithValue("@datasituacao", datasituacao);
                    command.Parameters.AddWithValue("@motivosituacao", motivosituacao);
                    command.Parameters.AddWithValue("@nomecidadeexterior", nomecidadeexterior);
                    command.Parameters.AddWithValue("@pais", pais);
                    command.Parameters.AddWithValue("@inicioatividade", inicioatividade);
                    command.Parameters.AddWithValue("@cnaeprincipal", cnaeprincipal);
                    command.Parameters.AddWithValue("@tipologradouro", tipologradouro);
                    command.Parameters.AddWithValue("@logradouro", logradouro);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.Parameters.AddWithValue("@complemento", complemento);
                    command.Parameters.AddWithValue("@bairro", bairro);
                    command.Parameters.AddWithValue("@cep", cep);
                    command.Parameters.AddWithValue("@uf", uf);
                    command.Parameters.AddWithValue("@municipio", municipio);
                    command.Parameters.AddWithValue("@telefoneprincipal", telefoneprincipal);
                    command.Parameters.AddWithValue("@telefonesecundario", telefonesecundario);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@situacaoespecial", situacaoespecial);
                    command.Parameters.AddWithValue("@datasituacaoespecial", datasituacaoespecial);
                    command.ExecuteNonQuery();

                    command.Dispose();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public MySql.Data.MySqlClient.MySqlConnection AdConnection()
        {
            MySql.Data.MySqlClient.MySqlConnection con;
            try
            {

                var connString = "Server=localhost;Database=adonais1_cnpj;Uid=root;Pwd=1816;";
                con = new MySql.Data.MySqlClient.MySqlConnection(connString);
                con.Open();
                Console.WriteLine("Deu Certo A Conexão");
                return con;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    
}
