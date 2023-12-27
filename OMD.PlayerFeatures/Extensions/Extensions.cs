using OMD.PlayersFeatures.Models;
using OMD.PlayersFeatures.Services;
using OpenMod.Unturned.Players;

namespace OMD.PlayersFeatures.Extensions;

public static class Extensions
{
    public static PlayerFeatures Features(this UnturnedPlayer player)
    {
        return PlayerFeaturesService.Instance[player];
    }
}
