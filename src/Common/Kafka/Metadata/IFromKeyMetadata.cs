namespace FinSecure.Platform.Common.Kafka.Metadata;
public interface IFromKeyMetadata
{
}

[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
public sealed class FromKeyAttribute : Attribute, IFromKeyMetadata
{
}




