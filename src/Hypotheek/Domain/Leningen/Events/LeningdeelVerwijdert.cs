namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Events;

public record LeningdeelVerwijdert(LeningId LeningId, LeningdeelId LeningdeelId) : EventRecord;
