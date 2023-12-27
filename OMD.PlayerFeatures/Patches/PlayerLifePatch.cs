using HarmonyLib;
using OMD.PlayersFeatures.Models;
using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;

namespace OMD.PlayersFeatures.Patches;

[HarmonyPatch(typeof(PlayerLife))]
internal static class PlayerLifePatch
{
    private static bool ShouldBlockFor(Player player)
    {
        var steamId = player.channel.owner.playerID.steamID;

        return OpenModPlayerFeatures.IsEnabled && OpenModPlayerFeatures.PlayersInGodMode.Contains(steamId);
    }

    [HarmonyTargetMethods]
    private static IEnumerable<MethodBase> FindTargetMethods()
    {
        var targetType = typeof(PlayerLife);

        yield return targetType.GetMethod("askStarve");
        yield return targetType.GetMethod("askDehydrate");
        yield return targetType.GetMethod("askInfect");
        yield return targetType.GetMethod("doDamage");
        yield return targetType.GetMethod("serverSetBleeding");
        yield return targetType.GetMethod("serverSetLegsBroken");
        yield return targetType.GetMethod("breakLegs");
    }

    [HarmonyPrefix]
    private static bool LifeParametersChangersPrefix(PlayerLife __instance)
    {
        return ShouldBlockFor(__instance.player);
    }
}
