using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramacaoAssincrona
{
    class Program
    {
        const int MAX = 500000;
        const int NUM_PROCS = 8;

        static void Main(string[] args)
        {
            QtdPrimosIntervalo(MAX);

            //QtdPrimosIntervaloAssincrono(MAX);

            CalcularPrimosAssincrono(MAX);

            Console.ReadKey();
        }

        static void CalcularPrimosAssincrono(int max)
        {
            var div = max / NUM_PROCS;
            int ini, fim;
            var cont = new Task<int>[NUM_PROCS];

            Console.WriteLine($"Assincrono - Início: {DateTime.Now.ToString("hh:mm:ss")}");

            for (int i = 0; i < NUM_PROCS; i++)
                cont[i] = QtdePrimosNaturaisAsync(i * div, (i+1) * div);

            Task.WaitAll(cont);

            Console.WriteLine($"  {cont.Sum(x => x.Result)} numero de primos encontrados entre 0 e {max}");

            Console.WriteLine($"Assincrono - Final: {DateTime.Now.ToString("hh:mm:ss")}\n");
        }

        static async Task<int> QtdePrimosNaturaisAsync(int ini, int fim)
        {
            int cont = 0;

            await Task.Run(() => {
                for (int i = ini; i < fim; i++)
                {
                    if (EhPrimoNatural(i))
                        cont++;
                }
            });

            return cont;
        }

        //Não seguir esse exemplo!!!!!!!!
        static void QtdPrimosIntervaloAssincrono(int max)
        {
            var resultados = new Task<bool>[max + 1];

            Console.WriteLine($"Assincrono - Início: {DateTime.Now.ToString("hh:mm:ss")}");

            for (int i = 0; i <= max; i++)
                resultados[i] = EhPrimoNaturalAsync(i);

            Task.WaitAll(resultados);

            Console.WriteLine($"  {resultados.Count(x => x.Result)} numero de primos encontrados entre 0 e {max}");

            Console.WriteLine($"Assincrono - Final:  {DateTime.Now.ToString("hh:mm:ss")}\n");
        }

        static void QtdPrimosIntervalo(int max)
        {
            int cont = 0;

            Console.WriteLine($"Sincrono - Início: {DateTime.Now.ToString("hh:mm:ss")}");

            for (int i = 0; i < max; i++)
                if (EhPrimoNatural(i))
                    cont++;

            Console.WriteLine($"  {cont} numero de primos encontrados entre 0 e {max}");

            Console.WriteLine($"Sincrono - Final:  {DateTime.Now.ToString("hh:mm:ss")}\n");
        }


        static async Task<bool> EhPrimoNaturalAsync(int num)
        {
            bool result = true;

            await Task.Run(() => {
                if (num < 2)
                    result = false;
                else if (num != 2 && num % 2 == 0)
                    result = false;
                else
                    for (int i = 3; i < num / 2; i++)
                        if (num % i == 0)
                        {
                            result = false;
                            break;
                        }
            });

            return result;
        }

        static bool EhPrimoNatural(int num)
        {
            bool result = true;

            if (num < 2)
                result = false;
            else if (num != 2 && num % 2 == 0)
                result = false;
            else
                for (int i = 3; i < num / 2; i++)
                    if (num % i == 0)
                    {
                        result = false;
                        break;
                    }

            return result;
        }
    }
}
