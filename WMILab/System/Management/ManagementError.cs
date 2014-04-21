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
namespace System.Management
{
    /// <summary>
    /// Describes the enumeration of all WMI error constant codes that are currently defined.
    /// </summary>
    /// <remarks>
    /// Wmi error constants: http://msdn.microsoft.com/en-us/library/aa394559(v=vs.85).aspx
    /// Wmi non-error constants: http://msdn.microsoft.com/en-us/library/aa394565(v=vs.85).aspx
    /// Common result constants: http://msdn.microsoft.com/en-us/library/windows/desktop/aa378137(v=vs.85).aspx
    /// </remarks>
    public enum ManagementError : uint
    {
        /*
         * Common Windows error constants
         */

        /// <summary>Operation successful</summary>
        S_OK = 0x00000000,

        /// <summary>Not implemented</summary>
        E_NOTIMPL = 0x80004001,

        /// <summary>No such interface supported</summary>
        E_NOINTERFACE = 0x80004002,

        /// <summary>Pointer that is not valid</summary>
        E_POINTER = 0x80004003,

        /// <summary>Operation aborted</summary>
        E_ABORT = 0x80004004,

        /// <summary>Unspecified failure</summary>
        E_FAIL = 0x80004005,

        /// <summary>Unexpected failure</summary>
        E_UNEXPECTED = 0x8000FFFF,

        /// <summary>General access denied error</summary>
        E_ACCESSDENIED = 0x80070005,

        /// <summary>Handle that is not valid</summary>
        E_HANDLE = 0x80070006,

        /// <summary>Failed to allocate necessary memory</summary>
        E_OUTOFMEMORY = 0x8007000E,

        /// <summary>One or more arguments are not valid</summary>
        E_INVALIDARG = 0x80070057,


        /*
         * WMI Non-error constants
         */

        /// <summary>The operation was successful.</summary>
        WBEM_S_NO_ERROR = 0x0,

        /// <summary>No more objects are available, the number of objects returned is less than the number requested, or this is the end of an enumeration. This value is also returned when this method is called with a value of 0 for the uCount parameter.</summary>
        WBEM_S_FALSE = 0x1,

        /// <summary>An attempt was made to create an object or class that already exists.</summary>
        WBEM_S_ALREADY_EXISTS = 0x40001,

        /// <summary>An overridden property was deleted. This value is returned to signal that the original non-overridden value has been restored as a result of the deletion.</summary>
        WBEM_S_RESET_TO_DEFAULT = 0x40002,

        /// <summary>The items (objects, classes, and so on) that are being compared are not identical.</summary>
        WBEM_S_DIFFERENT = 0x40003,

        /// <summary>A call timed out. This is not an error condition. Therefore, some results may have also been returned.</summary>
        WBEM_S_TIMEDOUT = 0x40004,

        /// <summary>No more data is available from the enumeration, and the user must terminate the enumeration.</summary>
        WBEM_S_NO_MORE_DATA = 0x40005,

        /// <summary>The operation was intentionally or unintentionally canceled.</summary>
        WBEM_S_OPERATION_CANCELLED = 0x40006,

        /// <summary>A request is still in progress, and the results are not yet available.</summary>
        WBEM_S_PENDING = 0x40007,

        /// <summary>More than one copy of the same object was detected in the result set of an enumeration.</summary>
        WBEM_S_DUPLICATE_OBJECTS = 0x40008,

        /// <summary>The user was denied access to some but not all resources.</summary>
        WBEM_S_ACCESS_DENIED = 0x40009,

        /// <summary>The user did not receive all of the objects requested due to inaccessible resources (other than security violations).</summary>
        WBEM_S_PARTIAL_RESULTS = 0x40010,

        /// <summary>The provider is capable of limited service.</summary>
        WBEM_S_LIMITED_SERVICE = 0x43001,

        /// <summary>Reserved for future use.</summary>
        WBEM_S_INDIRECTLY_UPDATED = 0x43002,

        /*
         * WMI Error constants
         */
        
        /// <summary>Call failed.</summary>
        WBEM_E_FAILED = 0x80041001,

        /// <summary>Object cannot be found.</summary>
        WBEM_E_NOT_FOUND = 0x80041002,

        /// <summary>Current user does not have permission to perform the action.</summary>
        WBEM_E_ACCESS_DENIED = 0x80041003,

