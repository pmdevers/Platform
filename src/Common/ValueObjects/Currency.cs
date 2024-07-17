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
public record Currency(string Symbol, string Code, string Unit) : IValueObject<Currency>
{
    public static Currency Default { get; set; } = Euro;
    public static Currency Euro => new("€", "EUR", "Euro");
    public static Currency Dollar => new("$", "USD", "United States Dollar");

    public static Currency Unknown => new("?", "Unknown", "Unknown Currency");

    public static Currency Empty => new(string.Empty, string.Empty, string.Empty);

    public static Currency Parse(string s)
        => Parse(s, null);


    public static Currency Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result :
        throw new FormatException($"Unknown currency '{s}'.");

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Currency result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Currency result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        result = s switch
        {
            "$" => Dollar,
            "€" => Euro,
            "EUR" => Euro,
            "USD" => Dollar,
            _ => Unknown
        };

        return result != Unknown;
    }

    public bool IsEmpty()
        => this == Empty;
}