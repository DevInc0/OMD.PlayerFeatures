using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMD.Events.Models;
using OMD.Events.Services;
using SDG.Unturned;

namespace OMD.PlayersFeatures.Events;

internal sealed class PlayerFeaturesEventsHandler : EventsHandler
{
    private static event Action<Player, bool>? PlayerGodModeChanged;

    private static event Action<Player, bool>? PlayerVanishModeChanged;

    internal PlayerFeaturesEventsHandler(IEventsService eventsService) : base(eventsService) { }

    public override void Subscribe()
    {
        PlayerGodModeChanged += Events_OnPlayerGodModeChanged;
        PlayerVanishModeChanged += Events_OnPlayerVanishModeChanged;
    }

    public override void Unsubscribe()
    {
        PlayerGodModeChanged -= Events_OnPlayerGodModeChanged;
        PlayerVanishModeChanged -= Events_OnPlayerVanishModeChanged;
    }

    private void Events_OnPlayerGodModeChanged(Player player, bool value)
    {
        var unturnedPlayer = GetUnturnedPlayer(player);
        var @event = new UnturnedPlayerGodModeChangedEvent(unturnedPlayer, value);

        Emit(@event);
    }

    private void Events_OnPlayerVanishModeChanged(Player player, bool value)
    {
        var unturnedPlayer = GetUnturnedPlayer(player);
        var @event = new UnturnedPlayerVanishModeChangedEvent(unturnedPlayer, value);

        Emit(@event);
    }

    internal static void InvokeOnPlayerGodModeChanged(Player player, bool value)
    {
        PlayerGodModeChanged?.Invoke(player, value);
    }

    internal static void InvokeOnPlayerVanishModeChanged(Player player, bool value)
    {
        PlayerVanishModeChanged?.Invoke(player, value);
    }
}