        /// <summary>Provider has failed at some time other than during initialization.</summary>
        WBEM_E_PROVIDER_FAILURE = 0x80041004,

        /// <summary>Type mismatch occurred.</summary>
        WBEM_E_TYPE_MISMATCH = 0x80041005,

        /// <summary>Not enough memory for the operation.</summary>
        WBEM_E_OUT_OF_MEMORY = 0x80041006,

        /// <summary>The IWbemContext object is not valid.</summary>
        WBEM_E_INVALID_CONTEXT = 0x80041007,

        /// <summary>One of the parameters to the call is not correct.</summary>
        WBEM_E_INVALID_PARAMETER = 0x80041008,

        /// <summary>Resource, typically a remote server, is not currently available.</summary>
        WBEM_E_NOT_AVAILABLE = 0x80041009,

        /// <summary>Internal, critical, and unexpected error occurred. Report the error to Microsoft Technical Support.</summary>
        WBEM_E_CRITICAL_ERROR = 0x8004100A,

        /// <summary>One or more network packets were corrupted during a remote session.</summary>
        WBEM_E_INVALID_STREAM = 0x8004100B,

        /// <summary>Feature or operation is not supported.</summary>
        WBEM_E_NOT_SUPPORTED = 0x8004100C,

        /// <summary>Parent class specified is not valid.</summary>
        WBEM_E_INVALID_SUPERCLASS = 0x8004100D,

        /// <summary>Namespace specified cannot be found.</summary>
        WBEM_E_INVALID_NAMESPACE = 0x8004100E,

        /// <summary>Specified instance is not valid.</summary>
        WBEM_E_INVALID_OBJECT = 0x8004100F,

        /// <summary>Specified class is not valid.</summary>
        WBEM_E_INVALID_CLASS = 0x80041010,

        /// <summary>Provider referenced in the schema does not have a corresponding registration.</summary>
        WBEM_E_PROVIDER_NOT_FOUND = 0x80041011,

        /// <summary>
        /// Provider referenced in the schema has an incorrect or incomplete registration.
        /// This error may be caused by many conditions, including the following:
        /// A missing #pragma namespace command in the Managed Object Format (MOF) file used to register the provider. The provider may be registered in the wrong WMI namespace.
        /// Failure to retrieve the COM registration.
        /// Hosting model is not valid. For more information, see Provider Hosting and Security.
        /// An class specified in the registration is not valid.
        /// Failure to create an instance of or inherit from the __Win32Provider class to create the provider registration in the MOF file.
        /// </summary>
        WBEM_E_INVALID_PROVIDER_REGISTRATION = 2147749906,

        /// <summary>
        /// COM cannot locate a provider referenced in the schema.
        /// This error may be caused by many conditions, including the following:
        /// Provider is using a WMI DLL that does not match the .lib file used when the provider was built.
        /// Provider's DLL, or any of the DLLs on which it depends, is corrupt.
        /// Provider failed to export DllRegisterServer.
        /// In-process provider was not registered using the regsvr32 command.
        /// Out-of-process provider was not registered using the /regserver switch. For example, myprog.exe /regserver.
        /// </summary>
        WBEM_E_PROVIDER_LOAD_FAILURE = 0x80041013,


        /// <summary>Component, such as a provider, failed to initialize for internal reasons.</summary>
        WBEM_E_INITIALIZATION_FAILURE = 0x80041014,

        /// <summary>Networking error that prevents normal operation has occurred.</summary>
        WBEM_E_TRANSPORT_FAILURE = 0x80041015,

        /// <summary>Requested operation is not valid. This error usually applies to invalid attempts to delete classes or properties.</summary>
        WBEM_E_INVALID_OPERATION = 0x80041016,

        /// <summary>Query was not syntactically valid.</summary>
        WBEM_E_INVALID_QUERY = 0x80041017,

        /// <summary>Requested query language is not supported.</summary>
        WBEM_E_INVALID_QUERY_TYPE = 0x80041018,

        /// <summary>In a put operation, the wbemChangeFlagCreateOnly flag was specified, but the instance already exists.</summary>
        WBEM_E_ALREADY_EXISTS = 0x80041019,

        /// <summary>Not possible to perform the add operation on this qualifier because the owning object does not permit overrides.</summary>
        WBEM_E_OVERRIDE_NOT_ALLOWED = 0x8004101A,

