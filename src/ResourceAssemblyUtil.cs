using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceAssembly
{
    class ResourceAssemblyUtil
    {
        /// <summary>
        /// The path of assemblies in the resources. (Need to modify as require)
        /// </summary>
        private static readonly string[] ResourceAssemblyFiles = new string[] { };

        private static Dictionary<string, Assembly> ResourceLoadedAssemblies { get; set; }

        static ResourceAssemblyUtil()
        {
            ResourceLoadedAssemblies = new Dictionary<string, Assembly>();

            foreach(string name in ResourceAssemblyFiles)
            {
                Uri uri = new Uri(name, UriKind.Relative);
                using (Stream stream = Application.GetResourceStream(uri).Stream)
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    Assembly assembly = Assembly.Load(buffer);
                    string key = assembly.GetName().FullName;
                    ResourceLoadedAssemblies.Add(key, assembly);
                }
            }
        }

        /// <summary>
        /// Try to load the specific assembly from resources.
        /// </summary>
        /// <param name="name">The full name fo the assembly</param>
        /// <returns>The requested assembly, </returns>
        public static Assembly LoadResourceAssembly(string name)
        {
            if (ResourceLoadedAssemblies.ContainsKey(name))
                return ResourceLoadedAssemblies[name];
            return null;
        }

        /// <summary>
        /// Hendler for AssemblyResolve event
        /// Run this line of code to use this utility (as earlier as possible, recommended in App's constructor)
        ///     AppDomain.CurrentDomain.AssemblyResolve += ResourceAssemblyUtil.AssemblyResolveHandler;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args)
        {
            return LoadResourceAssembly(args.Name);
        }
    }
}
