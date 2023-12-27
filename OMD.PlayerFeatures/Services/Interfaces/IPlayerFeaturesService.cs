using OMD.PlayersFeatures.Models;
using OpenMod.API.Ioc;
using OpenMod.Unturned.Players;

namespace OMD.PlayersFeatures.Services;

[Service]
public interface IPlayerFeaturesService
{
    PlayerFeatures this[UnturnedPlayer player] { get; }

    void TryDisposeFor(UnturnedPlayer player);
}