        /// <summary>User attempted to delete a qualifier that was not owned. The qualifier was inherited from a parent class.</summary>
        WBEM_E_PROPAGATED_QUALIFIER = 0x8004101B,

        /// <summary>User attempted to delete a property that was not owned. The property was inherited from a parent class.</summary>
        WBEM_E_PROPAGATED_PROPERTY = 0x8004101C,

        /// <summary>Client made an unexpected and illegal sequence of calls, such as calling EndEnumeration before calling BeginEnumeration.</summary>
        WBEM_E_UNEXPECTED = 0x8004101D,

        /// <summary>User requested an illegal operation, such as spawning a class from an instance.</summary>
        WBEM_E_ILLEGAL_OPERATION = 0x8004101E,

        /// <summary>Illegal attempt to specify a key qualifier on a property that cannot be a key. The keys are specified in the class definition for an object and cannot be altered on a per-instance basis.</summary>
        WBEM_E_CANNOT_BE_KEY = 0x8004101F,

        /// <summary>Current object is not a valid class definition. Either it is incomplete or it has not been registered with WMI using SWbemObject.Put_.</summary>
        WBEM_E_INCOMPLETE_CLASS = 0x80041020,

        /// <summary>Query is syntactically not valid.</summary>
        WBEM_E_INVALID_SYNTAX = 0x80041021,

        /// <summary>Reserved for future use.</summary>
        WBEM_E_NONDECORATED_OBJECT = 0x80041022,

        /// <summary>An attempt was made to modify a read-only property.</summary>
        WBEM_E_READ_ONLY = 0x80041023,

        /// <summary>Provider cannot perform the requested operation. This can include a query that is too complex, retrieving an instance, creating or updating a class, deleting a class, or enumerating a class.</summary>
        WBEM_E_PROVIDER_NOT_CAPABLE = 0x80041024,

        /// <summary>Attempt was made to make a change that invalidates a subclass.</summary>
        WBEM_E_CLASS_HAS_CHILDREN = 0x80041025,

        /// <summary>Attempt was made to delete or modify a class that has instances.</summary>
        WBEM_E_CLASS_HAS_INSTANCES = 0x80041026,

        /// <summary>Reserved for future use.</summary>
        WBEM_E_QUERY_NOT_IMPLEMENTED = 0x80041027,

        /// <summary>Value of Nothing/NULL was specified for a property that must have a value, such as one that is marked by a Key, Indexed, or Not_Null qualifier.</summary>
        WBEM_E_ILLEGAL_NULL = 0x80041028,

        /// <summary>Variant value for a qualifier was provided that is not a legal qualifier type.</summary>
        WBEM_E_INVALID_QUALIFIER_TYPE = 0x80041029,

        /// <summary>CIM type specified for a property is not valid.</summary>
        WBEM_E_INVALID_PROPERTY_TYPE = 0x8004102A,

        /// <summary>Request was made with an out-of-range value or it is incompatible with the type.</summary>
        WBEM_E_VALUE_OUT_OF_RANGE = 0x8004102B,

        /// <summary>Illegal attempt was made to make a class singleton, such as when the class is derived from a non-singleton class.</summary>
        WBEM_E_CANNOT_BE_SINGLETON = 0x8004102C,

        /// <summary>CIM type specified is not valid.</summary>
        WBEM_E_INVALID_CIM_TYPE = 0x8004102D,

        /// <summary>Requested method is not available.</summary>
        WBEM_E_INVALID_METHOD = 0x8004102E,

        /// <summary>Parameters provided for the method are not valid.</summary>
        WBEM_E_INVALID_METHOD_PARAMETERS = 0x8004102F,

        /// <summary>There was an attempt to get qualifiers on a system property.</summary>
        WBEM_E_SYSTEM_PROPERTY = 0x80041030,

        /// <summary>Property type is not recognized.</summary>
        WBEM_E_INVALID_PROPERTY = 0x80041031,

        /// <summary>Asynchronous process has been canceled internally or by the user. Note that due to the timing and nature of the asynchronous operation, the operation may not have been truly canceled.</summary>
        WBEM_E_CALL_CANCELLED = 0x80041032,

        /// <summary>User has requested an operation while WMI is in the process of shutting down.</summary>
        WBEM_E_SHUTTING_DOWN = 0x80041033,

