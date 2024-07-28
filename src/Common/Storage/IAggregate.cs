﻿
namespace FinSecure.Platform.Common.Storage;

public interface IAggregate<out TState>
{
    TState State { get; }
    IEventStream Stream { get; }
    ValidationMessage[] Messages { get; }
    bool IsValid { get; }
    void Apply(Event e);
    Task LoadAsync(Guid id);
}