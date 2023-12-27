using OMD.PlayersFeatures.Models;
using OpenMod.API.Ioc;
using OpenMod.Unturned.Players;

namespace OMD.PlayersFeatures.Services;



[Service]
public interface IPlayerFeaturesFactory
{
    void SetIntegrationType(string type);

    PlayerFeatures CreateFor(UnturnedPlayer player);
}
