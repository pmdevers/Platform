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
public record struct Money(Currency Currency, Amount Amount) : IValueObject<Money>
{
    public static Money Unknown => new(Currency.Unknown, Amount.Unknown);

    public static Money Empty => new(Currency.Empty, Amount.Empty);

    public override readonly string ToString()
        => CurrencyFormatter.FormatCurrency(Currency, Amount, 2);

    public static Money Parse(string s)
        => Parse(s, null);

    public static Money Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result :
        throw new FormatException($"Unknown money: '{s}'");

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Money result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Money result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length > 1)
        {
            var currency = Currency.Parse(parts[0]);
            var amount = Amount.Parse(parts[1]);
            result = new(currency, amount);
            return true;
        }

        result = Unknown;
        return false;
    }

    public readonly bool IsEmpty()
        => this == Empty;
}
