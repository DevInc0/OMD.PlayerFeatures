using OMD.PlayersFeatures.Events;
using SDG.Unturned;

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
