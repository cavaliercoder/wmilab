namespace System.Management.CodeGeneration
{
    using System.Reflection;

    public static class CodeGeneratorFactory
    {
        private static ICodeGenerator[] codeGenerators;

        public static ICodeGenerator[] CodeGenerators
        {
            get
            {
                if (codeGenerators == null)
                    codeGenerators = PluginFramework.GetPluginInstances<ICodeGenerator>();

                return codeGenerators;
            }
        }
    }
}
