using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     A set of extension methods for <see cref="IPluginContext"/>.
/// </summary>
public static class PluginContextExtensions
{
    /// <summary>
    ///     Adds a tile to the quick menu whose action is done right away.
    ///     See <see cref="IPluginContext.AddQuickAction(string, Glyph, PluginAction)"/>.
    /// </summary>
    /// <param name="context">The plugin's context.</param>
    /// <param name="title">The tile's text, such as <c>Open Recycle Bin</c>.</param>
    /// <param name="icon">The tile's icon.</param>
    /// <param name="run">What the tile does.</param>
    public static void AddQuickAction(this IPluginContext context, string title, Glyph icon, Action<IActionContext> run)
        => context.AddQuickAction(title, icon, run, pinned: true);

    /// <summary>
    ///     Adds a tile to the quick menu whose action is done right away, which starts out unpinned when
    ///     <paramref name="pinned"/> is <see langword="false"/>.
    ///     See <see cref="IPluginContext.AddQuickAction(string, Glyph, PluginAction, bool)"/>.
    /// </summary>
    /// <param name="context">The plugin's context.</param>
    /// <param name="title">The tile's text, such as <c>Empty Recycle Bin</c>.</param>
    /// <param name="icon">The tile's icon.</param>
    /// <param name="run">What the tile does.</param>
    /// <param name="pinned">Whether the tile is in the menu until a user unpins it.</param>
    public static void AddQuickAction(this IPluginContext context, string title, Glyph icon, Action<IActionContext> run, bool pinned)
    {
        ArgumentNullException.ThrowIfNull(run);

        context.AddQuickAction(title, icon, action =>
        {
            run(action);
            return Task.CompletedTask;
        }, pinned);
    }
}
