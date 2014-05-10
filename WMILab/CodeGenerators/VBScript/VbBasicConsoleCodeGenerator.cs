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
namespace WMILab.CodeGenerators.VBScript
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Management;
    using System.Management.CodeGeneration;
    using System.Text;
    using System.Windows.Forms;

    public class VbBasicConsoleCodeGenerator : ICodeGenerator
    {
        private const string ACTION_RUN = "Run in console";

        public VbBasicConsoleCodeGenerator()
        {
            this.ShowComments = true;
            this.PrintUnitTypes = true;
            this.PrintTranlatedValues = true;
        }

        public string Name
        {
            get { return "Basic console output"; }
        }

        public string Language
        {
            get { return "VB Script"; }
        }

        public string FileExtension
        {
            get { return "vbs"; }
        }

        public string Lexer
        {
            get { return "vbscript"; }
        }

        public string GetScript(System.Management.ManagementClass c, string query)
        {StringBuilder s = new StringBuilder();
            var lookups = new Dictionary<String, PropertyDataValueMap>();
            
            bool isEvent = c.IsEvent();
            bool hasArrays = false;
            
            string escapedQuery = query.ToString().Replace("\"", "\"\"");
            
            string[] comments = new string[] {
                "' Connect to WMI service\r\n",
                "' Start Watching for Instances of this Class\r\n",
                "' Loop indefinately to keep the script alive so callbacks can be processed\r\n",
                "' This routine is callby objWMIService when an instance of this Class is being raised\r\n",
                "' Print each query result\r\n",
                "' This routine is called by objWMIService when (if) the Notification Listener closes\r\n",
                "' Execute WMI query to return instances of this class\r\n",
                "' Returns members of an array as a line separated string\r\n"
            };

            if(!this.ShowComments)
                for(int i = 0; i < comments.Length; i++)
                    comments[i] = String.Empty;

            s.AppendFormat(this.GetVbStyleHeader());

            s.AppendLine("Option Explicit");

            if(this.IgnoreErrors)
                s.AppendLine(@"On Error Resume Next");

            if (this.EnableRemoteConnections)
            {
                s.AppendFormat(@"
Const strServer = ""{0}"" 'Change strServer to query a remote server
Const strUserName = ""{1}"" 'Make sure you provide credentials for remote servers
Const strPassWord = ""ABC123""

Dim objWMIService, objLocator, objCallBack, objInstances, objInstance

{2}WScript.Echo ""Connecting to WMI Service...""
Set objLocator = CreateObject(""WbemScripting.SWbemLocator"") 'Used to create a connection to the WMI Server
If strServer = ""."" Or LCase(strServer) = ""localhost"" Then
	Set objWMIService = objLocator.ConnectServer(strServer, ""{3}"") 'No credentials for local connections
Else
	Set objWMIService = objLocator.ConnectServer(strServer, ""{3}"", strUserName, strPassWord) 'Secure remote connection
End If

", c.Scope.Path.Server, c.Scope.Options.Username, comments[0], c.Scope.Path.NamespacePath);
            }

            else
            {
                s.AppendFormat(@"
Dim objWMIService, objCallBack, objInstances, objInstance
Set objWMIService = GetObject(""winmgmts:{{impersonationLevel=impersonate}}!\\localhost\{0}"")

", c.Scope.Path.NamespacePath);
            }
            
            if(isEvent)
            {
                s.AppendFormat(
 @"Set objCallBack = WScript.CreateObject(""WbemScripting.SWbemSink"",""CallBack_"") 'Create a Call Back pointer for Event Notifications to call.
{2}
WScript.Echo ""Watching for Instances of {0}...""
WScript.Echo ""(Press Ctrl+C to cancel.)""
objWMIService.ExecNotificationQueryAsync objCallBack, ""{1}""

{3}Do
	wscript.sleep(1000) 'in ms. 1 CPU cycle per x'ms
Loop

{4}Sub CallBack_OnObjectReady(objInstance, objAsyncContext)
	WScript.Echo ""Instance :"" & objInstance.Path_.Relpath
	WScript.Echo ""===============================================================================""
", c.Path.ClassName, escapedQuery, comments[1], comments[2], comments[3]);
            } 

            else {
                s.AppendFormat(@"{2}WScript.Echo ""Searching for Instances of {0}...""
Set objInstances = objWMIService.ExecQuery(""{1}"") 'Search for instances
WScript.Echo ""Found "" & objInstances.Count & "" results:""
WScript.Echo

{3}For Each objInstance in objInstances
    WScript.Echo ""Instance: "" & objInstance.Path_.Relpath
    WScript.Echo ""===============================================================================""
", c.Path.ClassName, escapedQuery, comments[6], comments[4]);
            }

            foreach(PropertyData p in c.Properties)
            {
                bool addConvertor = false;

                if (this.PrintTranlatedValues)
                {
                    var map = p.GetValueMap();
                    if(map.Count > 0 && !lookups.ContainsKey(p.Name))
                    {
                        lookups.Add(p.Name, map);
                        addConvertor = true;
                    }
                }

                if (this.ShowDocumentation)
                {
                    string[] description = p.GetDescription().Split(new char[] { '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                    if (description.Length > 0)
                    {
                        s.AppendLine();
                        foreach (string line in description)
                            s.AppendFormat("    '{0}\r\n", line);
                    }
                }

                if (addConvertor)
                {
                    s.AppendFormat("    WScript.Echo \"{0}: \" & Lookup{0}(objInstance.{0})", p.Name);
                }

                else if (p.IsArray)
                {
                    hasArrays = true;
                    s.AppendFormat("    WScript.Echo \"{0}: \" & ExpandArray(objInstance.{0})", p.Name);
                }

                else
                {
                    s.AppendFormat("    WScript.Echo \"{0}: \" & objInstance.{0}", p.Name);
                }

                if(this.PrintUnitTypes && p.Qualifiers.Exists("Units"))
                        s.AppendFormat(" & \" {0}\"", p.Qualifiers["Units"].Value);
                
                s.Append("\r\n");
            }

            if(isEvent)
            {
                s.AppendFormat(@"    WScript.Echo
End Sub

{0}Sub CallBack_OnCompleted(objObject, objAsyncContext)
    WScript.Echo ""Watcher Closed.""
	{1}WScript.Quit
End Sub", comments[5], (this.ConfirmExit ? "WScript.StdIn.ReadLine\r\n\t" : String.Empty));
            }

            else 
            {
                s.Append(@"    WScript.Echo
Next");

                if(this.ConfirmExit)
                    s.Append(@"

WScript.StdOut.Write ""Press Enter to Exit...""
WScript.StdIn.ReadLine 'Pause");
            }

            // Add array expander
            if (hasArrays)
            {
                s.AppendFormat(@"

{0}Function ExpandArray(array)
	Dim i
	If IsNull(array) Then
		ExpandArray = """"
		Exit Function
    Else
	    ExpandArray = ""Array["" & (UBound(array) + 1) & ""]""
	    For i = 0 To UBound(array)
		    ExpandArray = ExpandArray & VbCrLf & ""  "" & array(i)
	    Next
	End If
End Function", comments[7]);
            }

            // Add Lookup functions
            if (lookups.Count > 0)
            {
                foreach (string field in lookups.Keys)
                {
                    var map = lookups[field];
                    List<String> values = new List<string>(map.Count);
                    List<string> mappings = new List<string>(map.Count);
                    foreach (string key in map.Keys)
                    {
                        values.Add(key);
                        mappings.Add(String.Format("{0} ({1})", map[key], key));
                    }
                    string strValues = c.Properties[field].IsNumeric() ?
                        String.Join(", ", values.ToArray()) :
                        String.Format("\"{0}\"", String.Join("\", \"", values.ToArray()));

                    string strMappings = String.Join("\", \"", mappings.ToArray());
                    string description = this.ShowComments ? String.Format("'Returns the value map translation of a '{0}.{1}' member\r\n", c.ClassPath.ClassName, field) : String.Empty;

                    s.AppendFormat(@"

{3}Function Lookup{0}(key)
    Dim Keys, Values, i
	Keys = Array({1})
	Values = Array(""{2}"")
	For i = 0 To UBound(Keys)
		If Keys(i) = key Then
			Lookup{0} = Values(i)
			Exit Function
		End If
	Next
	Lookup{0} = key
End Function", field, strValues, strMappings, description);
                }
            }

            return s.ToString();
        }

        public CodeGeneratorAction[] GetActions(ManagementClass c, string query)
        {
            return new CodeGeneratorAction[] {
                new CodeGeneratorAction {
                    Name = ACTION_RUN,
                    Image = Properties.Resources.Execute
                }
            };
        }

        public int ExecuteAction(CodeGeneratorAction action, ManagementClass c, String query)
        {
            if (action.Name.Equals(ACTION_RUN))
            {
                String script = this.GetScript(c, query);
                String path = Path.GetTempFileName().Replace(".tmp", "." + this.FileExtension);
                File.WriteAllText(path, script);

                //ConsoleForm.StartProcess(null, "cscript", path);
                Process process = new Process();
                ProcessStartInfo start = new ProcessStartInfo("cmd.exe", String.Format("/K cscript /nologo \"{0}\"", path));

                process.StartInfo = start;
                process.Start();

                return 0;
            }

            return 1;
        }

        #region Options

        [CodeGeneratorOption("Show comments", "Show code comments to describe the actions taking place.")]
        public Boolean ShowComments { get; set; }

        [CodeGeneratorOption("Show class documentation", "Show member documentation source from 'Description' qualifiers as code comments.")]
        public Boolean ShowDocumentation { get; set; }

        [CodeGeneratorOption("Enable remote connections", "Enable connections to remote hosts.")]
        public Boolean EnableRemoteConnections { get; set; }

        [CodeGeneratorOption("Print translated values", "Print translated value for members which include a value map qualifier.")]
        public Boolean PrintTranlatedValues { get; set; }

        [CodeGeneratorOption("Print unit types", "Print the unit type for member which include a unit type qualifier.")]
        public Boolean PrintUnitTypes { get; set; }

        [CodeGeneratorOption("Ignore errors", "Ignore script errors and continue execution.")]
        public Boolean IgnoreErrors { get; set; }

        [CodeGeneratorOption("Confirm exit", "Prevent the window from closing after the script has completed.")]
        public Boolean ConfirmExit { get; set; }

        #endregion
    }
}
