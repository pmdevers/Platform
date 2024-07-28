using Microsoft.OpenApi.Validations;

namespace FinSecure.Platform.Common.Storage;

internal static class AggregateBuilderDefaults
{
    public static ApplyEventDelegate<TState> DefaultApplier<TState>(Dictionary<Type, ApplyEventDelegate<TState>> events)
        => (TState state, Event e) => events.TryGetValue(e.GetType(), out var applier)
            ? applier(state, e)
            : state;

    public static ValidateStateDelegate<TState> DefaultValidator<TState>(List<ValidationRule<TState>> rules) =>
        (TState state) => rules.Where(x => x.Rule(state))
                         .Select(x => new ValidationMessage(x.Message))
                         .ToArray();

    public static LoadEventStreamDelegate DefaultLoader(Event[]? events = null) 
        => (Guid streamId) => Task.FromResult(EventStream.Create(streamId, events));
}
public record ValidationRule<TState>(Func<TState, bool> Rule, string Message);
