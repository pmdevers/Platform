namespace FinSecure.Platform.Hypotheek.Domain.Aanvragen;

public record Aanvrager(AanvragerType Type, AanvragerId AanvragerId);


public enum AanvragerType
{
    NatuurlijkPersoon,
    Rechtspersoon,
}