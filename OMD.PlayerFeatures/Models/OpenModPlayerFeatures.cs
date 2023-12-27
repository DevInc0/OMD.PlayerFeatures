using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;

namespace OMD.PlayersFeatures.Models;

public sealed class OpenModPlayerFeatures : PlayerFeatures
{
    public static readonly bool IsEnabled = false;

    internal static readonly HashSet<CSteamID> PlayersInGodMode = [];

    /// <inheritdoc/>
    public override bool GodMode {
        get => _godMode;
        set => SetGodMode(value);
    }

    /// <inheritdoc/>
    public override bool VanishMode {
        get => _vanishMode;
        set => SetVanishMode(value);
    }

    private bool _godMode = false;

    private bool _vanishMode = false;

    private readonly Player _player;

    static OpenModPlayerFeatures()
    {
        IsEnabled = true;
    }

    internal OpenModPlayerFeatures(Player player)
    {
        _player = player;
    }

    private void SetGodMode(bool value)
    {
        if (_godMode == value)
            return;

        var steamId = _player.channel.owner.playerID.steamID;

        _godMode = value;

        if (_godMode)
        {
            var playerLife = _player.life;

            playerLife.serverModifyHealth(float.MaxValue);
            playerLife.serverModifyFood(float.MaxValue);
            playerLife.serverModifyWater(float.MaxValue);
            playerLife.serverModifyVirus(float.MaxValue);
            playerLife.serverSetLegsBroken(false);
            playerLife.serverSetBleeding(false);

            PlayersInGodMode.Add(steamId);
        }
        else
        {
            PlayersInGodMode.Remove(steamId);
        }
    }

    private void SetVanishMode(bool value)
    {
        if (_vanishMode == value)
            return;

        var playerMovement = _player.movement;
        var playerLook = _player.look;

        playerMovement.canAddSimulationResultsToUpdates = !value;

        if (_vanishMode && !value)
        {
            var stateUpdate = new PlayerStateUpdate {
                pos = playerMovement.transform.position,
                angle = playerLook.angle,
                rot = playerLook.rot
            };

            playerMovement.updates.Add(stateUpdate);
        }

        _vanishMode = value;
    }
}