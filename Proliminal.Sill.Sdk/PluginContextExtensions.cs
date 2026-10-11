using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     Shorthands for <see cref="IPluginContext"/>.
/// </summary>
public static class PluginContextExtensions
{
    /// <summary>
    ///     Adds a tile to the quick menu whose action is done right away.
    ///     See <see cref="IPluginContext.AddQuickAction"/>.
    /// </summary>
    /// <param name="context">The plugin's context.</param>
    /// <param name="title">The tile's text, such as <c>Open Recycle Bin</c>.</param>
    /// <param name="icon">The tile's icon.</param>
    /// <param name="run">What the tile does.</param>
    public static void AddQuickAction(this IPluginContext context, string title, Glyph icon, Action<IActionContext> run)
    {
        ArgumentNullException.ThrowIfNull(run);

        context.AddQuickAction(title, icon, action =>
        {
            run(action);
            return Task.CompletedTask;
        });
    }
}
