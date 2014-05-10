/*
 * Copyright (c) 2014 Ryan Armstrong (www.cavaliercoder.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * The Software shall be used for Good, not Evil.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */
namespace WMILab.CodeGenerators.DotNet
{
    using System;
    using System.IO;
    using System.Management;
    using System.Management.CodeGeneration;
    using System.Text;
    using System.Windows.Forms;

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

            var sb = new StringBuilder();
            switch(this.CodeLanguage)
            {
                case System.Management.CodeLanguage.VB:
                    sb.Append(this.GetVbStyleHeader());
                    break;

                default:
                    sb.Append(this.GetCStyleHeader());
                    break;
            }

            // Dump class to file
            c.GetStronglyTypedClassCode(this.CodeLanguage, tmp, "");
            sb.Append(File.ReadAllText(tmp));
            File.Delete(tmp);

            return sb.ToString();
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
