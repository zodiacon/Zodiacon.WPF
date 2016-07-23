using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zodiacon.WPF {
    public static class Extensions {
        static readonly Dictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();

        public static void LoadAssemblies(this Application app) {
            var appAssembly = app.GetType().Assembly;
            foreach(var resourceName in appAssembly.GetManifestResourceNames()) {
                if(resourceName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase)) {
                    using(var stream = appAssembly.GetManifestResourceStream(resourceName)) {
                        var assemblyData = new byte[(int)stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);
                        var assembly = Assembly.Load(assemblyData);
                        _assemblies.Add(assembly.GetName().Name, assembly);
                    }
                }
            }
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args) {
            var shortName = new AssemblyName(args.Name).Name;
            Assembly assembly;
            if(_assemblies.TryGetValue(shortName, out assembly)) {
                return assembly;
            }
            return null;
        }

    }
}
