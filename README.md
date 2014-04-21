## WMI Lab
### Synopsis
WMI Lab expands on functions offered in common WMI tools to allow for the inspection, querying, deeper interrogation and code generation of WMI classes on local or remote Windows systems.

### License
Copyright (c) 2014 Ryan Armstrong (www.cavaliercoder.com)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

The Software shall be used for Good, not Evil.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

### Contributing
Development contributions are encouraged and welcomed.
Please see the WMI Lab project on Github (https://github.com/cavaliercoder/wmilab).

__Requirements:__

* Microsoft Visual Studio 2010

### Bugs

* Line endings don't appear consistently in all class and member descriptions

* Class view flickers when filtering the class list

* Value maps are not loaded for subclass results of a base class query 
  eg. the Win32_ComputerSystem result in a CIM_System query
  
### Todo

* Add support for remote connections

* Add progress indicator for namespace/class list loading

* Add state restoration for UI controls and navigation

* Add common text functions to query results, inspector, log, detail view, etc.

* Add about dialog

* Add license details

* Add license details for Scintilla.Net

* Add attribution header to all files

* Improve code documentation and naming

* Add WiX Installer project

### Example Queries

* Grouped Event query

  `SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_Process' GROUP WITHIN 5`
  
  