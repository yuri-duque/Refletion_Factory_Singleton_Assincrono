using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsultaSite
{
    class Program
    {
        static void Main(string[] args)
        {
            var clienteHttp = new HttpClient();
            var conteudo = clienteHttp.GetAsync("http://google.com.br");
            var resultado = conteudo.Result.Content.ReadAsStringAsync();

            //conteudo.Wait(); removido pois o metodo 'Result' já tem um WaitAll implementado dentro dele, 
            //fazendo com que espere o final da requisição assincrona
            Console.WriteLine(resultado.Result);

            var tarefa1 = Imprimir("Primeiro");
            var tarefa2 = Imprimir("Segundo");

            tarefa1.Wait();
            tarefa2.Wait();

            var tarefa3 = Imprimir("Terceiro");

            Console.ReadKey();
        }

        static async Task Imprimir(string texto)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                    Console.WriteLine(texto + $" - {i}");
            });
        }
    }
}
