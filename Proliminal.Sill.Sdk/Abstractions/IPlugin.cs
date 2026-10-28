namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     Represents a plugin: a feature added to Sill from outside, with a settings page that Sill draws from what the plugin describes.
///     Sill's own features implement this too.
/// </summary>
/// <remarks>
///     <para>
///         Sill creates the plugin with its parameterless constructor and configures it once, on the UI thread.
///         It's disposed when it's turned off, when it fails to start, and when Sill exits. Turning it on again creates a new instance, so nothing carries over in fields; keep that in its settings.
///     </para>
///     <para>
///         Anything long-lived while it's on, such as values bound to the page, belongs in fields of the plugin.
///         A DLL may hold several plugins; they're loaded together, and when one can't be loaded, none are.
///     </para>
/// </remarks>
public interface IPlugin : IDisposable
{
    /// <summary>
    ///     Gets the plugin's ID, display name and version. Read right after it's created, before <see cref="Configure"/>.
    /// </summary>
    public PluginInfo Info { get; }

    /// <summary>
    ///     Sets up the plugin: its page, quick actions, settings and whatever it runs in the background. Called once, on the UI thread.
    ///     Throwing here disables only this plugin; Sill disposes it and shows why.
    /// </summary>
    /// <remarks>
    ///     Sill waits for a plugin at startup and logs one that takes longer than a second.
    ///     Run heavy work, such as reading a large file or the network, on a thread of its own and show its result with <see cref="IPluginContext.Post"/>.
    /// </remarks>
    /// <param name="context">What Sill offers the plugin while it's configured.</param>
    public void Configure(IPluginContext context);
}
