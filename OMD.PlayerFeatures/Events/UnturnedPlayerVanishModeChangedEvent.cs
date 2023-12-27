using OpenMod.Unturned.Events;
using OpenMod.Unturned.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMD.PlayersFeatures.Events;

public sealed class UnturnedPlayerVanishModeChangedEvent : UnturnedPlayerEvent
{
    public readonly bool VanishMode;

    public UnturnedPlayerVanishModeChangedEvent(UnturnedPlayer player, bool vanishMode) : base(player)
    {
        VanishMode = vanishMode;
    }
}
