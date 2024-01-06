using HarmonyLib;
using OMD.PlayersFeatures.Models;
using SDG.Unturned;
using System;
using System.Reflection;
using UnityEngine;

namespace OMD.PlayersFeatures.Patches;

[HarmonyPatch]
internal static class AlertToolPatch
{
    private static bool ShouldNotBlockFor(Player player)
    {
        var steamId = player.channel.owner.playerID.steamID;

        return !OpenModPlayerFeatures.IsEnabled || !OpenModPlayerFeatures.PlayersInVanishMode.Contains(steamId);
    }

    [HarmonyTargetMethod]
    private static MethodBase FindTargetMethod()
    {
        var targetType = typeof(AlertTool);
        var targetMethodName = nameof(AlertTool.alert);
        var targetMethodParameterTypes = new Type[] {
            typeof(Player),
            typeof(Vector3),
            typeof(float),
            typeof(bool),
            typeof(Vector3),
            typeof(bool)
        };

        return AccessTools.Method(targetType, targetMethodName, targetMethodParameterTypes);
    }

    [HarmonyPrefix]
    private static bool AlertPrefix(Player player)
    {
        return ShouldNotBlockFor(player);
    }
}
