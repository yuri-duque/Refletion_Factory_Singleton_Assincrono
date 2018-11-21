using System;
using System.Collections.Generic;
using System.Text;

namespace Utilizacao.Logs
{
    public class LogErroCompleto : ILogErro
    {
        public void Logar(Exception ex)
        {
            Console.WriteLine($"Log completo:\n\n{ex.Message}\n\n{ex.StackTrace}\n---------------------------------------");
        }
    }
}
