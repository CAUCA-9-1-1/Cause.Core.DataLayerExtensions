using System.Text.RegularExpressions;

namespace Cause.Core.DataLayerExtensions;

public static partial class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var startUnderscores = StartUnderscoresRegex().Match(input);
        return startUnderscores + LowerUpperRegex().Replace(input, "$1_$2").ToLower();
    }

    [GeneratedRegex(@"^_+")]
    private static partial Regex StartUnderscoresRegex();
    [GeneratedRegex(@"([a-z0-9])([A-Z])")]
    private static partial Regex LowerUpperRegex();
}