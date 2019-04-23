using System.Globalization;

namespace Library.Extension
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }
    }
}
