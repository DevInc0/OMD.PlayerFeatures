using OMD.PlayersFeatures.Enumerations;
using OMD.PlayersFeatures.Models;
using OpenMod.API.Ioc;
using OpenMod.Unturned.Players;

namespace OMD.PlayersFeatures.Services;



[Service]
public interface IPlayerFeaturesFactory
{
    FeaturesIntegrationType IntegrationType { get; }

    void SetIntegrationType(string type);

    PlayerFeatures CreateFor(UnturnedPlayer player);
}
