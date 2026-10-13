namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     A plugin: a feature added to Sill from outside, with a settings page that Sill draws from what the plugin
///     describes. Sill's own features implement this too.
/// </summary>
/// <remarks>
///     <para>
///         Sill creates the plugin with its constructor without parameters and configures it once, on the UI thread. It's
///         disposed when it's turned off, when it fails to start, and when Sill exits. Turning it on again creates a new
///         one, so nothing carries over in fields from one time it's on to the next; keep that in its settings.
///     </para>
///     <para>
///         Anything long-lived while it's on, such as values bound to the page, belongs in fields of the plugin. A DLL may
///         hold several plugins; they're loaded together, and when one can't be, none is.
///     </para>
/// </remarks>
public interface IPlugin : IDisposable
{
    /// <summary>
    ///     Who the plugin is: its id, display name and version. Read right after it's created, before
    ///     <see cref="Configure"/>.
    /// </summary>
    public PluginInfo Info { get; }

    /// <summary>
    ///     Sets up the plugin: its page, quick actions, settings and whatever it runs in the background. Called once on
    ///     the UI thread. Throwing here disables only this plugin; Sill disposes it and shows why.
    /// </summary>
    public void Configure(IPluginContext context);
}
