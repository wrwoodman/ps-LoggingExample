
namespace Argo.NLogExtensions
{
    internal static class UtilStringFunctions
    {
        public static string PadRight(string data, int length, char character = ' ')
        {
            data = data ?? string.Empty;
            return (data.Length <= length)   // Less than or equal to
                ? data.PadRight(length, character)
                : data.Substring(0, length);
        }
    }
}
