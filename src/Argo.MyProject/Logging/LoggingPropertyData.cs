using PostSharp.Patterns.Diagnostics.Custom;

namespace Argo.MyProject.Logging
{
    internal class LoggingPropertyData
    {
        [LoggingPropertyOptions(IsInherited = true)]
        public string UniqueId { get; set; }
    }
}
