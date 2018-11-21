using System;
using System.Collections.Generic;
using System.Text;

namespace Utilizacao.Logs
{
    public interface ILogErro
    {
        void Logar(Exception ex);
    }
}
