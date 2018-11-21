using InjecaoDependencia;
using System;
using System.Collections.Generic;
using System.Text;
using Utilizacao.Logs;

namespace Utilizacao.Models
{
    public class Teste2
    {
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

        [Injetavel]
        public ILogErro Log { get; private set; }
    }
}
