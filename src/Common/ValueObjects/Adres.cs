using Featurize.ValueObjects;

namespace FinSecure.Platform.Common.ValueObjects;

public record Adres(string Straat, string Huisnummer, string PostCode, string Plaats, Country Land)
{
    public static Adres Empty()
        => new(string.Empty, string.Empty, string.Empty, string.Empty, Country.Empty);
}