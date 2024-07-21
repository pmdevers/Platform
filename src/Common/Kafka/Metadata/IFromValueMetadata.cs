﻿namespace FinSecure.Platform.Common.Kafka.Metadata;

[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
public sealed class FromValueAttribute : Attribute, IFromValueMetadata
{
}

public interface IFromValueMetadata
{
}