        /// <summary>Attempt was made to reuse an existing method name from a parent class and the signatures do not match.</summary>
        WBEM_E_PROPAGATED_METHOD = 0x80041034,

        /// <summary>One or more parameter values, such as a query text, is too complex or unsupported. WMI is therefore requested to retry the operation with simpler parameters.</summary>
        WBEM_E_UNSUPPORTED_PARAMETER = 0x80041035,

        /// <summary>Parameter was missing from the method call.</summary>
        WBEM_E_MISSING_PARAMETER_ID = 0x80041036,

        /// <summary>Method parameter has an ID qualifier that is not valid.</summary>
        WBEM_E_INVALID_PARAMETER_ID = 0x80041037,

        /// <summary>One or more of the method parameters have ID qualifiers that are out of sequence.</summary>
        WBEM_E_NONCONSECUTIVE_PARAMETER_IDS = 0x80041038,

        /// <summary>Return value for a method has an ID qualifier.</summary>
        WBEM_E_PARAMETER_ID_ON_RETVAL = 0x80041039,

        /// <summary>Specified object path was not valid.</summary>
        WBEM_E_INVALID_OBJECT_PATH = 0x8004103A,

        /// <summary>
        /// Disk is out of space or the 4 GB limit on WMI repository (CIM repository) size is reached.
        /// Windows XP:  Disk is out of space.
        /// </summary>
        WBEM_E_OUT_OF_DISK_SPACE = 0x8004103B,

        /// <summary>Supplied buffer was too small to hold all of the objects in the enumerator or to read a string property.</summary>
        WBEM_E_BUFFER_TOO_SMALL = 0x8004103C,

        /// <summary>Provider does not support the requested put operation.</summary>
        WBEM_E_UNSUPPORTED_PUT_EXTENSION = 0x8004103D,

        /// <summary>Object with an incorrect type or version was encountered during marshaling.</summary>
        WBEM_E_UNKNOWN_OBJECT_TYPE = 0x8004103E,

        /// <summary>Packet with an incorrect type or version was encountered during marshaling.</summary>
        WBEM_E_UNKNOWN_PACKET_TYPE = 0x8004103F,

        /// <summary>Packet has an unsupported version.</summary>
        WBEM_E_MARSHAL_VERSION_MISMATCH = 0x80041040,

        /// <summary>Packet appears to be corrupt.</summary>
        WBEM_E_MARSHAL_INVALID_SIGNATURE = 0x80041041,

        /// <summary>Attempt was made to mismatch qualifiers, such as putting [key] on an object instead of a property.</summary>
        WBEM_E_INVALID_QUALIFIER = 0x80041042,

        /// <summary>Duplicate parameter was declared in a CIM method.</summary>
        WBEM_E_INVALID_DUPLICATE_PARAMETER = 0x80041043,

        /// <summary>Reserved for future use.</summary>
        WBEM_E_TOO_MUCH_DATA = 0x80041044,

        /// <summary>Call to IWbemObjectSink::Indicate has failed. The provider can refire the event.</summary>
        WBEM_E_SERVER_TOO_BUSY = 0x80041045,

        /// <summary>Specified qualifier flavor was not valid.</summary>
        WBEM_E_INVALID_FLAVOR = 0x80041046,

        /// <summary>Attempt was made to create a reference that is circular (for example, deriving a class from itself).</summary>
        WBEM_E_CIRCULAR_REFERENCE = 0x80041047,

        /// <summary>Specified class is not supported.</summary>
        WBEM_E_UNSUPPORTED_CLASS_UPDATE = 0x80041048,

        /// <summary>Attempt was made to change a key when instances or subclasses are already using the key.</summary>
        WBEM_E_CANNOT_CHANGE_KEY_INHERITANCE = 0x80041049,

        /// <summary>An attempt was made to change an index when instances or subclasses are already using the index.</summary>
        WBEM_E_CANNOT_CHANGE_INDEX_INHERITANCE = 0x80041050,

        /// <summary>Attempt was made to create more properties than the current version of the class supports.</summary>
        WBEM_E_TOO_MANY_PROPERTIES = 0x80041051,

        /// <summary>Property was redefined with a conflicting type in a derived class.</summary>
        WBEM_E_UPDATE_TYPE_MISMATCH = 0x80041052,

