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
namespace WMILab.CodeGenerators
{
    using System;
    using System.Management;
    using System.Management.CodeGeneration;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Managed Object Format Specification:
    /// http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
    /// </remarks>
    public class MofCodeGenerator : ICodeGenerator
    {
        private const Int32 MAX_LINE_LEN = 76;

        public string Name
        {
            get { return "MOF Declaration Example"; }
        }

        public string Language
        {
            get { return "Managed Object Format"; }
        }

        public string FileExtension
        {
            get { return "mof"; }
        }

        public string Lexer
        {
            get { return "cpp"; }
        }

        public string GetScript(ManagementClass c, string query)
        {
            var sb = new StringBuilder();

            String classname = c.ClassPath.ClassName;
            String ns = String.Format(@"\\\\.\\{0}", c.ClassPath.NamespacePath.Replace("\\", "\\\\"));
            String quals = GetQualiferDeclaration(c.Qualifiers);
            String baseclass = c.Derivation.Count > 0 ? String.Format(" : {0}", c.Derivation[0]) : String.Empty;

            sb.AppendFormat(@"#pragma namespace(""{0}"")

{1}
class {2}{3}
{{
", ns, quals, classname, baseclass);

            var i = 0;
            foreach (var property in c.Properties)
            {
                if (i++ > 0 && property.Qualifiers.Count > 0)
                    sb.AppendLine();

                var propstring = GetPropertyDeclaration(property).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in propstring)
                    sb.AppendFormat("    {0}\r\n", line);
            }

            sb.Append("}");
            return sb.ToString();
        }

        private String GetPropertyDeclaration(PropertyData property)
        {
            var sb = new StringBuilder();

            if(property.Qualifiers.Count > 0)
                sb.AppendFormat("{0}\r\n", GetQualiferDeclaration(property.Qualifiers));

            sb.AppendFormat("{0} {1};\r\n", property.Type.ToString().ToLowerInvariant(), property.Name);
            
            return sb.ToString();
        }

        private String GetQualiferDeclaration(QualifierDataCollection qualifiers)
        {
            if (qualifiers.Count == 0)
                return String.Empty;

            int i = 0;
            var sb = new StringBuilder();
            sb.Append("[");

            int count = 0;
            foreach (var qualifier in qualifiers)
                if (qualifier.IsLocal)
                    count++;

            foreach (QualifierData qualifier in qualifiers)
            {
                if(qualifier.IsLocal) {
                    sb.Append(qualifier.Name);

                    // Append qualifer value
                    if (qualifier.Value != null)
                    {
                        if (qualifier.Value.GetType() == typeof(Boolean))
                        {
                            if (qualifier.Value == (Object)false)
                                sb.Append("(FALSE)");
                        }

                        else if (qualifier.Value.GetType().IsNumeric())
                        {
                            sb.AppendFormat(@"({0})", qualifier.Value);
                        }

                        else
                        {
                            sb.AppendFormat(@"({0})", GetQualifierStringValue(qualifier, 0));
                        }
                    }

                    if (i < (count - 1))
                        sb.Append(",\r\n ");;
                    
                    i++;
                }
            }

            sb.Append("]");
            return sb.ToString();
        }

        private String GetQualifierStringValue(QualifierData qualifier, Int32 lineoffset)
        {
            /*
             * Print each word unless it exceeds MAX_LINE_LEN
             */
            var sb = new StringBuilder();
            String[] values;

            // Expand value to a array
            if (qualifier.Value.GetType().IsArray)
                values = (String[])qualifier.Value;
            else
                values = new String[] { (String) qualifier.Value };

            // Print each value
            for (int i = 0; i < values.Length; i++)
            {
                sb.Append(@"""");
                int c = 0;
                int l = 0;

                // Escape characters
                values[i] = new StringBuilder(values[i])
                    .Replace("\r", @"\r")
                    .Replace("\n", @"\n")
                    .Replace("\"", "\"\"")
                    .ToString();
                /*
                // Escape quotes without causing a memory error
                var qsb = new StringBuilder();
                foreach (char tc in values[i])
                    if (tc == '"')
                        qsb.Append(@"""""");
                    else
                        qsb.Append(tc);

                values[i] = qsb.ToString();
                */

                // Get index of the end of the next word
                while (c < values[i].Length)
                {

                    int nextc = 1 + values[i].IndexOfAny(new char[] { ' ', ',', '.', '\r', '\n' }, c);
                    if (nextc == 0)
                        nextc = values[i].Length;

                    if (l < MAX_LINE_LEN)
                    {
                        l += nextc - c;
                        sb.Append(values[i].Substring(c, nextc - c));
                    }
                    else
                    {
                        l = nextc - c;
                        sb.AppendFormat("\"\r\n\"{0}", values[i].Substring(c, nextc - c));
                    }

                    c = nextc;
                }

                sb.Append(@"""");

                if (i < values.Length - 1)
                    sb.Append(", ");

                lineoffset = 0;
            }
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
}
