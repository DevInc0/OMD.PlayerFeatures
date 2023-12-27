using OpenMod.Unturned.RocketMod;
using SDG.Unturned;
using System;
using System.Reflection;
using UnityEngine;

namespace OMD.PlayersFeatures.Models;

public sealed class RocketModPlayerFeatures : PlayerFeatures
{
    public static readonly bool IsEnabled = false;

    private const string LegacyPlayerFeaturesTypeFullName = "Rocket.Unturned.Player.UnturnedPlayerFeatures";

    private static readonly Type LegacyPlayerFeaturesType;

    private static readonly PropertyInfo LegacyGodModePropertyInfo;

    private static readonly PropertyInfo LegacyVanishModePropertyInfo;

    /// <inheritdoc/>
    public override bool GodMode {
        get => (bool)LegacyGodModePropertyInfo.GetValue(_legacyComponent);
        set => LegacyGodModePropertyInfo.SetValue(_legacyComponent, value);
    }

    /// <inheritdoc/>
    public override bool VanishMode {
        get => (bool)LegacyVanishModePropertyInfo.GetValue(_legacyComponent);
        set => LegacyVanishModePropertyInfo.SetValue(_legacyComponent, value);
    }

    private readonly Component _legacyComponent;

    private readonly Player _player;

    static RocketModPlayerFeatures()
    {
        if (!RocketModIntegration.IsRocketModUnturnedLoaded(out var rocketUnturnedAssembly))
            throw new ArgumentException("Could not find Rocket.Unturned assembly");

        if (rocketUnturnedAssembly is null)
            throw new NullReferenceException(nameof(rocketUnturnedAssembly));

        LegacyPlayerFeaturesType = rocketUnturnedAssembly.GetType(LegacyPlayerFeaturesTypeFullName) ??
            throw new NullReferenceException(nameof(LegacyPlayerFeaturesType));
        LegacyGodModePropertyInfo = LegacyPlayerFeaturesType.GetProperty(nameof(GodMode)) ??
            throw new NullReferenceException(nameof(LegacyGodModePropertyInfo));
        LegacyVanishModePropertyInfo = LegacyPlayerFeaturesType.GetProperty(nameof(VanishMode)) ??
            throw new NullReferenceException(nameof(LegacyVanishModePropertyInfo));
        IsEnabled = true;
    }

    internal RocketModPlayerFeatures(Player player)
    {
        _player = player;
        _legacyComponent = _player.GetComponent(LegacyPlayerFeaturesType);
    }
}
