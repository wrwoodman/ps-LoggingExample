// TODO: If you have a large set of DTOs and they are in their own project, you will want to move this to the DTO project

using PostSharp.Patterns.Diagnostics;

namespace Argo.MyProject.Util
{
    /// <summary>
    /// Used to hide PII data.
    /// </summary>
    [Log(AttributeExclude = true)]
    public static class ScrubData
    {
        /// <summary>
        ///  Mask sensitive PII data within DTOs.
        /// </summary>
        /// <param name="data">The data to mask</param>
        /// <param name="maxLen">The maximum length of masked data</param>
        /// <param name="replaceChar">The character used as the mask.</param>
        /// <returns></returns>
        public static string Obscure(string data, int maxLen = 5, char replaceChar = '*')
        {
            int len = (string.IsNullOrEmpty(data)) ? 1 : (data.Length >= maxLen) ? maxLen : data.Length;
            return string.IsNullOrEmpty(data) ? data : new string('*', len);
        }
    }
}
