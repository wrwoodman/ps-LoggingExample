using System;
using System.Diagnostics;
using Argo.MyProject.Util;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Patterns.Formatters;
#pragma warning disable 1591  // Disable XML comment warning

namespace Argo.MyProject.Logging
{
    public class CustomNLogRecordBuilder : NLogLogRecordBuilder
    {
        public CustomNLogRecordBuilder(NLogLoggingBackend backend) : base(backend)
        {
        }

        protected override void Write(UnsafeString message)
        {
            try
            {
                string uniqueId = string.Empty;
                var log = ((NLogLoggingTypeSource)TypeSource).Logger;

                Context.VisitProperties((string name, object value) =>
                {
                    // UniqueId is the name of a property of the LoggingPropertyData class
                    if (name == "UniqueId")
                    {
                        if (value != null && !(value is string s && string.IsNullOrEmpty(s)))
                        {
                            uniqueId = value.ToString();
                        }
                    }
                });
                log.Properties[Constants.UniqueIdKey] = uniqueId;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            base.Write(message);
        }
    }
}