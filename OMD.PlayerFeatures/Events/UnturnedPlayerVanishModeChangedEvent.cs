using OpenMod.Unturned.Events;
using OpenMod.Unturned.Players;

namespace OMD.PlayersFeatures.Events;

public sealed class UnturnedPlayerVanishModeChangedEvent : UnturnedPlayerEvent
{
    public readonly bool VanishMode;

    public UnturnedPlayerVanishModeChangedEvent(UnturnedPlayer player, bool vanishMode) : base(player)
    {
        VanishMode = vanishMode;
    }
}
