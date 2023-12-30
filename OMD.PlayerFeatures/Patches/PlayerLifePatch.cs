using HarmonyLib;
using OMD.PlayersFeatures.Models;
using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;

namespace OMD.PlayersFeatures.Patches;

[HarmonyPatch]
internal static class PlayerLifePatch
{
    private static bool ShouldNotBlockFor(Player player)
    {
        var steamId = player.channel.owner.playerID.steamID;

        return !OpenModPlayerFeatures.IsEnabled || !OpenModPlayerFeatures.PlayersInGodMode.Contains(steamId);
    }

    [HarmonyTargetMethods]
    private static IEnumerable<MethodBase> FindTargetMethods()
    {
        var targetType = typeof(PlayerLife);

        yield return AccessTools.Method(targetType, "askStarve");
        yield return AccessTools.Method(targetType, "askDehydrate");
        yield return AccessTools.Method(targetType, "askInfect");
        yield return AccessTools.Method(targetType, "doDamage");
        yield return AccessTools.Method(targetType, "serverSetBleeding");
        yield return AccessTools.Method(targetType, "serverSetLegsBroken");
        yield return AccessTools.Method(targetType, "breakLegs");
    }

    [HarmonyPrefix]
    private static bool LifeParametersChangersPrefix(PlayerLife __instance)
    {
        return ShouldNotBlockFor(__instance.player);
    }
}
