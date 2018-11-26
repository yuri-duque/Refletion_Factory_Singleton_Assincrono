using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Configuracao.GetInstancia();

            config.ValorXptp = 123;

            Console.ReadKey();
        }
    }
}
