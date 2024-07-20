using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Numerics;
using System.Globalization;

namespace FinSecure.Platform.Common.ValueObjects;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public partial record struct Percentage : IValueObject<Percentage>
{
    private decimal? _value;
    private Percentage(decimal? value)
    {
        _value = value;
    }

    public static Percentage Unknown => new(null);

    public static Percentage Empty => new(0);

    public static Percentage Create(decimal value)
        => Parse(value.ToString());

    public override readonly string ToString()
    {
        if (IsEmpty())
            return string.Empty;

        if (this == Unknown)
            return "?";

        return PercentageParser.ToString(_value);
    }

    public static Percentage Parse(string s)
         => Parse(s, null);

    public static Percentage Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : Unknown;

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Percentage result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Percentage result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        if (PercentageParser.TryParse(s, out decimal r))
        {
            result = new Percentage(r);
            return true;
        }

        return false;
    }

    public readonly bool IsEmpty() => this == Empty;

    public static implicit operator Percentage(decimal val) => Create(val);
    public static explicit operator decimal(Percentage val) => val._value ?? 0;
    public static explicit operator double(Percentage val) => (double)(val._value ?? 0);
}

public partial record struct Percentage :
    IIncrementOperators<Percentage>,
    IDecrementOperators<Percentage>,
    IUnaryPlusOperators<Percentage, Percentage>,
    IUnaryNegationOperators<Percentage, Percentage>,
    IAdditionOperators<Percentage, Percentage, Percentage>,
    ISubtractionOperators<Percentage, Percentage, Percentage>,
    IMultiplyOperators<Percentage, Percentage, Percentage>,
    IDivisionOperators<Percentage, Percentage, Percentage>,
    IDivisionOperators<Percentage, decimal, Percentage>,
    IDivisionOperators<Percentage, int, Percentage>
{
    public static Percentage operator ++(Percentage value)
        => new(value._value++);

    public static Percentage operator --(Percentage value)
        => new(value._value--);

    public static Percentage operator +(Percentage value)
        => new(+value._value);

    public static Percentage operator -(Percentage value)
        => new(-value._value);

    public static Percentage operator +(Percentage left, Percentage right)
        => new(left._value + right._value);

    public static Percentage operator -(Percentage left, Percentage right)
        => new(left._value - right._value);

    public static Percentage operator *(Percentage left, Percentage right)
        => new(left._value * right._value);

    public static Percentage operator /(Percentage left, Percentage right)
        => new(left._value / right._value);

    public static Percentage operator /(Percentage left, decimal right)
        => new((decimal)left / right);

    public static Percentage operator /(Percentage left, int right)
        => new((decimal)left / right);
}

internal static class PercentageParser
{
    private const int PROCENT_FACTOR = 100;
    private const int PROMILE_FACTOR = 1000;

    public static string ToString(decimal? value)
    {
        var culture = new CultureInfo("nl-NL");
        return ((value ?? 0) * PROCENT_FACTOR).ToString(culture) + "%";
    }

    public static bool TryParse(string s, out decimal result)
    {
        result = 0;
        var value = Normalize(s);

        if (decimal.TryParse(value, out var tmp))
        {

            result = tmp / PROCENT_FACTOR;

            if (s.Contains('‰'))
            {
                result = tmp / PROMILE_FACTOR;
            }

            return true;
        }

        return false;
    }

    private static string Normalize(string s)
        => s.Replace("%", string.Empty)
            .Replace("‰", string.Empty);

}