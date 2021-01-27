using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace Argo.NLogExtensions
{
    [LayoutRenderer("UniqueId")]
    public class NlogUniqueIdRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var uniqueId = string.Empty;
            if (logEvent.Properties.ContainsKey(Constants.UniqueIdKey))
            {
                var uniqueIdRaw = logEvent.Properties[Constants.UniqueIdKey].ToString();
                // A GUID typically has 36 characters.
                // If we take out the dashes, we'll have the 32 characters available for the log.
                var uniqueIdRawNoDashes = uniqueIdRaw.Replace("-", "");
                uniqueId = UtilStringFunctions.PadRight(uniqueIdRawNoDashes, 32);
            }
            else
            {
                uniqueId = UtilStringFunctions.PadRight(uniqueId, 32);
            }

            builder.Append(uniqueId);
        }
    }
}
