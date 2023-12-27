using Microsoft.Extensions.DependencyInjection;
using OMD.PlayersFeatures.Services;
using OpenMod.API.Eventing;
using OpenMod.Core.Eventing;
using OpenMod.Unturned.Players.Connections.Events;
using System.Threading.Tasks;

namespace OMD.PlayersFeatures.Events;

[EventListenerLifetime(ServiceLifetime.Transient)]
public sealed class ConnectionEvents :
    IEventListener<UnturnedPlayerDisconnectedEvent>
{
    private readonly IPlayerFeaturesService _featuresService;

    public ConnectionEvents(IPlayerFeaturesService featuresService)
    {
        _featuresService = featuresService;
    }

    public Task HandleEventAsync(object? sender, UnturnedPlayerDisconnectedEvent @event)
    {
        _featuresService.TryDisposeFor(@event.Player);

        return Task.CompletedTask;
    }
}
