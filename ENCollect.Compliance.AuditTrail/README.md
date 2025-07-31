# Audit Trail release notes
The interface IAuditTrailManager is implemented by a simplistic InMemoryAuditTrailManager.
A console application is provided to validate working. 

### ToDo
1) A production grade audit trail manager that writes to a database table should be implemented. The table should be optimised for append. 
2) The AuditedEntityType Enum should be made exhaustive.

### Audit trail structure
The AuditTrailRecord contains the final structure. It uses AuditEventData (which is what devs need to use). AuditTrailRecord is intentionally maintained as an internal sealed class for use within the library. 
Given the old object and new object, the differences are automatically inferred and converted into a json. Only the differential json is stored as trail. 
Nested objects are intentionally not supported. Since it is an audit trail, every entity modified should be trailed separately, not as a collection. 
Only Edits and Deletes to be audit trailed (Enum AuditOperation will mandate this).

### Note
1) 2 packages are used for diff generation. Newtonsoft.Json and KellermanSoftware.CompareNetObjects
2) Note that AuditEventData is a record and a not a class. This is a C#9+ construct which suits immutable data types very well. 