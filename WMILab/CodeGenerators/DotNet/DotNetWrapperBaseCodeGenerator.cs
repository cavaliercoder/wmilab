namespace WMILab.CodeGenerators.DotNet
{
    using System;
    using System.IO;
    using System.Management;
    using System.Management.CodeGeneration;

    public abstract class DotNetWrapperBaseCodeGenerator : ICodeGenerator
    {
        protected abstract CodeLanguage CodeLanguage { get; }

        public string Name
        {
            get { return "Strongly Typed Class Wrapper"; }
        }

        public string Language
        {
            get
            {
                switch (this.CodeLanguage)
                {
                    case CodeLanguage.CSharp:
                        return "C#";

                    case CodeLanguage.JScript:
                        return "JavaScript";

                    case CodeLanguage.Mcpp:
                        return "Managed C++";

                    case CodeLanguage.VB:
                        return "Visual Basic.Net";

                    case CodeLanguage.VJSharp:
                        return "Visual J#";

                    default:
                        return "Unknown";
                }

            }
        }

        public string FileExtension
        {
            get
            {
                switch (this.CodeLanguage)
                {
                    case CodeLanguage.CSharp:
                        return "cs";

                    case CodeLanguage.JScript:
                        return "js";

                    case CodeLanguage.Mcpp:
                        return "cpp";

                    case CodeLanguage.VB:
                        return "vb";

                    case CodeLanguage.VJSharp:
                        return "vjs";

                    default:
                        return "Unknown";
                }
            }
        }

        public string Lexer
        {
            get
            {
                switch (this.CodeLanguage)
                {
                    case System.Management.CodeLanguage.CSharp:
                        return "cs";

                    case System.Management.CodeLanguage.VB:
                        return "vbscript";

                    case System.Management.CodeLanguage.Mcpp:
                        return "cpp";

                    default:
                        return String.Empty;
                }
            }
        }

        public String GetScript(ManagementClass c, String query)
        {
            // Create temp file
            string tmp = Path.GetTempFileName();

            // Dump class to file
            c.GetStronglyTypedClassCode(this.CodeLanguage, tmp, "");
            string code = File.ReadAllText(tmp);
            File.Delete(tmp);

            return code;
        }

        public CodeGeneratorAction[] GetActions(ManagementClass c, string query)
        {
            return new CodeGeneratorAction[] { };
        }

        public int ExecuteAction(CodeGeneratorAction action, ManagementClass c, string query)
        {
            return 0;
        }
    }

    public class CsWrapperCodeGenerator : DotNetWrapperBaseCodeGenerator, ICodeGenerator
    {
        protected override CodeLanguage CodeLanguage
        {
            get
            {
                return CodeLanguage.CSharp;
            }
        }
    }

    public class VbNetWrapperCodeGenerator : DotNetWrapperBaseCodeGenerator, ICodeGenerator
    {
        protected override CodeLanguage CodeLanguage
        {
            get
            {
                return CodeLanguage.VB;
            }
        }
    }

    public class CppNetWrapperCodeGenerator : DotNetWrapperBaseCodeGenerator, ICodeGenerator
    {
        protected override CodeLanguage CodeLanguage
        {
            get
            {
                return CodeLanguage.Mcpp;
            }
        }
    }
}
