
namespace WebApp.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string title)
        {
            return char.ToUpper(title[0]) + title.Substring(1).ToLower();
        }
    }
}
