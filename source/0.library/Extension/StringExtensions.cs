using System.Globalization;

namespace Library.Extension
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }

        public static string AddPrefix(this string s, string prefix, string separator = "")
        {
            return $"{(!string.IsNullOrWhiteSpace(prefix) ? $"{prefix}{separator}" : string.Empty)}{s}";
        }
        public static string AddSufix(this string s, string sufix, string separator = "")
        {
            return $"{s}{(!string.IsNullOrWhiteSpace(sufix) ? $"{separator}{sufix}" : string.Empty)}";
        }
    }
}
