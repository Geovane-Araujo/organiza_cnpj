using System;

namespace Cnpj_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Cnpj cnpj = new();
            Console.WriteLine("Digite o Caminho do Arquivo de Cnpj");
            String caminho = Console.ReadLine();
            Console.WriteLine("Digite o Caminho do arquivo de Log");
            String log = Console.ReadLine();
            cnpj.decodeCnpj(caminho, log);
        }
    }
}
