## WMI Lab

### Synopsis

WMI Lab expands on functions offered in common WMI tools to allow for the inspection, querying, deeper interrogation and code generation of WMI classes on local or remote Windows systems.

### License

Copyright (c) 2014 Ryan Armstrong (www.cavaliercoder.com)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

The Software shall be used for Good, not Evil.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

This software incorporates the Scintilla control for syntax editing. Licensing information for Scintilla is available from http://www.scintilla.org/License.txt

This software incorporates the Scintilla control using .Net wrapper library ScintillaNet. Licensing information for ScintillaNet is available from http://scintillanet.codeplex.com/license

This software uses a custom RichTextBox control which incorporates source code written by mav.northwind and published on Code Project. The original article and license information can be found at http://www.codeproject.com/Articles/9196/Links-with-arbitrary-text-in-a-RichTextBox

This software incorporates the TreeGridView control written by Mark Rideout and published on MSDN. The original article and license information can be found at http://blogs.msdn.com/b/markrideout/archive/2006/01/08/510700.aspx

### Contributing

Development contributions are encouraged and welcomed.

Please see the WMI Lab project on Github (https://github.com/cavaliercoder/wmilab).

__Requirements:__

* Microsoft Visual Studio 2010

### Known issues

* SELECT queries with named fields (ie. not '*') fail

* Occasionally the class list does not refresh on first load

* Line endings don't appear consistently in all class and member descriptions

* Class view flickers when filtering the class list

* Value maps are not loaded for subclass results of a base class query 
  eg. the Win32_ComputerSystem result in a CIM_System query
  
### Todo

* Add progress indicator for namespace/class list loading

* Add state restoration for UI controls and navigation

* Add common text functions to query results, inspector, log, detail view, etc.

* Add about dialog

* Improve scripts

* Improve code documentation and naming

* Add WiX Installer project

* Remove nuget configuration

### Example Queries

* Grouped Event query

  `SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_Process' GROUP WITHIN 5`
  
  