        /// <summary>Attempt was made in a derived class to override a qualifier that cannot be overridden.</summary>
        WBEM_E_UPDATE_OVERRIDE_NOT_ALLOWED = 0x80041053,

        /// <summary>Method was re-declared with a conflicting signature in a derived class.</summary>
        WBEM_E_UPDATE_PROPAGATED_METHOD = 0x80041054,

        /// <summary>Attempt was made to execute a method not marked with [implemented] in any relevant class.</summary>
        WBEM_E_METHOD_NOT_IMPLEMENTED = 0x80041055,

        // /// <summary>Attempt was made to execute a method marked with [disabled].</summary>
        // WBEM_E_METHOD_DISABLED = ???

        /// <summary>Refresher is busy with another operation.</summary>
        WBEM_E_REFRESHER_BUSY = 0x80041057,

        /// <summary>Filtering query is syntactically not valid.</summary>
        WBEM_E_UNPARSABLE_QUERY = 0x80041058,

        /// <summary>The FROM clause of a filtering query references a class that is not an event class (not derived from __Event).</summary>
        WBEM_E_NOT_EVENT_CLASS = 0x80041059,

        /// <summary>A GROUP BY clause was used without the corresponding GROUP WITHIN clause.</summary>
        WBEM_E_MISSING_GROUP_WITHIN = 0x8004105A,

        /// <summary>A GROUP BY clause was used. Aggregation on all properties is not supported.</summary>
        WBEM_E_MISSING_AGGREGATION_LIST = 0x8004105B,

        /// <summary>Dot notation was used on a property that is not an embedded object.</summary>
        WBEM_E_PROPERTY_NOT_AN_OBJECT = 0x8004105C,

        /// <summary>A GROUP BY clause references a property that is an embedded object without using dot notation.</summary>
        WBEM_E_AGGREGATING_BY_OBJECT = 0x8004105D,

        /// <summary>Event provider registration query (__EventProviderRegistration) did not specify the classes for which events were provided.</summary>
        WBEM_E_UNINTERPRETABLE_PROVIDER_QUERY = 0x8004105F,

        /// <summary>Request was made to back up or restore the repository while it was in use by WinMgmt.exe, or by the SVCHOST process that contains the WMI service.</summary>
        WBEM_E_BACKUP_RESTORE_WINMGMT_RUNNING = 0x80041060,

        /// <summary>Asynchronous delivery queue overflowed from the event consumer being too slow.</summary>
        WBEM_E_QUEUE_OVERFLOW = 0x80041061,

        /// <summary>Operation failed because the client did not have the necessary security privilege.</summary>
        WBEM_E_PRIVILEGE_NOT_HELD = 0x80041062,

        /// <summary>Operator is not valid for this property type.</summary>
        WBEM_E_INVALID_OPERATOR = 0x80041063,

        /// <summary>User specified a username/password/authority on a local connection. The user must use a blank username/password and rely on default security.</summary>
        WBEM_E_LOCAL_CREDENTIALS = 0x80041064,

        /// <summary>Class was made abstract when its parent class is not abstract.</summary>
        WBEM_E_CANNOT_BE_ABSTRACT = 0x80041065,

        /// <summary>Amended object was written without the WBEM_FLAG_USE_AMENDED_QUALIFIERS flag being specified.</summary>
        WBEM_E_AMENDED_OBJECT = 0x80041066,

        /// <summary>Client did not retrieve objects quickly enough from an enumeration. This constant is returned when a client creates an enumeration object, but does not retrieve objects from the enumerator in a timely fashion, causing the enumerator's object caches to back up.</summary>
        WBEM_E_CLIENT_TOO_SLOW = 0x80041067,

        /// <summary>Null security descriptor was used.</summary>
        WBEM_E_NULL_SECURITY_DESCRIPTOR = 0x80041068,

        /// <summary>Operation timed out.</summary>
        WBEM_E_TIMED_OUT = 0x80041069,

        /// <summary>Association is not valid.</summary>
        WBEM_E_INVALID_ASSOCIATION = 2147749994,

        /// <summary>Operation was ambiguous.</summary>
        WBEM_E_AMBIGUOUS_OPERATION = 0x8004106B,

        /// <summary>WMI is taking up too much memory. This can be caused by low memory availability or excessive memory consumption by WMI.</summary>
        WBEM_E_QUOTA_VIOLATION = 0x8004106C,

