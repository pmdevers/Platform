
using System.ComponentModel.DataAnnotations;

namespace FinSecure.Platform.Common.Storage;

public delegate TState CreateStateDelegate<out TState>();
public delegate TState ApplyEventDelegate<TState>(TState state, Event e);
public delegate Task<IEventStream> LoadEventStreamDelegate(Guid id);

public delegate ValidationMessage[] ValidateStateDelegate<in TState>(TState state);

public class Aggregate<TState> 
    : IAggregate<TState>
{
    private readonly ApplyEventDelegate<TState> _applier;
    private readonly ValidateStateDelegate<TState> _validator;
    private readonly LoadEventStreamDelegate _loader;
    private readonly CreateStateDelegate<TState> _creator;

    internal Aggregate(
        CreateStateDelegate<TState> creator,
        ApplyEventDelegate<TState> applier,
        ValidateStateDelegate<TState> validator,
        LoadEventStreamDelegate loader)
    {
        _creator = creator;
        _applier = applier;
        _validator = validator;
        _loader = loader;

        _stream = EventStream.Create();
        State = _creator();
        UpdateState();
    }

    private IEventStream _stream;
    public TState State { get; private set; }
    public ValidationMessage[] Messages => _validator(State);
    public bool IsValid => Messages.Length == 0;
    public IEventStream Stream => _stream;
    public void Apply(Event e)
    {
        State = _applier(State, e);
        _stream.Append(e);
    }
    public async Task LoadAsync(Guid id)
    {
        _stream = await _loader(id);
        UpdateState();
    }

    private void UpdateState()
    {
        State = _stream.Aggregate(_creator(), (state, e) => _applier(state, e));
    }
}