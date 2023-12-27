using OMD.PlayersFeatures.Events;
using OpenMod.API.Eventing;
using System.Threading.Tasks;

namespace OMD.PlayersFeatures.EventTesting.Events;

public sealed class FeaturesEvents :
    IEventListener<UnturnedPlayerGodModeChangedEvent>,
    IEventListener<UnturnedPlayerVanishModeChangedEvent>
{
    public async Task HandleEventAsync(object? sender, UnturnedPlayerGodModeChangedEvent @event)
    {
        await @event.Player.PrintMessageAsync($"You set your god mode to: {@event.GodMode}");
    }

    public async Task HandleEventAsync(object? sender, UnturnedPlayerVanishModeChangedEvent @event)
    {
        await @event.Player.PrintMessageAsync($"You set your vanish mode to: {@event.VanishMode}");
    }
}
