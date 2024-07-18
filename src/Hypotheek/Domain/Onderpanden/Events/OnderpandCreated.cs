namespace FinSecure.Platform.Hypotheek.Domain.Onderpanden.Events;

public record OnderpandCreated(OnderpandId OnderpandId, OnderpandType OnderpandType) : EventRecord;
