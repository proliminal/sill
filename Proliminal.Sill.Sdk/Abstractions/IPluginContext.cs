using Proliminal.Sill.Sdk.Pages;

namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     What Sill offers a plugin while it's configured. Call its members on the UI thread, from
///     <see cref="IPlugin.Configure"/>; <see cref="Notify"/>, <see cref="Post"/> and <see cref="Log"/> can also be used
///     later, from any thread.
/// </summary>
public interface IPluginContext
{
    /// <summary>
    ///     The plugin being configured.
    /// </summary>
    public PluginInfo Info { get; }

    /// <summary>
    ///     Sill's log, for what the plugin did and what failed. Keep it and use it from any thread, at any time.
    /// </summary>
    public IPluginLog Log { get; }

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

    /// <summary>
    ///     Runs <paramref name="work"/> on Sill's UI thread, soon after this returns. Can be called from any thread, at
    ///     any time, such as from a thread or timer the plugin started itself to update a <see cref="State{T}"/> its page
    ///     shows.
    /// </summary>
    /// <remarks>
    ///     If <paramref name="work"/> throws, Sill logs the error and shows it as a notification, and keeps running. An
    ///     exception that escapes a plugin's own thread can't be caught that way: it ends Sill. Work posted before the
    ///     plugin was turned off doesn't run.
    /// </remarks>
    /// <param name="work">What to do on the UI thread.</param>
    public void Post(Action work);

    /// <summary>
    ///     Notes something on Sill noticed's timeline, under the plugin's name: something the plugin noticed or did on its
    ///     own, such as a setting it put back. It's kept on this PC for 90 days, also when notifications are off. Can be
    ///     called from any thread, at any time.
    /// </summary>
    /// <param name="title">What happened, as a short sentence.</param>
    /// <param name="text">More about it, or empty.</param>
    /// <param name="isWarning">Whether it left the PC or account less protected, rather than being dealt with.</param>
    public void Note(string title, string text, bool isWarning = false);

    /// <summary>
    ///     Adds a recommendation to the Privacy or Security score, for a setting of the user's own account that the plugin
    ///     changes: the score counts it, and applies it with the others on Apply all. It's taken out when the plugin is
    ///     turned off. When the plugin changes what it's about itself, as with a switch on its page, call
    ///     <see cref="ScoresChanged"/>.
    /// </summary>
    /// <param name="category"><see cref="PageCategory.Privacy"/> or <see cref="PageCategory.Security"/>.</param>
    /// <param name="title">What it is, such as <c>Hide my recent files</c>.</param>
    /// <param name="actionLabel">The button that applies it, such as <c>Turn on</c>.</param>
    /// <param name="isApplied">Whether it's applied now. Called whenever a score is shown, on the UI thread, so keep it quick.</param>
    /// <param name="apply">Applies it, on the UI thread. What it throws is shown as the reason it wasn't applied.</param>
    public void AddRecommendation(PageCategory category, string title, string actionLabel, Func<bool> isApplied, Action apply);

    /// <summary>
    ///     Says what a recommendation of the plugin's is about changed, so the scores are read again. Call on the UI thread.
    /// </summary>
    public void ScoresChanged();

    /// <summary>
    ///     Shows <paramref name="problem"/> on Home, among what needs the user, with a link to the plugin's page; with
    ///     <see langword="null"/> it's gone again. For something the user can fix that keeps the plugin from working,
    ///     such as a program it needs that isn't installed. Can be called from any thread, at any time.
    /// </summary>
    /// <param name="problem">What's wrong and how to fix it, as a sentence or two; <see langword="null"/> when nothing is.</param>
    public void ReportProblem(string? problem);
}
