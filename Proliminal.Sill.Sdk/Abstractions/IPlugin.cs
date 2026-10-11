namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     A plugin: a feature added to Sill from outside, with a settings page that Sill draws from what the plugin
///     describes. Sill's own features implement this too.
/// </summary>
/// <remarks>
///     The plugin is created once, configured once on the UI thread, and disposed when Sill exits or the plugin fails to
///     start. Anything long-lived, such as values bound to the page, belongs in fields of the plugin.
/// </remarks>
public interface IPlugin : IDisposable
{
    /// <summary>
    ///     Who the plugin is: its id, display name and version. Read before <see cref="Configure"/>.
    /// </summary>
    public PluginInfo Info { get; }

    /// <summary>
    ///     Sets up the plugin: its page, quick actions, settings and whatever it runs in the background. Called once on
    ///     the UI thread. Throwing here disables only this plugin; Sill disposes it and shows why.
    /// </summary>
    public void Configure(IPluginContext context);
}
