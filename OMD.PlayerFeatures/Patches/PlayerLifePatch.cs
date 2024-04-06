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
        string[] targetMethodNames = [
            "askStarve",
            "askDehydrate",
            "askInfect",
            "doDamage",
            "serverSetBleeding",
            "serverSetLegsBroken",
            "breakLegs"
        ];

        foreach (var targetMethodName in targetMethodNames)
            yield return AccessTools.Method(targetType, targetMethodName);
    }

    [HarmonyPrefix]
    private static bool LifeParametersChangersPrefix(PlayerLife __instance)
    {
        return ShouldNotBlockFor(__instance.player);
    }
}
