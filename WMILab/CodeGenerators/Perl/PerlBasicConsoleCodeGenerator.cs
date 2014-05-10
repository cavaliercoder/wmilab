namespace WMILab.CodeGenerators.Perl
{
    using System;
    using System.Management.CodeGeneration;
    using System.Text;
    using System.IO;
    using System.Diagnostics;

    public class PerlBasicConsoleCodeGenerator : ICodeGenerator
    {
        private const string ACTION_RUN = "Run in console";

        public string Name
        {
            get { return "Basic Console Output"; }
        }

        public string Language
        {
            get { return "Perl"; }
        }

        public string FileExtension
        {
            get { return "pl"; }
        }

        public string Lexer
        {
            get { return "perl"; }
        }

        public string GetScript(System.Management.ManagementClass c, string query)
        {
            var sb = new StringBuilder();

            sb.Append(this.GetPerlStyleHeader());

            sb.AppendFormat(@"use Data::Dumper;
use DBI;

my $dbh = DBI->connect('dbi:WMI:');
my $sth = $dbh->prepare(""{0}"");

print(""Searching for instances of {1}...\n\n"");
$sth->execute();
while (my @row = $sth->fetchrow) {{
", query, c.ClassPath.ClassName);

            sb.Append("    print(\"Instance: $row[0]->{ Path_ }->{ RelPath }\\n\");\r\n");
            sb.Append("    print(\"===============================================================================\\n\");\r\n");

            foreach (var property in c.Properties)
            {
                sb.AppendFormat("    print(\"{0}: $row[0]->{{ {0}  }}\\n\");\r\n", property.Name);
            }

            sb.Append("    print(\"\\n\");\r\n}");

            return sb.ToString();
        }

        public CodeGeneratorAction[] GetActions(System.Management.ManagementClass c, string query)
        {
            return new CodeGeneratorAction[] {
                new CodeGeneratorAction {
                    Name = ACTION_RUN,
                    Image = Properties.Resources.Execute
                }
            };
        }

        public int ExecuteAction(CodeGeneratorAction action, System.Management.ManagementClass c, string query)
        {
            if (action.Name.Equals(ACTION_RUN))
            {
                String script = this.GetScript(c, query);
                String path = Path.GetTempFileName().Replace(".tmp", "." + this.FileExtension);
                File.WriteAllText(path, script);

                //ConsoleForm.StartProcess(null, "cscript", path);
                Process process = new Process();
                ProcessStartInfo start = new ProcessStartInfo("cmd.exe", String.Format("/K perl \"{0}\"", path));

                process.StartInfo = start;
                process.Start();

                return 0;
            }

            return 1;
        }
    }
}
