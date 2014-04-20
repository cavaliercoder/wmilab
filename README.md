## WMI Lab
### Synopsis
WMI Lab expands on common WMI tools to allow for inspection, querying, deeper interrogation and code generation of WMI classes on local or remote Windows systems.

### Contributing
__Requirements:__

* Microsoft Visual Studio 2010

### Bugs
* Line ending ignored in member descriptors
* Query must be cancelled before changing to another class
* Queries fail for classes after the selected namespace changes
* Value mappings with value 0 don't translate (eg. Win32_CacheMemory.Location)
* System class filter button does nothing
* Class view flickers when filtering the class list

### Todo
* Improve error feedback for bad queries
* Add support for remote connections
* Add code generators
* Add progress indicator for namspace/class lists
* Add state restoration for UI controls and navigation
* Add common text functions to query results, inspector, log, detail view, etc.
* Improve font colors in code editor

* Add about dialog
* Add license details
* Add license details for Scintilla.Net
* Add attribution header to all files
* Improve code documentation and naming
* Add WiX Installer project

### Example Queries

* Grouped Event query

  `SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_Process' GROUP WITHIN 5`
  
  