using Microsoft.Extensions.DependencyInjection;

namespace FinSecure.Platform.Common.Storage;

public static class AggregateBuilder 
{
    public static AggregateBuilder<TState> Create<TState>(TState initialState)
        => Create(() => initialState);

    public static AggregateBuilder<TState> Create<TState>(CreateStateDelegate<TState> creator)
        => new(creator);
}

public class AggregateBuilder<TState> : IAggregateBuilder<TState>
{
    private readonly Dictionary<Type, ApplyEventDelegate<TState>> _events = [];
    private readonly List<ValidationRule<TState>> _rules = [];
    private readonly CreateStateDelegate<TState> _creator;

    private ValidateStateDelegate<TState>? _validator = null;
    private ApplyEventDelegate<TState>? _applier = null;
    private LoadEventStreamDelegate? _loader = null;

    internal AggregateBuilder(CreateStateDelegate<TState> creator)
    {
        _creator = creator;
    }
    public IAggregateBuilder<TState> WithEvents(Event[] events)
    {
        _loader = AggregateBuilderDefaults.DefaultLoader(events);
        return this;
    }

    public IAggregate<TState> Build()
    {
        var applier = _applier ?? AggregateBuilderDefaults.DefaultApplier(_events);
        var validator = _validator ?? AggregateBuilderDefaults.DefaultValidator(_rules);
        var loader = _loader ?? AggregateBuilderDefaults.DefaultLoader();

        return new Aggregate<TState>(_creator, applier, validator, loader);
    }

    public IAggregateBuilder<TState> WithLoader(LoadEventStreamDelegate loader)
    {
        _loader = loader;
        return this;
    }

    public IAggregateBuilder<TState> WithValidator(ValidateStateDelegate<TState> validator)
    {
        _validator = validator;
        return this;
    }

    public IAggregateBuilder<TState> WithValidator(Func<TState, bool> rule, string message)
    {
        _rules.Add(new(rule, message));
         return this;
    }

    public IAggregateBuilder<TState> WithApplier(ApplyEventDelegate<TState> applier)
    {
        _applier = applier;
        return this;
    }

    public IAggregateBuilder<TState> WithApplier<TEvent>(Func<TState, TEvent, TState> applier) where TEvent : Event
    {
        _events.Add(typeof(TEvent), (state, e) => applier(state, (TEvent)e));
        return this;
    }
}
