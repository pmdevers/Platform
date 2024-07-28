namespace FinSecure.Platform.Common.Storage;

public interface IAggregateBuilder<TState>
{
    IAggregateBuilder<TState> WithEvents(Event[] events);
    IAggregateBuilder<TState> WithLoader(LoadEventStreamDelegate loader);
    IAggregateBuilder<TState> WithApplier(ApplyEventDelegate<TState> applier);
    IAggregateBuilder<TState> WithApplier<TEvent>(Func<TState, TEvent, TState> applier) 
        where TEvent : Event;
    IAggregateBuilder<TState> WithValidator(ValidateStateDelegate<TState> validator);
    IAggregateBuilder<TState> WithValidator(Func<TState, bool> rule, string message);
}
