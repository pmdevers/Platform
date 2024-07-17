using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace FinSecure.Platform.Common.ValueObjects;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", 
    Justification = "BSN is a known abbreviation.")]
public readonly record struct BSN : IValueObject<BSN>
{
    private readonly string _value;

    private BSN(string value)
    {
        _value = value;
    }

    public static BSN Create(long value)
        => Parse(value.ToString());

    public static BSN Unknown => new("?");

    public static BSN Empty => new("");

    public override string ToString()
    {
        if (IsEmpty() || this == Unknown)
            return _value;

        return $"{_value[..3]}.{_value.Substring(3, 3)}.{_value.Substring(6, 3)}";
    }

    public static BSN Parse(string s)
        => Parse(s, null);

    public static BSN Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : Unknown;

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out BSN result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out BSN result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        s = Normalize(s);

        if (s.Length != 9 || !long.TryParse(s, out _))
        {
            result = Unknown;
            return false;
        }

        if (ElfProef(s))
        {
            result = new BSN(s);
            return true;
        }

        return false;
    }

    private static string Normalize(string s) =>
        s.Replace(".", string.Empty)
         .Replace(",", string.Empty);

    private static bool ElfProef(string s)
    {
        int[] weging = [9, 8, 7, 6, 5, 4, 3, 2, -1];
        int totaal = 0;

        for (int i = 0; i < s.Length; i++)
        {
            totaal += (s[i] - '0') * weging[i];
        }

        return totaal % 11 == 0;
    }

    public bool IsEmpty() => this == Empty;
}