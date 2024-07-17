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
public partial record struct SubscriptionId : IValueObject<SubscriptionId>
{
    public const string HeaderName = "Subscription";

    public const string _unknownValue = "?";
    private readonly string _value;
    private SubscriptionId(string value)
    {
        _value = value;
    }

    public static SubscriptionId Next()
        => Create(Guid.NewGuid());

    public static SubscriptionId Create(Guid value)
        => new(value.ToString());

    public override readonly string ToString()
        => SubscriptionIdFormatter.ToString(_value);

    public static SubscriptionId Unknown => new(_unknownValue);

    public static SubscriptionId Empty => new(string.Empty);

    public static SubscriptionId Parse(string s)
        => Parse(s, null);

    public static SubscriptionId Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : Unknown;

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out SubscriptionId result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SubscriptionId result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return true;
        }

        if (s == _unknownValue)
        {
            result = Unknown;
            return true;
        }

        return SubscriptionIdFormatter.TryParse(s, out result);
    }

    public readonly bool IsEmpty()
        => this == Empty;
}


internal static class SubscriptionIdFormatter
{
    public static string ToString(string value)
    {
        return value;
    }

    public static bool TryParse(string s, out SubscriptionId result)
    {
        if (Guid.TryParse(s, out var id))
        {
            result = SubscriptionId.Create(id);
            return true;
        }

        result = SubscriptionId.Unknown;
        return false;
    }
}