        /// <summary>Operation resulted in a transaction conflict.</summary>
        WBEM_E_TRANSACTION_CONFLICT = 0x8004106D,

        /// <summary>Transaction forced a rollback.</summary>
        WBEM_E_FORCED_ROLLBACK = 0x8004106E,

        /// <summary>Locale used in the call is not supported.</summary>
        WBEM_E_UNSUPPORTED_LOCALE = 0x8004106F,

        /// <summary>Object handle is out-of-date.</summary>
        WBEM_E_HANDLE_OUT_OF_DATE = 0x80041070,

        /// <summary>Connection to the SQL database failed.</summary>
        WBEM_E_CONNECTION_FAILED = 0x80041071,

        /// <summary>Handle request was not valid.</summary>
        WBEM_E_INVALID_HANDLE_REQUEST = 0x80041072,

        /// <summary>Property name contains more than 255 characters.</summary>
        WBEM_E_PROPERTY_NAME_TOO_WIDE = 0x80041073,

        /// <summary>Class name contains more than 255 characters.</summary>
        WBEM_E_CLASS_NAME_TOO_WIDE = 0x80041074,

        /// <summary>Method name contains more than 255 characters.</summary>
        WBEM_E_METHOD_NAME_TOO_WIDE = 0x80041075,

        /// <summary>Qualifier name contains more than 255 characters.</summary>
        WBEM_E_QUALIFIER_NAME_TOO_WIDE = 0x80041076,

        /// <summary>The SQL command must be rerun because there is a deadlock in SQL. This can be returned only when data is being stored in an SQL database.</summary>
        WBEM_E_RERUN_COMMAND = 0x80041077,

        /// <summary>The database version does not match the version that the repository driver processes.</summary>
        WBEM_E_DATABASE_VER_MISMATCH = 0x80041078,

        /// <summary>WMI cannot execute the delete operation because the provider does not allow it.</summary>
        WBEM_E_VETO_DELETE = 0x80041079,

        /// <summary>WMI cannot execute the put operation because the provider does not allow it.</summary>
        WBEM_E_VETO_PUT = 0x8004107A,

        /// <summary>Specified locale identifier was not valid for the operation.</summary>
        WBEM_E_INVALID_LOCALE = 0x80041080,

        /// <summary>Provider is suspended.</summary>
        WBEM_E_PROVIDER_SUSPENDED = 0x80041081,

        /// <summary>Object must be written to the WMI repository and retrieved again before the requested operation can succeed. This constant is returned when an object must be committed and retrieved to see the property value.</summary>
        WBEM_E_SYNCHRONIZATION_REQUIRED = 0x80041082,

        /// <summary>Operation cannot be completed; no schema is available.</summary>
        WBEM_E_NO_SCHEMA = 0x80041083,

        /// <summary>Provider cannot be registered because it is already registered.</summary>
        WBEM_E_PROVIDER_ALREADY_REGISTERED = 0x119FD010,

        /// <summary>Provider was not registered.</summary>
        WBEM_E_PROVIDER_NOT_REGISTERED = 0x80041085,

        /// <summary>A fatal transport error occurred.</summary>
        WBEM_E_FATAL_TRANSPORT_ERROR = 0x80041086,

        /// <summary>User attempted to set a computer name or domain without an encrypted connection.</summary>
        WBEM_E_ENCRYPTED_CONNECTION_REQUIRED = 0x80041087,

        /// <summary>A provider failed to report results within the specified timeout.</summary>
        WBEM_E_PROVIDER_TIMED_OUT = 0x80041088,

        /// <summary>User attempted to put an instance with no defined key.</summary>
        WBEM_E_NO_KEY = 0x80041089,

        /// <summary>User attempted to register a provider instance but the COM server for the provider instance was unloaded.</summary>
        WBEM_E_PROVIDER_DISABLED = 0x8004108A,

        /// <summary>Provider registration overlaps with the system event domain.</summary>
        WBEMESS_E_REGISTRATION_TOO_BROAD = 0x80042001,

        /// <summary>A WITHIN clause was not used in this query.</summary>
        WBEMESS_E_REGISTRATION_TOO_PRECISE = 0x80042002,

        /// <summary>This computer does not have the necessary domain permissions to support the security functions that relate to the created subscription instance. Contact the Domain Administrator to get this computer added to the Windows Authorization Access Group.</summary>
        WBEMESS_E_AUTHZ_NOT_PRIVILEGED = 0x80042003,

