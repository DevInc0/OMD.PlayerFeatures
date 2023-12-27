using OMD.PlayersFeatures.Events;
using OMD.PlayersFeatures.Models;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMD.PlayersFeatures.Patches;

internal static class FeaturesPatch
{
    internal static void GodModePostfix(Player ____player, bool value)
    {
        PlayerFeaturesEventsHandler.InvokeOnPlayerGodModeChanged(____player, value);
    }

    internal static void VanishModePostfix(Player ____player, bool value)
    {
        PlayerFeaturesEventsHandler.InvokeOnPlayerVanishModeChanged(____player, value);
    }
}
