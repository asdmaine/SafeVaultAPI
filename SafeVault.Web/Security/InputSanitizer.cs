using System.Text.RegularExpressions;

namespace SafeVault.Web.Security
{
    public static class InputSanitizer
    {
        // Create patterns to remove script tags and html tags from any inputs to prevent injection
        private static readonly Regex ScriptTagPattern = new(@"<script.*?>.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex HtmlTagPattern = new(@"<.*?>", RegexOptions.Singleline | RegexOptions.Compiled);
        
        public static string Sanitize(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            // Remove script tags from the input, if there are any. Then remove html tags, if there are any.
            var cleaned = ScriptTagPattern.Replace(input, string.Empty);
            cleaned = HtmlTagPattern.Replace(cleaned, string.Empty);
            return cleaned.Trim();
        }
    }
}