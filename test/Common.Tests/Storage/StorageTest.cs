using FinSecure.Platform.Common.Storage;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Tests.Storage;
public class StorageTest
{
    [Fact]
    public void ApiTest()
    {
        var services = new ServiceCollection();
        var provider = services.BuildServiceProvider();

       var builder = AggregateBuilder.Create(State.Initial);

       builder.WithApplier<StateCreated>(Created)
              .WithApplier<StateNameChanged>(ChangeNameFeature.NameChanged);

        var aggregate = builder.Build(provider);
        
        aggregate.Apply(new StateCreated(Guid.NewGuid(), "Hello World"));
        aggregate.Apply(new StateNameChanged("Hello"));

        aggregate.State.Name.Should().Be("Hello");
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
    public async Task LoadFromHistory1()
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

        aggregate.State.Name.Should().Be("How about state");
    }


    [Fact]
    public async Task LoadFromHistory()
    {
        var services = new ServiceCollection();
        var streamId = StreamId.Guid();

        
        var streamManager = new StreamManager(streamId, [
            new StateCreated(Guid.NewGuid(), "Hello World"),
            new StateNameChanged("How about state")
        ]);

        services.AddAggregate<State>(new())
            .WithLoader((x) => streamManager.LoadAsync)
            .WithSaver((x) => SaveAggregate)
            .WithApplier<StateCreated>(Created)
            .CanChangeName();

        var provider = services.BuildServiceProvider();

        var aggregate = provider.GetRequiredService<IAggregate<State>>();
        var id = StreamId.Guid();

        await aggregate.LoadAsync(id);

        aggregate.State.Name.Should().BeNull();

        await aggregate.LoadAsync(streamId);

        aggregate.State.Name.Should().Be("How about state");

        var invalidSave = () => aggregate.SaveAsync();

        await invalidSave.Should().ThrowAsync<InvalidOperationException>();

        aggregate.ChangeName("Test1");

        await aggregate.SaveAsync();
    }


    public async static Task<IEventStream> SaveAggregate(IAggregate<State> aggregate)
    {
        if (!aggregate.IsValid)
        {
            throw new InvalidOperationException("Can not save aggregate in an invalid state.");
        }

        foreach (var item in aggregate.Stream.GetUncommittedEvents())
        {
            await Task.CompletedTask;
        }

        var result = aggregate.Stream.Commit();
        return result;
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




public static class ChangeNameFeature
{

    public static IAggregateBuilder<State> CanChangeName(this IAggregateBuilder<State> builder)
    {
        builder.WithValidator((s) => s.Name.Length > 7, "Name To Long.")
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
