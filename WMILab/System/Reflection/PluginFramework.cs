namespace System.Reflection
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>Provides methods useful for creating an application plugin framework.</summary>
    internal static class PluginFramework
    {
        #region Get Plugin Types

        public static Type[] GetPluginTypes(Assembly assembly, Type baseType)
        {
            List<Type> types = new List<Type>();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    if (baseType.IsInterface)
                    {
                        if (type.GetInterfaces().Contains(baseType))
                            types.Add(type);
                    }

                    else if (baseType.IsAssignableFrom(type))
                    {
                        // Add this type
                        types.Add(type);
                    }
                }
            }

            return types.ToArray();
        }

        public static Type[] GetPluginTypes<T>(Assembly assembly)
        {
            return GetPluginTypes(assembly, typeof(T));
        }

        /// <summary>Returns an array of types assignable from the specified type found in assemblies located at the given path.</summary>
        /// <param name="path">The path to a directory containing managed 'dll' files or a managed assembly file.</param>
        /// <param name="type">The <see cref="T:System.Type" /> from which returned types must be derived from. Typically an interface.</param>
        /// <returns>An array of <see cref="T:System.Type" /> objects found in at the specified path that derive from the specified <see cref="T:System.Type" />.</returns>
        public static Type[] GetPluginTypes(string path, Type baseType)
        {
            // Get assembly files
            string[] files;
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path,"*.dll");
            }
            else if (File.Exists(path))
            {
                files = new string[] { path };
            }
            else
            {
                throw new FileNotFoundException("File or directory '" + path + "' does not exist.", path);
            }

            // Create types list
            List<Type> types = new List<Type>();

            // Inspect assemblies
            foreach (string file in files)
            {
                Assembly assembly = SafeLoadAssembly(file);
                types.AddRange(GetPluginTypes(assembly, baseType));
            }

            // Return
            return types.ToArray();
        }

        /// <summary>Returns an array of types assignable from the specified type found in assemblies located in the same directory as this assembly.</summary>
        /// <param name="type">The <see cref="T:System.Type" /> from which returned types must be derived from. Typically an interface.</param>
        /// <returns>An array of <see cref="T:System.Type" /> objects located in the same directory as this assembly that derive from the specified <see cref="T:System.Type" />.</returns>
        public static Type[] GetPluginTypes(Type baseType)
        {
            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetFilePath());
            //return PluginFramework.GetPluginTypes(path, baseType);
            return PluginFramework.GetPluginTypes(Assembly.GetExecutingAssembly(), baseType);
        }

        public static Type[] GetPluginTypes<T>()
        {
            return GetPluginTypes(typeof(T));
        }

        #endregion

        #region Initialize Plugins

        public static T[] InitPlugins<T>(Type[] types)
        {
            List<T> instances = new List<T>(types.Length);
            foreach (Type type in types)
            {
                instances.Add(InitPlugin<T>(type));
            }

            return instances.ToArray();
        }

        /// <summary>Return a new instance of the specified plugin class.</summary>
        /// <typeparam name="T">The plugin class to instanciate.</typeparam>
        /// <returns>The newly instanciated plugin.</returns>
        public static T InitPlugin<T>(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        #endregion

        #region Get Plugin Instances

        public static T[] GetPluginInstances<T>(Assembly assembly)
        {
            Type[] types= GetPluginTypes<T>(assembly);
            return InitPlugins<T>(types);
        }

        public static T[] GetPluginInstances<T>(string path)
        {
            Type[] types = GetPluginTypes(path, typeof(T));
            return InitPlugins<T>(types);
        }

        /// <summary>
        /// Returns an array of class instances assignable from the specified type found in assemblies located in the same directory as this assembly.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type" /> from which returned types must be derived from. Typically an interface.</typeparam>
        /// <returns>An array of <see cref="T:System.Type" /> class instances located in the same directory as this assembly that derive from the specified <see cref="T:System.Type" />.</returns>
        public static T[] GetPluginInstances<T>()
        {
            Type[] types = GetPluginTypes(typeof(T));
            return InitPlugins<T>(types);
        }

        #endregion

        #region Safe Load Assemblies

        private static Assembly[] GetLoadedAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private static Assembly SafeLoadAssembly(string path)
        {
            foreach (Assembly assembly in GetLoadedAssemblies())
            {
                string testPath = assembly.GetFilePath();
                if (testPath == path)
                    return assembly;
            }

            return Assembly.LoadFrom(path);
        }

        #endregion
    }

    #region Extension Methods

    public static class PluginFrameworkExtensions
    {
        public static Type[] GetPluginTypes<T>(this Assembly assembly)
        {
            return PluginFramework.GetPluginTypes<T>(assembly);
        }

        public static T[] GetPluginInstances<T>(this Assembly assembly)
        {
            return PluginFramework.GetPluginInstances<T>(assembly);
        }

        public static string GetFilePath(this Assembly assembly)
        {
            // Return logical path for dynamically loaded assemblies
            if (assembly.ManifestModule is Emit.ModuleBuilder)
                return assembly.FullName;

            // File path for the rest
            return Path.GetFullPath(assembly.Location);
        }

        public static bool Contains(this Type[] types, Type i)
        {
            foreach (Type type in types)
            {
                if (type.FullName == i.FullName) return true;
            }

            return false;
        }
    }

    #endregion
}
