using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OMD.PlayersFeatures.Extensions;
using OpenMod.API.Users;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Commands;
using OpenMod.Unturned.Users;
using System;

namespace OMD.PlayersFeatures.Commands;

[Command("god")]
[CommandDescription("Switch your god mode")]
public sealed class CommandGod : UnturnedCommand
{
    private readonly IStringLocalizer _localizer;

    private readonly IUnturnedUserDirectory _userDirectory;

    public CommandGod(IStringLocalizer localizer, IUnturnedUserDirectory userDirectory, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _localizer = localizer;
        _userDirectory = userDirectory;
    }

    protected override async UniTask OnExecuteAsync()
    {
        var player = _userDirectory.FindUser(Context.Actor.Id, UserSearchMode.FindById)!.Player;
        var features = player.Features();

        features.GodMode = !features.GodMode;

        await PrintAsync(_localizer[$"god:{(features.GodMode ? "enabled" : "disabled")}"]);
    }
}
