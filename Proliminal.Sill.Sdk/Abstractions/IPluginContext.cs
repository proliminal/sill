using Proliminal.Sill.Sdk.Pages;

namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     What Sill offers a plugin while it's configured. Call its members on the UI thread, from
///     <see cref="IPlugin.Configure"/>; <see cref="Notify"/> can also be called later.
/// </summary>
public interface IPluginContext
{
    /// <summary>
    ///     The plugin being configured.
    /// </summary>
    public PluginInfo Info { get; }

    /// <summary>
    ///     Gives the plugin a page in the settings window's navigation bar, titled with its name. <paramref name="build"/>
    ///     is called once, right away, to describe the page; mistakes in it, such as a toggle without a value, throw here.
    /// </summary>
    /// <param name="icon">The icon in the navigation bar.</param>
    /// <param name="build">Describes the page's cards.</param>
    public void SetPage(Glyph icon, Action<PageBuilder> build);

    /// <summary>
    ///     Gives the plugin a page within <paramref name="category"/>'s page instead, which links to it with
    ///     <paramref name="description"/>. See <see cref="SetPage(Glyph, Action{PageBuilder})"/>.
    /// </summary>
    /// <param name="icon">The icon of the link to the page.</param>
    /// <param name="category">The category the page is in.</param>
    /// <param name="description">A sentence under the link, saying what's on the page.</param>
    /// <param name="build">Describes the page's cards.</param>
    public void SetPage(Glyph icon, PageCategory category, string description, Action<PageBuilder> build);

    /// <summary>
    ///     Adds cards to the top of <paramref name="category"/>'s page, above the links to its pages, such as a
    ///     <see cref="Pages.Abstractions.CardsBuilder{TSelf}.Score"/> of what its pages set. Headers of several plugins are
    ///     shown in the order the plugins started.
    /// </summary>
    /// <param name="category">The category whose page gets the cards.</param>
    /// <param name="build">Describes the cards, as for a page.</param>
    public void SetCategoryHeader(PageCategory category, Action<PageBuilder> build);

    /// <summary>
    ///     Adds a tile to the quick menu that opens from the tray icon. <paramref name="run"/> is called on the UI
    ///     thread once the menu has closed; if it throws, Sill shows the error as a notification.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Users arrange the quick menu themselves: they can move tiles, unpin them and pin them back from the
    ///         menu's edit mode. So a tile may not be shown, and its place isn't up to the plugin. A new tile goes at the
    ///         end, pinned; see <see cref="AddQuickAction(string, Glyph, PluginAction, bool)"/> for one that starts out
    ///         unpinned.
    ///     </para>
    ///     <para>
    ///         Sill remembers the arrangement by the plugin's <see cref="PluginInfo.Id"/> and the tile's
    ///         <paramref name="title"/>. Keep the title the same between versions: a tile whose title changed is new to
    ///         Sill, and goes back to the end. A plugin's tiles need titles that differ from each other.
    ///     </para>
    /// </remarks>
    /// <param name="title">The tile's text, such as <c>Open Recycle Bin</c>.</param>
    /// <param name="icon">The tile's icon.</param>
    /// <param name="run">What the tile does.</param>
    public void AddQuickAction(string title, Glyph icon, PluginAction run);

    /// <summary>
    ///     Adds a tile to the quick menu that starts out unpinned when <paramref name="pinned"/> is
    ///     <see langword="false"/>: it's only shown once a user pins it from the menu's edit mode, such as for an action
    ///     few want at hand or one that's hard to undo. See <see cref="AddQuickAction(string, Glyph, PluginAction)"/>.
    /// </summary>
    /// <remarks>
    ///     It only decides how the tile starts out. Once a user has pinned or unpinned it, Sill keeps their choice.
    /// </remarks>
    /// <param name="title">The tile's text, such as <c>Empty Recycle Bin</c>.</param>
    /// <param name="icon">The tile's icon.</param>
    /// <param name="run">What the tile does.</param>
    /// <param name="pinned">Whether the tile is in the menu until a user unpins it.</param>
    public void AddQuickAction(string title, Glyph icon, PluginAction run, bool pinned);

    /// <summary>
    ///     Gets the plugin's settings, stored under its <see cref="PluginInfo.Id"/> in <c>settings.json</c>. Missing
    ///     options are written with their defaults, which are the property initializers of <typeparamref name="T"/>.
    ///     Call it once and keep the result; it stays up to date when the file is edited.
    /// </summary>
    /// <typeparam name="T">A class with a property per option, serialized as JSON.</typeparam>
    public IPluginSettings<T> GetSettings<T>()
        where T : class, new();

    /// <summary>
    ///     Shows a Windows notification from Sill's tray icon. Can be called from any thread, at any time.
    /// </summary>
    /// <param name="title">The notification's first line.</param>
    /// <param name="text">The notification's text.</param>
    /// <param name="kind">The icon shown with it.</param>
    public void Notify(string title, string text, NotificationKind kind = NotificationKind.None);
}
