namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public sealed record RenteVastePeriode(Percentage Rente, int Looptijd)
{
    public static RenteVastePeriode Create(Percentage rente, int looprijd)
        => new(rente, looprijd);

    public Percentage MaandRente => Rente / 12m;

}
