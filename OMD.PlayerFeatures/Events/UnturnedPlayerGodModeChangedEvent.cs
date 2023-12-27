using OpenMod.Unturned.Events;
using OpenMod.Unturned.Players;

namespace OMD.PlayersFeatures.Events;

public sealed class UnturnedPlayerGodModeChangedEvent : UnturnedPlayerEvent
{
    public readonly bool GodMode;

    public UnturnedPlayerGodModeChangedEvent(UnturnedPlayer player, bool godMode) : base(player)
    {
        GodMode = godMode;
    }
}
