using OMD.PlayersFeatures.Enumerations;
using OMD.PlayersFeatures.Models;
using OpenMod.API.Ioc;
using OpenMod.Unturned.Players;
using System;

namespace OMD.PlayersFeatures.Services;

[PluginServiceImplementation]
public sealed class PlayerFeaturesFactory : IPlayerFeaturesFactory
{
    private FeaturesIntegrationType _type;

    public void SetIntegrationType(string type)
    {
        if (!Enum.TryParse(type, true, out _type))
        {
            _type = FeaturesIntegrationType.None;

            throw new ArgumentException($"Failed to parse \"{type}\" to {nameof(FeaturesIntegrationType)}");
        }
    }

    public PlayerFeatures CreateFor(UnturnedPlayer player) => _type switch {
        FeaturesIntegrationType.RocketMod => new RocketModPlayerFeatures(player.Player),
        FeaturesIntegrationType.OpenMod => new OpenModPlayerFeatures(player.Player),
        _ => throw new ArgumentException("Features integration type is not set!"),
    };
}
