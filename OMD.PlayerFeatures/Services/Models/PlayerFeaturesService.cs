using Microsoft.Extensions.DependencyInjection;
using OMD.PlayersFeatures.Models;
using OpenMod.API.Ioc;
using OpenMod.Unturned.Players;
using Steamworks;
using System;
using System.Collections.Concurrent;

namespace OMD.PlayersFeatures.Services;

[PluginServiceImplementation(Lifetime = ServiceLifetime.Singleton)]
public sealed class PlayerFeaturesService : IPlayerFeaturesService, IDisposable
{
    internal static PlayerFeaturesService Instance { get; private set; } = null!;

    public PlayerFeatures this[UnturnedPlayer player] {
        get {
            EnsureCreatedFor(player);

            return _features[player.SteamId];
        }
    }

    private readonly ConcurrentDictionary<CSteamID, PlayerFeatures> _features;

    private readonly IPlayerFeaturesFactory _featuresFactory;

    public PlayerFeaturesService(IPlayerFeaturesFactory factory)
    {
        Instance = this;

        _features = [];
        _featuresFactory = factory;
    }

    public void Dispose()
    {
        Instance = null!;
    }

    private void EnsureCreatedFor(UnturnedPlayer player)
    {
        if (_features.ContainsKey(player.SteamId)) return;

        var playerFeatures = _featuresFactory.CreateFor(player);

        _features.TryAdd(player.SteamId, playerFeatures);
    }

    public void TryDisposeFor(UnturnedPlayer player)
    {
        var playerSteamId = player.SteamId;

        OpenModPlayerFeatures.PlayersInGodMode.Remove(playerSteamId);
        OpenModPlayerFeatures.PlayersInVanishMode.Remove(playerSteamId);

        _features.TryRemove(player.SteamId, out _);
    }
}
