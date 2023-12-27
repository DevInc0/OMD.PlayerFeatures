using OpenMod.Unturned.Events;
using OpenMod.Unturned.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMD.PlayersFeatures.Events;

public sealed class UnturnedPlayerGodModeChangedEvent : UnturnedPlayerEvent
{
    public readonly bool GodMode;

    public UnturnedPlayerGodModeChangedEvent(UnturnedPlayer player, bool godMode) : base(player)
    {
        GodMode = godMode;
    }
}
