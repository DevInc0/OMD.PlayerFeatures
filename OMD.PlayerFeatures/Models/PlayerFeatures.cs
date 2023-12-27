namespace OMD.PlayersFeatures.Models;

public abstract class PlayerFeatures
{
    /// <summary>
    /// Gets or sets player's god mode
    /// </summary>
    public abstract bool GodMode { get; set; }

    /// <summary>
    /// Gets or sets player's vanish mode
    /// </summary>
    public abstract bool VanishMode { get; set; }
}
