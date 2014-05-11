## WMI Lab

### Synopsis

WMI Lab expands on functions offered in common WMI tools to allow for the inspection, querying, deeper interrogation and code generation of WMI classes on local or remote Windows systems.

### System Requirements

* Windows XP SP3/Windows Server 2003 SP1 or above

* .Net Framework 3.5

### Contributing

Development contributions are encouraged and welcomed.

Please see the WMI Lab project on Github (https://github.com/cavaliercoder/wmilab).

__Minimum Requirements:__

* Microsoft Visual Studio 2010 Professional

* WiX Toolset v3.8

### Known issues

* Connecting to servers in foreign authentication domains causes and RPC error and fails

* Occasionally the class list does not refresh on first load

* Value maps are not loaded for subclass results of a base class query 
  eg. the Win32_ComputerSystem result in a CIM_System query
  
### Feature Requests

* Add progress indicator for namespace/class list loading

* Add state restoration for UI controls and navigation

* Add common text functions (copy, paste, etc.) to query results, inspector, log, detail view, etc.

* Improve code documentation and naming standards

* Remove unused nuget configuration

* Improve script generators (C#, C++, C, Powershell, Perl, etc.)

* Add menu to get associators/references of linked objects (such as those in assoc/ref query results)

### Example Queries

* Grouped Event query

  `SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_Process' GROUP WITHIN 5`
  
  