using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AL.LeagueRoster.Common;

public static class MiscExtenders
{
    private static readonly Regex isEmailAddress = new(
        @"(?!.*\.\.)(^[^\.][^@\s]+@[^@\s]+\.[^@\s\.]+$)",
        RegexOptions.Compiled);

    private static readonly Regex isBritishPostCode = new(
        "^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$");

    private static readonly Regex isZipCode = new(
        @"^\d{5}(?:-\d{4})?$", RegexOptions.Compiled);

    private static readonly Regex isMexicanPostalCode = new(
        @"^\d{5}(?:-\d{4})?$", RegexOptions.Compiled);

    private static readonly Regex isCanadianPostalCode = new(
        "^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$");

    private static readonly HashSet<string> countryCodes =
        new() { "US", "CA", "MX", "GB" };

    public static R Funcify<T, R>(
        this T value, Func<T, R> func) => func(value);

    public static bool IsTrimmed(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value)
            && !char.IsWhiteSpace(value[0])
            && !char.IsWhiteSpace(value[^1]);
    }

    public static bool IsEmailAddress(this string? value) =>
        value != null && isEmailAddress.IsMatch(value);

    public static bool IsPostalCode(this string? value, CountryCode? countryCode)
    {
        if (value == null || !countryCode.HasValue)
            return false;

        return countryCode switch
        {
            CountryCode.US => isZipCode.IsMatch(value),
            CountryCode.MX => isMexicanPostalCode.IsMatch(value),
            CountryCode.CA => isCanadianPostalCode.IsMatch(value),
            CountryCode.GB => isBritishPostCode.IsMatch(value),
            _ => false
        };
    }
}
