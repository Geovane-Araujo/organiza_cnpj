using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        public String cnae { get; set; }

        public long idempresa { get; set; }



        public void decodeCnpj(string[] caminhos, string log)
        {
            MySql.Data.MySqlClient.MySqlConnection con;
            MySql.Data.MySqlClient.MySqlCommand command;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string sql = "";
            String linha = "";
            con = AdConnection();
            int i = 0;
            StreamWriter sw = new StreamWriter(log);

            sql = "INSERT INTO empresas( cnpj, tipo, fantasia, situacao, datasituacao, motivosituacao, nomecidadeexterior, pais, inicioatividade, cnaeprincipal, tipologradouro, logradouro, numero, complemento, bairro, cep, uf, municipio, telefoneprincipal, telefonesecundario, email, situacaoespecial, datasituacaoespecial) VALUES(@cnpj,@tipo,@fantasia,@situacao,@datasituacao,@motivosituacao,@nomecidadeexterior,@pais,@inicioatividade,@cnaeprincipal,@tipologradouro,@logradouro,@numero,@complemento,@bairro,@cep,@uf,@municipio,@telefoneprincipal,@telefonesecundario,@email,@situacaoespecial,@datasituacaoespecial);";
            string sqlcnae = "INSERT INTO cnaessec( cnae, idempresa) VALUES(@cnae,@idempresa);";
            foreach (String caminho in caminhos)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(caminho);
                while ((linha = file.ReadLine()) != null)
                {
                    String[] empresa = linha.Split("\";");

                    cnpj = empresa[0].Replace("\"", "") + empresa[1].Replace("\"", "") + empresa[2].Replace("\"", "");
                    tipo = Convert.ToInt32(empresa[3].Replace("\"", ""));
                    fantasia = empresa[4].Replace("\"", "");
                    situacao = empresa[5].Replace("\"", "");
                    
                    motivosituacao = empresa[7].Replace("\"", "");
                    nomecidadeexterior = empresa[8].Replace("\"", "");
                    pais = empresa[9].Replace("\"", "");
                    
                    cnaeprincipal = empresa[11].Replace("\"", "");
                    tipologradouro = empresa[13].Replace("\"", "");
                    logradouro = empresa[14].Replace("\"", "");
                    numero = empresa[15].Replace("\"", "");
                    complemento = empresa[16].Replace("\"", "");
                    bairro = empresa[17].Replace("\"", "");
                    cep = empresa[18].Replace("\"", "");
                    uf = empresa[19].Replace("\"", "");
                    municipio = Convert.ToInt32(empresa[20].Replace("\"", ""));
                    telefoneprincipal = empresa[21].Replace("\"", "") + empresa[22].Replace("\"", "");
                    telefonesecundario = empresa[23].Replace("\"", "") + empresa[24].Replace("\"", "");
                    email = empresa[27].Replace("\"", "");
                    situacaoespecial = empresa[28].Replace("\"", "");

                    command = new MySql.Data.MySqlClient.MySqlCommand(sql, con);

                    command.Parameters.AddWithValue("@cnpj", cnpj);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@fantasia", fantasia);
                    command.Parameters.AddWithValue("@situacao", situacao);

                    if (validaDados(empresa[6].Replace("\"", "").Replace("\n", "")) == null)
                        command.Parameters.AddWithValue("@datasituacao", DBNull.Value);
                    else
                    {
                        command.Parameters.AddWithValue("@datasituacao", intToDate(empresa[6].Replace("\"", "").Replace("\n", "")));
                    }
                        

                    command.Parameters.AddWithValue("@motivosituacao", motivosituacao);
                    command.Parameters.AddWithValue("@nomecidadeexterior", nomecidadeexterior);
                    command.Parameters.AddWithValue("@pais", pais);

                    if (validaDados(empresa[10].Replace("\"", "").Replace("\n", "")) == null)
                        command.Parameters.AddWithValue("@inicioatividade", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@inicioatividade", intToDate(empresa[10].Replace("\"", "").Replace("\n", "")));

                    command.Parameters.AddWithValue("@cnaeprincipal", cnaeprincipal);
                    command.Parameters.AddWithValue("@tipologradouro", tipologradouro);
                    command.Parameters.AddWithValue("@logradouro", logradouro);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.Parameters.AddWithValue("@complemento", TrucateString(complemento,50));
                    command.Parameters.AddWithValue("@bairro", TrucateString(bairro,70));
                    command.Parameters.AddWithValue("@cep", cep);
                    command.Parameters.AddWithValue("@uf", uf);
                    command.Parameters.AddWithValue("@municipio", municipio);
                    command.Parameters.AddWithValue("@telefoneprincipal", telefoneprincipal);
                    command.Parameters.AddWithValue("@telefonesecundario", telefonesecundario);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@situacaoespecial", situacaoespecial);

                    if (validaDados(empresa[29].Replace("\"", "").Replace("\n", "")) == null)
                        command.Parameters.AddWithValue("@datasituacaoespecial", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@datasituacaoespecial", intToDate(empresa[29].Replace("\"", "").Replace("\n", "")));

                    try
                    {
                        command.ExecuteNonQuery();
                        idempresa = command.LastInsertedId;
                        command.Dispose();
                        Console.WriteLine(caminho + " " + i + " Empresa: " + fantasia);
                    }
                    catch (Exception ex)
                    {
                        sw.WriteLine(empresa.ToString());
                        Console.WriteLine(ex.Message);
                    }
                    command.Parameters.Clear();

                    foreach(String cnae in empresa[11].Replace("\"", "").Split(","))
                    {
                        command = new MySql.Data.MySqlClient.MySqlCommand(sqlcnae, con);
                        command.Parameters.AddWithValue("@cnae", cnae);
                        command.Parameters.AddWithValue("@idempresa", idempresa);
                        try
                        {
                            command.ExecuteNonQuery();
                            command.Dispose();
                        }
                        catch(Exception ex)
                        {
                            sw.WriteLine(empresa.ToString());
                            Console.WriteLine(ex.Message);
                        }
                        command.Parameters.Clear();
                    }

                    i++;
                }

                con.Close();
            } 
        }

        public Object validaDados(object dado)
        {
            if (dado.Equals("\n") || dado.Equals(""))
                dado = null;
            return dado;
        }

        public DateTime intToDate(string dataisformat)
        {
            int data = Convert.ToInt32(dataisformat);
            DateTime dt;
            if (DateTime.TryParseExact(data.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return dt;
            }
            return dt;
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
        public String TrucateString(String dado,int tamanho)
        {
            if(dado.Length > tamanho)
            {
                dado = dado.Substring(0, tamanho);
                return dado;
            }
            return dado;

        }
    }

    
}
