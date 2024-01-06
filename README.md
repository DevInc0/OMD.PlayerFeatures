# OMD.PlayersFeatures | [![Nuget](https://img.shields.io/nuget/v/OMD.PlayersFeatures)](https://www.nuget.org/packages/OMD.PlayersFeatures/) [![Nuget](https://img.shields.io/nuget/dt/OMD.PlayersFeatures?label=nuget%20downloads)](https://www.nuget.org/packages/OMD.PlayersFeatures/)
PlayersFeatures is a plugin for Unturned / OpenMod. 

It provides a more ellegant way to use God and Vanish modes without LDM to be installed. 

However, it can also integrate with LDM.

### Remarks
**God mode** makes a player immortal, he won't get damage, broke legs, get a bleeding and so on.

**Vanish mode** makes a player invisible. Other players won't see him. **Zombies and animals won't be alerted by a player in vanish mode.**
![Vanish Alert Blocking Demo](https://github.com/K1nd0/OMD.PlayersFeatures/blob/master/Vanish-Alert-Blocking.gif)

# How to install
Run command `openmod install OMD.PlayersFeatures`

**If you are using RocketMod (even if you set `featuresSystem` to `RocketMod` in configuration file):**
- Make sure you disabled `god` command in `.../Rocket/Commands.config.xml` file
- Make sure you disable `vanish` command in `.../Rocket/Commands.config.xml` file

Example:
```xml
...
<Command Name="god" Enabled="false" Priority="Normal">Rocket.Unturned.Commands.CommandGod/god</Command>
<Command Name="vanish" Enabled="false" Priority="Normal">Rocket.Unturned.Commands.CommandVanish/vanish</Command>
...
```

## Commands
- /god - Switches your god mode.
- /vanish - Switches your vanish mode.

## Permissions
- OMD.PlayersFeatures:commands.god: Grants access to the `/god` command.
- OMD.PlayersFeatures:commands.vanish: Grants access to the `/vanish` command.

## Configuration
```yaml
# Sets the features system to use
# Available values:
#  - OpenMod:   plugin will use custom OpenMod's features system (default)    
#  - RocketMod: plugin will use legacy RocketMod's features system
featuresSystem: OpenMod 
```

## Translations
```yaml
# Prints to player, whenever he uses /god command
god:
  enabled: <color=green>You enabled god mode!
  disabled: <color=yellow>You disabled god mode!

# Prints to player, whenever he uses /vanish command
vanish:
  enabled: <color=green>You enabled vanish mode!
  disabled: <color=yellow>You disabled vanish mode!
```

# For developers

Developers can also access player's features through code. Here's an example of how to do so:

```cs
using OpenMod.Unturned.Players;
using OMD.PlayersFeatures.Extensions; // provides an extension method to access player's features

private void Foo(UnturnedPlayer player)
{
    PlayerFeautres features = player.Features(); // gets player's features via extensions method

    bool currentGodMode = features.GodMode; // gets player's current god mode
    bool currentVanishMode = fatures.VanishMode; // gets player's current vanish mode

    features.GodMode = true; // sets player's god mode
    features.VanishMode = false; // sets player's vanish mode
}
```

By doing so you can access either RocketMod or OpenMod player's features, depends on which `featuresSystem` type is set in configuration file.

You can find `class PlayerFeatures` implementation over here: [link](https://github.com/DevInc0/OMD.PlayerFeatures/blob/master/OMD.PlayerFeatures/Models/PlayerFeatures.cs)
