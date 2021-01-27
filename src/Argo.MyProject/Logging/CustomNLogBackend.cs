using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
#pragma warning disable 1591  // Disable XML comment warning

namespace Argo.MyProject.Logging
{
    public class CustomNLogBackend : NLogLoggingBackend
    {
        public override LogRecordBuilder CreateRecordBuilder()
        {
            return new CustomNLogRecordBuilder(this);
        }

    }
}