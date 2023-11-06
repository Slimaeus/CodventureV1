namespace CodventureV1.Domain.Common.Extensions;

public static class StringExtensions
{
    public static string Pluralize(this string text, int number = 2)
    {
        if (number == 1)
        {
            return text;
        }
        else
        {
            // Create a dictionary of exceptions that have to be checked first
            // This is very much not an exhaustive list!
            var exceptions = new Dictionary<string, string>() {
                { "man", "men" },
                { "woman", "women" },
                { "child", "children" },
                { "tooth", "teeth" },
                { "foot", "feet" },
                { "mouse", "mice" },
                { "belief", "beliefs" } };

            if (exceptions.ContainsKey(text.ToLowerInvariant()))
            {
                return exceptions[text.ToLowerInvariant()];
            }
            else if (text.EndsWith("y", StringComparison.OrdinalIgnoreCase) &&
                !text.EndsWith("ay", StringComparison.OrdinalIgnoreCase) &&
                !text.EndsWith("ey", StringComparison.OrdinalIgnoreCase) &&
                !text.EndsWith("iy", StringComparison.OrdinalIgnoreCase) &&
                !text.EndsWith("oy", StringComparison.OrdinalIgnoreCase) &&
                !text.EndsWith("uy", StringComparison.OrdinalIgnoreCase))
            {
                return text.Substring(0, text.Length - 1) + "ies";
            }
            else if (text.EndsWith("us", StringComparison.InvariantCultureIgnoreCase))
            {
                // http://en.wikipedia.org/wiki/Plural_form_of_words_ending_in_-us
                return text + "es";
            }
            else if (text.EndsWith("ss", StringComparison.InvariantCultureIgnoreCase))
            {
                return text + "es";
            }
            else if (text.EndsWith("s", StringComparison.InvariantCultureIgnoreCase))
            {
                return text;
            }
            else if (text.EndsWith("x", StringComparison.InvariantCultureIgnoreCase) ||
                text.EndsWith("ch", StringComparison.InvariantCultureIgnoreCase) ||
                text.EndsWith("sh", StringComparison.InvariantCultureIgnoreCase))
            {
                return text + "es";
            }
            else if (text.EndsWith("f", StringComparison.InvariantCultureIgnoreCase) && text.Length > 1)
            {
                return text.Substring(0, text.Length - 1) + "ves";
            }
            else if (text.EndsWith("fe", StringComparison.InvariantCultureIgnoreCase) && text.Length > 2)
            {
                return text.Substring(0, text.Length - 2) + "ves";
            }
            else
            {
                return text + "s";
            }
        }
    }
}
