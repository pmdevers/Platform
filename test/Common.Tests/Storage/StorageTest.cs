using FinSecure.Platform.Common.Storage;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Tests.Storage;
public class StorageTest
{
    [Fact]
    public void ApiTest()
    {
       var builder = AggregateBuilder.Create(State.Initial);

       builder.WithApplier<StateCreated>(Created)
              .WithApplier<StateNameChanged>(ChangeNameFeature.NameChanged);

        var aggregate = builder.Build();
        
        aggregate.Apply(new StateCreated(Guid.NewGuid(), "Hello World"));
        aggregate.Apply(new StateNameChanged("Hello"));

    }

    [Fact]
    public void FromServices()
    {
        var services = new ServiceCollection();

        services.AddAggregate<State>(new())
            .WithApplier<StateCreated>(Created)
            .CanChangeName();
           
        var provider = services.BuildServiceProvider();

        var aggregate = provider.GetRequiredService<IAggregate<State>>();

        aggregate.Apply(new StateCreated(Guid.NewGuid(), "Hello World"));
        aggregate.ChangeName("Hello");

        aggregate.ChangeName("Hello!!");

        aggregate.IsValid.Should().BeFalse();
    }


    [Fact]
    public async Task LoadFromHistory()
    {
        var services = new ServiceCollection();

        services.AddAggregate<State>(new())
            .WithEvents([
                new StateCreated(Guid.NewGuid(), "Hello World"),
                new StateNameChanged("How about state"),
                ])
            .WithApplier<StateCreated>(Created)
            .CanChangeName();

        var provider = services.BuildServiceProvider();

        var aggregate = provider.GetRequiredService<IAggregate<State>>();
        var id = StreamId.Guid();

        await aggregate.LoadAsync(id);

    }

    
    private State Created(State state, StateCreated created)
    {
        return state with
        {
            Id = created.Id,
            Name = created.Name,
        };
    }
    

}


public static class ServiceCollectionExtensions
{
    public static IAggregateBuilder<TState> AddAggregate<TState>(this IServiceCollection services, TState initialState)
    {
        var builder = AggregateBuilder.Create(initialState);
        services.AddSingleton(builder);
        services.AddScoped(x => x.GetRequiredService<AggregateBuilder<TState>>().Build());
        return builder;
    }
}

public static class ChangeNameFeature
{

    public static IAggregateBuilder<State> CanChangeName(this IAggregateBuilder<State> builder)
    {
        builder.WithValidator((s) => s.Name.Length > 2, "Name To Long.")
            .WithApplier<StateNameChanged>(NameChanged);
        return builder;
    }

    public static void ChangeName(this IAggregate<State> aggregate, string name)
        => aggregate.Apply(new StateNameChanged(name));
        
    public static State NameChanged(State state, StateNameChanged changed)
    {
        return state with
        {
            Name = changed.Name
        };
    }
}


public readonly record struct State(Guid Id, string Name)
{
    public static State Initial => new(Guid.Empty, string.Empty);
}
public record StateCreated(Guid Id, string Name) : Event;
public record StateNameChanged(string Name) : Event;
