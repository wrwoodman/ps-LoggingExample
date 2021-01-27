using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;

// Add other directives as needed.

[assembly: Log("default", AttributePriority = 1, AttributeTargetMemberAttributes = MulticastAttributes.Protected | MulticastAttributes.Public)] // Includes all public and protected members
[assembly: Log(AttributePriority = 2, AttributeExclude = true, AttributeTargetMembers = "get_*")]  // Exclude all Getter properties
[assembly: Log(AttributePriority = 3, AttributeExclude = true, AttributeTargetMembers = "set_*")]  // Exclude all Setter properties
[assembly: Log(AttributePriority = 4, AttributeExclude = true, AttributeTargetMembers = "*ctor*")] // Exclude all constructors
// If you wanted to exclude an entire namespace, do it like this.
[assembly: Log(AttributePriority = 5, AttributeExclude = true, AttributeTargetTypes = "Argo.MyProject.Logging.*")]
