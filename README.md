## WMI Lab

### Synopsis
WMI Lab expands on common WMI tools to allow for inspection, querying, deeper interrogation and code generation of WMI classes on local or remote Windows systems.

### Contributing

__Requirements:__

* Microsoft .Net Framework 3.5
* Microsoft Visual Studio 2010

### Bugs

* Line ending ignored in member descriptors
* Query must be cancelled before changing to another class
* Queries fail for classes after the selected namespace changes
* Value mappings with value 0 don't translate

### Todo

* Improve error feedback for bad queries
* Add support for remote connections
* Add code generators
* Add progress indicator for namspace/class lists
* Add duration timestamps to query completion
* Add support for toggling value map translations

### Example Queries

* Grouped Event query
  SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_Process' GROUP WITHIN 5