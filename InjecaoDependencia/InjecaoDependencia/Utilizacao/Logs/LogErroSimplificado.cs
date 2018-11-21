using System;
using System.Collections.Generic;
using System.Text;

namespace Utilizacao.Logs
{
    public class LogErroSimplificado : ILogErro
    {
        public void Logar(Exception ex)
        {
            Console.WriteLine($"Log simplificado:\n\n{ex.Message}\n---------------------------------------");
        }
    }
}
