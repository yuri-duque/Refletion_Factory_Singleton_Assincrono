using System;
using Utilizacao.Logs;

namespace Utilizacao.Models
{
    public class Teste1
    {
        public Teste1(ILogErro log)
        {
            Log = log;
        }

        public void Testar()
        {
            try
            {
                int a = 5, b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                Log.Logar(ex);
            }
        }

        public ILogErro Log { get; private set; }
    }
}
