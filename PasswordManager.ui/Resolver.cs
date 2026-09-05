using System;
using System.Linq;
using System.Reflection;

namespace PasswordManager.ui
{
    public static class Resolver
    {
        public static Assembly? AssemblyResolver(AssemblyName assemblyName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName.FullName);
        }

        public static Type? TypeResolver(Assembly? assembly, string typeName, bool b)
        {
            if (assembly is not null)
            {
                return assembly.GetType(typeName);
            }
            else
            {
                return null;
            }
        }
    }
}