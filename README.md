## WMI Lab
### Synopsis
WMI Lab expands on common WMI tools to allow for inspection, querying, deeper interrogation and code generation of WMI classes on local or remote Windows systems.

### Contributing
__Requirements:__

* Microsoft Visual Studio 2010

### Bugs
* Line ending ignored in member descriptors
* Class view flickers when filtering the class list

### Todo
* Add support for remote connections
* Add progress indicator for namespace/class lists
* Add state restoration for UI controls and navigation
* Add common text functions to query results, inspector, log, detail view, etc.
* Add class details to object inspector

* Add about dialog
* Add license details
* Add license details for Scintilla.Net
* Add attribution header to all files
* Improve code documentation and naming
* Add WiX Installer project

### Example Queries

* Grouped Event query

  `SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_Process' GROUP WITHIN 5`
  
  