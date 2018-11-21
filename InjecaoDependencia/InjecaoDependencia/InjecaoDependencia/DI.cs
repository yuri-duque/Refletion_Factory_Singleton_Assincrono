using System;
using System.Collections.Generic;
using System.Reflection;

namespace InjecaoDependencia
{
    public class DI
    {
        public static DI Instancia { get => instancia; }

        public void Registrar<TInterface, TClasse>() where TClasse : TInterface, new()
        {
            if (!tipos.ContainsKey(typeof(TInterface)))
                tipos.Add(typeof(TInterface), typeof(TClasse));
        }

        public TClasse Resolver<TClasse>()
        {
            if (tipos.Count == 0)
                RegistrarDependencias(Assembly.GetCallingAssembly());
            
            TClasse objetoRetorno = InstanciarObjeto<TClasse>();

            PreencherParametros(objetoRetorno);

            return objetoRetorno;
        }

        private void PreencherParametros<TClasse>(TClasse objetoRetorno)
        {
            var tipoInjetavel = typeof(InjetavelAttribute);

            foreach (var prop in objetoRetorno.GetType().GetProperties())
                if (prop.GetCustomAttribute(tipoInjetavel) != null)
                    prop.SetValue(objetoRetorno, InstanciarDoMapa(tipos[prop.PropertyType]));
        }
        
        private TClasse InstanciarObjeto<TClasse>()
        {
            var tipo = typeof(TClasse);
            var construtor = tipo.GetConstructors()[0];
            var parametros = construtor.GetParameters();
            var objetosInstanciados = new object[parametros.Length];

            for (int i = 0; i < parametros.Length; i++)
                if (tipos.ContainsKey(parametros[i].ParameterType))
                    objetosInstanciados[i] = InstanciarDoMapa(tipos[parametros[i].ParameterType]);
                else
                    throw new Exception($"Tipo '{parametros[i].ParameterType.Name}' indefinido na injeção de dependência.");
            
            return (TClasse)construtor.Invoke(objetosInstanciados);
        }

        private object InstanciarDoMapa(Type type)
        {
            return (type.GetConstructor(Type.EmptyTypes).Invoke(new object[] { }));
        }

        private void RegistrarDependencias(Assembly assembly)
        {
            var tipoDependencia = typeof(IDependencias);

            foreach (var tipo in assembly.GetTypes())
                if (tipo.IsClass && tipoDependencia.IsAssignableFrom(tipo))
                    ((IDependencias)tipo.GetConstructor(Type.EmptyTypes).Invoke(new object[] { })).Mapear();
        }

        private static Dictionary<Type, Type> tipos = new Dictionary<Type, Type>();

        private static DI instancia => new DI();

        private DI() { }
    }
}
