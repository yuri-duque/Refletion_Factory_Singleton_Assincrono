using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public class Configuracao
    {
        public static Configuracao GetInstancia()
        {
            if (instancia == null)
                instancia = new Configuracao();

            return instancia;
        }

        private Configuracao() { }

        public int ValorXptp { get; set; }

        private static Configuracao instancia = null;
    }
}
