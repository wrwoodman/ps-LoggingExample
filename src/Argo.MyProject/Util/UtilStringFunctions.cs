using PostSharp.Patterns.Diagnostics;

#pragma warning disable 1591  // Disable XML comment warning
namespace Argo.MyProject.Util
{
    /// <summary>
    /// Generally useful string functions.  Remove if not needed.
    /// </summary>
    [Log(AttributeExclude = true)]
    public static class UtilStringFunctions
    {
        public static string PadRight(string data, int length, char character = ' ')
        {
            data ??= string.Empty;
            return (data.Length <= length)   // Less than or equal to
                ? data.PadRight(length, character)
                : data.Substring(0, length);
        }

        public static string PadLeft(string data, int length, char character = ' ')
        {
            data ??= string.Empty;
            return (data.Length <= length)   // Less than or equal to
                ? data.PadLeft(length, character)
                : data.Substring(0, length);
        }

        public static string TakeRightmostCharacters(string data, int maxLength)
        {
            if (string.IsNullOrEmpty(data) || data.Length <= maxLength)
                return data;

            int startIndex = data.Length - maxLength;
            return data.Substring(startIndex);
        }

        public static string FormatUniqueId(string uniqueId)
        {
            var uniqueIdRawNoDashes = uniqueId.Replace("-", "");
            uniqueIdRawNoDashes = PadRight(uniqueIdRawNoDashes, 32);
            return uniqueIdRawNoDashes;
        }
    }
}
