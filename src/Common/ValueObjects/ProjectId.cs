using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace FinSecure.Platform.Common.ValueObjects;
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public partial record struct ProjectId : IValueObject<ProjectId>
{
    public const string _unknownValue = "?";
    private readonly string _value;
    private ProjectId(string value)
    {
        _value = value;
    }

    public static ProjectId Next()
        => Create(Guid.NewGuid());

    public static ProjectId Create(Guid value)
        => new(value.ToString());

    public override readonly string ToString()
        => ProjectIdFormatter.ToString(_value);

    public static ProjectId Unknown => new(_unknownValue);

    public static ProjectId Empty => new(string.Empty);

    public static ProjectId Parse(string s)
        => Parse(s, null);

    public static ProjectId Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : Unknown;

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out ProjectId result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ProjectId result)
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

        return ProjectIdFormatter.TryParse(s, out result);
    }

    public readonly bool IsEmpty()
        => this == Empty;
}


internal static class ProjectIdFormatter
{
    public static string ToString(string value)
    {
        return value;
    }

    public static bool TryParse(string s, out ProjectId result)
    {
        if (Guid.TryParse(s, out var id))
        {
            result = ProjectId.Create(id);
            return true;
        }

        result = ProjectId.Unknown;
        return false;
    }
}