        /// <summary>Reserved for future use.</summary>
        WBEM_E_RETRY_LATER = 0x80043001,

        /// <summary>Reserved for future use.</summary>
        WBEM_E_RESOURCE_CONTENTION = 0x80043002,

        /// <summary>Expected a qualifier name.</summary>
        WBEMMOF_E_EXPECTED_QUALIFIER_NAME = 0x80044001,

        /// <summary>Expected semicolon or '='.</summary>
        WBEMMOF_E_EXPECTED_SEMI = 0x80044002,

        /// <summary>Expected an opening brace.</summary>
        WBEMMOF_E_EXPECTED_OPEN_BRACE = 0x80044003,

        /// <summary>Missing closing brace or an illegal array element.</summary>
        WBEMMOF_E_EXPECTED_CLOSE_BRACE = 0x80044004,

        /// <summary>Expected a closing bracket.</summary>
        WBEMMOF_E_EXPECTED_CLOSE_BRACKET = 0x80044005,

        /// <summary>Expected closing parenthesis.</summary>
        WBEMMOF_E_EXPECTED_CLOSE_PAREN = 0x80044006,

        /// <summary>Numeric value out of range or strings without quotes.</summary>
        WBEMMOF_E_ILLEGAL_CONSTANT_VALUE = 0x80044007,

        /// <summary>Expected a type identifier.</summary>
        WBEMMOF_E_EXPECTED_TYPE_IDENTIFIER = 0x80044008,

        /// <summary>Expected an open parenthesis.</summary>
        WBEMMOF_E_EXPECTED_OPEN_PAREN = 0x80044009,

        /// <summary>Unexpected token in the file.</summary>
        WBEMMOF_E_UNRECOGNIZED_TOKEN = 0x8004400A,

        /// <summary>Unrecognized or unsupported type identifier.</summary>
        WBEMMOF_E_UNRECOGNIZED_TYPE = 0x8004400B,

        /// <summary>Expected property or method name.</summary>
        WBEMMOF_E_EXPECTED_PROPERTY_NAME = 0x8004400B,

        /// <summary>Typedefs and enumerated types are not supported.</summary>
        WBEMMOF_E_TYPEDEF_NOT_SUPPORTED = 0x8004400D,

        /// <summary>Only a reference to a class object can have an alias value.</summary>
        WBEMMOF_E_UNEXPECTED_ALIAS = 0x8004400E,

        /// <summary>Unexpected array initialization. Arrays must be declared with [].</summary>
        WBEMMOF_E_UNEXPECTED_ARRAY_INIT = 0x8004400F,

        /// <summary>Namespace path syntax is not valid.</summary>
        WBEMMOF_E_INVALID_AMENDMENT_SYNTAX = 0x80044010,

        /// <summary>Duplicate amendment specifiers.</summary>
        WBEMMOF_E_INVALID_DUPLICATE_AMENDMENT = 0x80044011,

        /// <summary>#pragma must be followed by a valid keyword.</summary>
        WBEMMOF_E_INVALID_PRAGMA = 0x80044012,

        /// <summary>Namespace path syntax is not valid.</summary>
        WBEMMOF_E_INVALID_NAMESPACE_SYNTAX = 0x80044013,

        /// <summary>Unexpected character in class name must be an identifier.</summary>
        WBEMMOF_E_EXPECTED_CLASS_NAME = 0x80044014,

        /// <summary>The value specified cannot be made into the appropriate type.</summary>
        WBEMMOF_E_TYPE_MISMATCH = 0x80044015,

        /// <summary>Dollar sign must be followed by an alias name as an identifier.</summary>
        WBEMMOF_E_EXPECTED_ALIAS_NAME = 0x80044016,

        /// <summary>Class declaration is not valid.</summary>
        WBEMMOF_E_INVALID_CLASS_DECLARATION = 0x80044017,

        /// <summary>The instance declaration is not valid. It must start with "instance of"</summary>
        WBEMMOF_E_INVALID_INSTANCE_DECLARATION = 0x80044018,

        /// <summary>Expected dollar sign. An alias in the form "$name" must follow the "as" keyword.</summary>
        WBEMMOF_E_EXPECTED_DOLLAR = 0x80044019,

