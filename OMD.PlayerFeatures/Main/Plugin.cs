using Cysharp.Threading.Tasks;
using HarmonyLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OMD.PlayersFeatures.Enumerations;
using OMD.PlayersFeatures.Models;
using OMD.PlayersFeatures.Patches;
using OMD.PlayersFeatures.Services;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;
using OpenMod.Unturned.RocketMod;
using System;

[assembly: PluginMetadata("OMD.PlayersFeatures", DisplayName = "OMD.PlayersFeatures", Author = "K1nd")]

namespace OMD.PlayersFeatures.Main;

public class PlayerFeaturesPlugin(
    IPlayerFeaturesService _, // initialize service
    IPlayerFeaturesFactory featuresFactory,
    IConfiguration configuration,
    ILogger<PlayerFeaturesPlugin> logger,
    IServiceProvider serviceProvider) : OpenModUnturnedPlugin(serviceProvider)
{
    protected override UniTask OnLoadAsync()
    {
        try
        {
            var integrationType = configuration.GetSection("featuresSystem").Get<string>();

            featuresFactory.SetIntegrationType(integrationType);

            logger.LogInformation("Succesfully set integration type to {IntegrationType}", featuresFactory.IntegrationType);

            PatchFeatures();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred while initializing OMD.PlayerFeatures!");
        }

        return UniTask.CompletedTask;
    }

    private void PatchFeatures()
    {
        var patchType = typeof(FeaturesPatch);
        var godModePostfixMethod = new HarmonyMethod(AccessTools.Method(patchType, nameof(FeaturesPatch.GodModePostfix)));
        var vanishModePostfixMethod = new HarmonyMethod(AccessTools.Method(patchType, nameof(FeaturesPatch.VanishModePostfix)));

        void PatchOpenModFeatures()
        {
            var targetType = typeof(OpenModPlayerFeatures);
            var targetGodModeSetter = AccessTools.PropertySetter(targetType, nameof(OpenModPlayerFeatures.GodMode));
            var targetVanishModeSetter = AccessTools.PropertySetter(targetType, nameof(OpenModPlayerFeatures.VanishMode));

            Harmony.Patch(targetGodModeSetter, postfix: godModePostfixMethod);
            Harmony.Patch(targetVanishModeSetter, postfix: vanishModePostfixMethod);
        }

        void PatchRocketModFeatures()
        {
            if (!RocketModIntegration.IsRocketModInstalled())
                throw new InvalidOperationException("Could not patch RocketMod since it is not installed!");

            var targetType = AccessTools.TypeByName("Rocket.Unturned.Player.UnturnedPlayerFeatures");
            var targetGodModeSetter = AccessTools.PropertySetter(targetType, nameof(RocketModPlayerFeatures.GodMode));
            var targetVanishModeSetter = AccessTools.PropertySetter(targetType, nameof(RocketModPlayerFeatures.VanishMode));

            Harmony.Patch(targetGodModeSetter, postfix: godModePostfixMethod);
            Harmony.Patch(targetVanishModeSetter, postfix: vanishModePostfixMethod);
        }

        switch (featuresFactory.IntegrationType)
        {
            case FeaturesIntegrationType.OpenMod:
                PatchOpenModFeatures();
                break;
            case FeaturesIntegrationType.RocketMod:
                PatchRocketModFeatures();
                break;
            default:
                throw new InvalidOperationException("Failed to patch features since integration type is not set!");
        }
    }
}
