using System;

namespace Cnpj_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Cnpj cnpj = new();
            Console.WriteLine("Digite a quantidade de Arquivos a Serer Organizados");
            int qtd = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o Caminho do arquivo de Log");
            String log = Console.ReadLine();
            String[] caminhos = new string[qtd];
            for (int i = 0; i < qtd; i++)
            {
                Console.Out.Flush();
                Console.WriteLine("Digite o Caminho do Arquivo de Cnpj");
                String caminho = Console.ReadLine();
                caminhos[i] = caminho;
            }
            // caminhos[0] = "C://Dados//0.ESTABELE";

            cnpj.decodeCnpj(caminhos, log);
        }
    }
}
