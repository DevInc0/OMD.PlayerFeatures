using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OMD.PlayersFeatures.Services;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;
using System;

[assembly: PluginMetadata("OMD.PlayerFeatures", DisplayName = "OMD.PlayerFeatures", Author = "K1nd")]

namespace OMD.PlayersFeatures.Main;

public class PlayerFeaturesPlugin : OpenModUnturnedPlugin
{
    private readonly IPlayerFeaturesFactory _featuresFactory;

    private readonly IConfiguration _configuration;

    private readonly ILogger<PlayerFeaturesPlugin> _logger;

    public PlayerFeaturesPlugin(
        IPlayerFeaturesFactory featuresFactory,
        IConfiguration configuration,
        ILogger<PlayerFeaturesPlugin> logger,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _featuresFactory = featuresFactory;
        _configuration = configuration;
        _logger = logger;
    }

    protected override UniTask OnLoadAsync()
    {
        try
        {
            var integrationType = _configuration.GetSection("featuresSystem").Get<string>();

            _featuresFactory.SetIntegrationType(integrationType);

            _logger.LogInformation("Succesfully set integration type to {IntegrationType}", integrationType);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while setting features integration type!");
        }

        return UniTask.CompletedTask;
    }
}
