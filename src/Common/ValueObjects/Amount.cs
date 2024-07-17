using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace FinSecure.Platform.Common.ValueObjects;
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public partial record struct Amount : IValueObject<Amount>
{
    private decimal? _value = 0;

    private Amount(decimal? value)
    {
        _value = value;
    }

    public static Amount Unknown => new(null);

    public static Amount Empty => new(0);
    public static Amount Zero => new(0);

    public static Amount One => new(1);
    public static Amount Max => new(decimal.MaxValue);
    public static Amount Min => new(decimal.MinValue);

    public static Amount Create(decimal value)
        => new(value);

    public static Amount Create(int value)
        => Create((decimal)value);

    public static Amount Create(double value)
        => Create((decimal)value);


    public override readonly string ToString()
        => (_value ?? 0).ToString();

    public static Amount Parse(string s)
        => Parse(s, null);

    public static Amount Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out Amount result) ? result : Unknown;

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Amount result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Amount result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        if (decimal.TryParse(s, NumberStyles.Any, provider, out decimal value))
        {
            result = new Amount(value);
            return true;
        }

        result = Unknown;

        return false;
    }

    public readonly bool IsEmpty()
        => this == Empty;

    public static implicit operator Amount(decimal val) => Create(val);
    public static explicit operator decimal(Amount val) => val._value ?? 0;
    public static explicit operator double(Amount val) => (double)(val._value ?? 0);
}

public partial record struct Amount :
    IIncrementOperators<Amount>,
    IDecrementOperators<Amount>,
    IUnaryPlusOperators<Amount, Amount>,
    IUnaryNegationOperators<Amount, Amount>,
    IAdditionOperators<Amount, Amount, Amount>,
    IAdditionOperators<Amount, Currency, Money>,
    ISubtractionOperators<Amount, Amount, Amount>,
    IMultiplyOperators<Amount, decimal, Amount>,
    IMultiplyOperators<Amount, int, Amount>,
    IMultiplyOperators<Amount, double, Amount>,
    IMultiplyOperators<Amount, Percentage, Amount>,
    IDivisionOperators<Amount, decimal, Amount>,
    IDivisionOperators<Amount, double, Amount>,
    IDivisionOperators<Amount, int, Amount>,
    IDivisionOperators<Amount, Percentage, Amount>
{
    public static Amount operator +(Amount value)
        => new(+value._value);
    public static Amount operator -(Amount value)
       => new(-value._value);

    public static Amount operator +(Amount left, Amount right)
        => new(left._value + right._value);

    public static Amount operator -(Amount left, Amount right)
        => new(left._value - right._value);

    public static Amount operator ++(Amount value)
        => new(value._value++);

    public static Amount operator --(Amount value)
        => new(value._value--);

    public static Amount operator *(Amount left, decimal right)
        => new(left._value * right);

    public static Amount operator /(Amount left, decimal right)
        => new(left._value / right);

    public static Amount operator *(Amount left, Percentage right)
        => new(left._value * (decimal)right);

    public static bool operator >(Amount left, Amount right)
        => left._value > right._value;

    public static bool operator >=(Amount left, Amount right)
        => right._value >= left._value;

    public static bool operator <(Amount left, Amount right)
        => left._value < right._value;

    public static bool operator <=(Amount left, Amount right)
        => left._value <= right._value;

    public static Amount operator %(Amount left, decimal right)
        => new(left._value % right);

    public static Amount operator *(Amount left, double right)
        => new(left._value * (decimal)right);

    public static Amount operator /(Amount left, double right)
        => new(left._value / (decimal)right);

    public static Amount operator *(Amount left, int right)
        => new(left._value * right);

    public static Amount operator /(Amount left, int right)
        => new(left._value / right);

    public static Money operator +(Amount left, Currency right)
        => new(right, left);

    public static Amount operator /(Amount left, Percentage right)
        => new(left._value / (decimal)right);
}


public static class AmountLinqExtensions
{
    public static Amount Sum(this IEnumerable<Amount> source)
    {
        return source.Aggregate((left, right) => left + right);
    }

    public static IEnumerable<Money> Sum(this IEnumerable<Money> source)
    {
        var results = source.GroupBy(x => x.Currency)
            .Select(x => new Money(x.Key, x.Sum(y => y.Amount)));
        return results;
    }

    public static Money Sum(this IEnumerable<Money> source, Currency currency)
    {
        var results = source.Where(x => x.Currency == currency)
            .Select(x => x.Amount).Sum();
        return new(currency, results);
    }

    public static Amount Sum<T>(this IEnumerable<T> source, Func<T, Amount> selector)
        => Sum(source.Select(selector));

    public static IEnumerable<Money> Sum<T>(this IEnumerable<T> source, Func<T, Money> selector)
    {
        var results = source.Select(selector).GroupBy(x => x.Currency)
            .Select(x => new Money(x.Key, x.Sum(y => y.Amount)));
        return results;
    }

    public static Money Sum<T>(this IEnumerable<T> source, Func<T, Money> selector, Currency currency)
    {
        var results = source
            .Select(selector)
            .Where(x => x.Currency == currency)
            .Select(x => x.Amount).Sum();

        return new Money(currency, results);
    }
}
