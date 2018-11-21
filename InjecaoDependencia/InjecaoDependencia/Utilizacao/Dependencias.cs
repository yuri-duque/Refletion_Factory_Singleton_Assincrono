using InjecaoDependencia;
using System;
using System.Collections.Generic;
using System.Text;
using Utilizacao.Logs;

namespace Utilizacao
{
    class Dependencias : IDependencias
    {
        public void Mapear()
        {
            DI.Instancia.Registrar<ILogErro, LogErroCompleto>();            
        }
    }
}