        /// <summary>"CIMTYPE" qualifier cannot be specified directly in a MOF file. Use standard type notation.</summary>
        WBEMMOF_E_CIMTYPE_QUALIFIER = 0x8004401A,

        /// <summary>Duplicate property name was found in the MOF.</summary>
        WBEMMOF_E_DUPLICATE_PROPERTY = 0x8004401B,

        /// <summary>Namespace syntax is not valid. References to other servers are not allowed.</summary>
        WBEMMOF_E_INVALID_NAMESPACE_SPECIFICATION = 0x8004401C,

        /// <summary>Value out of range.</summary>
        WBEMMOF_E_OUT_OF_RANGE = 0x8004401D,

        /// <summary>The file is not a valid text MOF file or binary MOF file.</summary>
        WBEMMOF_E_INVALID_FILE = 0x8004401E,

        /// <summary>Embedded objects cannot be aliases.</summary>
        WBEMMOF_E_ALIASES_IN_EMBEDDED = 0x8004401F,

        /// <summary>NULL elements in an array are not supported.</summary>
        WBEMMOF_E_NULL_ARRAY_ELEM = 0x80044020,

        /// <summary>Qualifier was used more than once on the object.</summary>
        WBEMMOF_E_DUPLICATE_QUALIFIER = 0x80044021,

        /// <summary>Expected a flavor type such as ToInstance, ToSubClass, EnableOverride, or DisableOverride.</summary>
        WBEMMOF_E_EXPECTED_FLAVOR_TYPE = 0x80044022,

        /// <summary>Combining EnableOverride and DisableOverride on same qualifier is not legal.</summary>
        WBEMMOF_E_INCOMPATIBLE_FLAVOR_TYPES = 0x80044023,

        /// <summary>An alias cannot be used twice.</summary>
        WBEMMOF_E_MULTIPLE_ALIASES = 0x80044024,

        /// <summary>Combining Restricted, and ToInstance or ToSubClass is not legal.</summary>
        WBEMMOF_E_INCOMPATIBLE_FLAVOR_TYPES2 = 0x80044025,

        /// <summary>Methods cannot return array values.</summary>
        WBEMMOF_E_NO_ARRAYS_RETURNED = 0x80044026,

        /// <summary>Arguments must have an In or Out qualifier.</summary>
        WBEMMOF_E_MUST_BE_IN_OR_OUT = 0x80044027,

        /// <summary>Flags syntax is not valid.</summary>
        WBEMMOF_E_INVALID_FLAGS_SYNTAX = 0x80044028,

        /// <summary>The final brace and semi-colon for a class are missing.</summary>
        WBEMMOF_E_EXPECTED_BRACE_OR_BAD_TYPE = 0x80044029,

        /// <summary>A CIM version 2.2 feature is not supported for a qualifier value.</summary>
        WBEMMOF_E_UNSUPPORTED_CIMV22_QUAL_VALUE = 0x8004402A,

        /// <summary>The CIM version 2.2 data type is not supported.</summary>
        WBEMMOF_E_UNSUPPORTED_CIMV22_DATA_TYPE = 0x8004402B,

        /// <summary>The delete instance syntax is not valid. It should be #pragma DeleteInstance("instancepath", FAIL|NOFAIL)</summary>
        WBEMMOF_E_INVALID_DELETEINSTANCE_SYNTAX = 0x8004402C,

        /// <summary>The qualifier syntax is not valid. It should be qualifiername:type=value,scope(class|instance), flavorname.</summary>
        WBEMMOF_E_INVALID_QUALIFIER_SYNTAX = 0x8004402D,

        /// <summary>The qualifier is used outside of its scope.</summary>
        WBEMMOF_E_QUALIFIER_USED_OUTSIDE_SCOPE = 0x8004402E,

        /// <summary>Error creating temporary file. The temporary file is an intermediate stage in the MOF compilation.</summary>
        WBEMMOF_E_ERROR_CREATING_TEMP_FILE = 0x8004402F,

        /// <summary>A file included in the MOF by the preprocessor command #include is not valid.</summary>
        WBEMMOF_E_ERROR_INVALID_INCLUDE_FILE = 0x80044030,

        /// <summary>The syntax for the preprocessor commands #pragma deleteinstance or #pragma deleteclass is not valid.	</summary>
        WBEMMOF_E_INVALID_DELETECLASS_SYNTAX = 0x80044031,

    }
}
