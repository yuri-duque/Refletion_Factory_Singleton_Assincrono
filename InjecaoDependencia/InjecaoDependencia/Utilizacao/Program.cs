using InjecaoDependencia;
using System;
using System.Text;
using Utilizacao.Models;

namespace Utilizacao
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var p1 = new Produto() { Codigo = 01, Nome = "Produto 01", Preco = 10.5 };

            //var propString = Serializar(p1);

            //Console.WriteLine(propString);

            //var p2 = Deserializar<Produto>(propString);

            //Console.WriteLine($"Deserializado = {p2.Codigo} - {p2.Nome} - {p2.Preco}\n");

            var teste1 = DI.Instancia.Resolver<Teste1>();
            var teste2 = DI.Instancia.Resolver<Teste2>();

            teste1.Testar();
            teste2.Testar();

            Console.WriteLine("Fim do programa");
            Console.ReadKey();
        }

        private static string Serializar(Object obj)
        {
            var sb = new StringBuilder();

            if(obj != null){
                foreach (var prop in obj.GetType().GetProperties())
                {
                    sb.Append($"{prop.Name}:{prop.GetValue(obj)}\n");
                }
            }

            return sb.ToString();
        }

        private static T Deserializar<T>(string objSerializado) where T: new()
        {
            var obj = new T();
            var porpsString = objSerializado.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            foreach(var proStr in porpsString)
            {
                var elementos = proStr.Split(':');
                var prop = obj.GetType().GetProperty(elementos[0]);

                if(prop.PropertyType == typeof(int))
                    prop.SetValue(obj, Convert.ToInt32(elementos[1]));
                else if (prop.PropertyType == typeof(string))
                    prop.SetValue(obj, elementos[1]);
                else if (prop.PropertyType == typeof(double))
                    prop.SetValue(obj, Convert.ToDouble(elementos[1]));
            }

            return obj;
        }

        public class Produto
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public double Preco { get; set; }
        } 
    }
}
