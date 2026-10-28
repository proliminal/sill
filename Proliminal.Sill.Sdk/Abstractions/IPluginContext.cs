using Proliminal.Sill.Sdk.Pages;

namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     Represents what Sill offers a plugin while it's configured.
///     Call its members on the UI thread, from <see cref="IPlugin.Configure"/>; <see cref="Notify"/>, <see cref="Post"/>, <see cref="Note"/>, <see cref="ReportProblem"/> and <see cref="Log"/> can also be used later, from any thread.
/// </summary>
public interface IPluginContext
{
    /// <summary>
    ///     Gets the plugin being configured.
    /// </summary>
    public PluginInfo Info { get; }

    /// <summary>
    ///     Gets Sill's log, for what the plugin did and what failed. Keep it and use it from any thread, at any time.
    /// </summary>
    public IPluginLog Log { get; }

    #region Pages

    /// <summary>
    ///     Sets the plugin's page in the settings window's navigation bar, titled with the plugin's name.
    ///     <paramref name="build"/> is called once, right away, to describe the page; mistakes in it, such as a toggle without a value, throw here.
    /// </summary>
    /// <param name="icon">The icon in the navigation bar.</param>
    /// <param name="build">Describes the page's cards.</param>
    public void SetPage(Glyph icon, Action<PageBuilder> build);

    /// <summary>
    ///     Sets the plugin's page within <paramref name="category"/>'s page instead, which links to it with <paramref name="description"/>.
    ///     The category is the plugin's own, and is in the navigation bar while a page is in it. See <see cref="SetPage(Glyph, Action{PageBuilder})"/>.
    /// </summary>
    /// <param name="icon">The icon of the link to the page.</param>
    /// <param name="category">The category the page is in, made by the plugin.</param>
    /// <param name="description">A sentence under the link, stating what's on the page.</param>
    /// <param name="build">Describes the page's cards.</param>
    public void SetPage(Glyph icon, PageCategory category, string description, Action<PageBuilder> build);

    /// <summary>
    ///     Sets cards at the top of <paramref name="category"/>'s page, above the links to its pages, such as a <see cref="Pages.Abstractions.CardsBuilder{TSelf}.Score"/> of what its pages set.
    /// </summary>
    /// <param name="category">The category whose page gets the cards, made by the plugin.</param>
    /// <param name="build">Describes the cards, as for a page.</param>
    public void AddCategoryHeader(PageCategory category, Action<PageBuilder> build);

    #endregion

    #region Quick Menu

    /// <summary>
    ///     Adds a tile to the quick menu that opens from the tray icon.
    ///     <paramref name="run"/> is called on the UI thread once the menu has closed; if it throws, Sill shows the error as a notification.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Users arrange the quick menu themselves: they can move, unpin and pin tiles from the menu's edit mode. A tile may therefore not be shown, and its position isn't up to the plugin.
    ///         A new tile goes at the end, pinned; see <see cref="AddQuickAction(string, Glyph, PluginAction, bool)"/> for one that starts out unpinned.
    ///     </para>
    ///     <para>
    ///         Sill remembers the arrangement by the plugin's <see cref="PluginInfo.Id"/> and the tile's <paramref name="title"/>.
    ///         Keep the title the same between versions: a tile whose title changed is new to Sill, and goes back to the end. A plugin's tiles need distinct titles.
    ///     </para>
    /// </remarks>
    /// <param name="title">The tile's text, such as <c>Open Recycle Bin</c>.</param>
    /// <param name="icon">The tile's icon.</param>
    /// <param name="run">What the tile does.</param>
    public void AddQuickAction(string title, Glyph icon, PluginAction run);

    /// <summary>
    ///     Adds a tile to the quick menu that starts out unpinned when <paramref name="pinned"/> is <see langword="false"/>, such as for an action few people need at hand or one that's hard to undo.
    ///     An unpinned tile is only shown once a user pins it from the menu's edit mode; after that, Sill keeps the user's choice.
    ///     See <see cref="AddQuickAction(string, Glyph, PluginAction)"/>.
    /// </summary>
    /// <param name="title">The tile's text, such as <c>Empty Recycle Bin</c>.</param>
    /// <param name="icon">The tile's icon.</param>
    /// <param name="run">What the tile does.</param>
    /// <param name="pinned">Whether the tile is in the menu until a user unpins it.</param>
    public void AddQuickAction(string title, Glyph icon, PluginAction run, bool pinned);

    /// <summary>
    ///     Adds a section to the top of the quick menu, above its tiles, such as what's running now with a button that stops it.
    ///     Unlike a tile, it follows its values while the menu is open, can hide itself with <see cref="QuickMenuSectionBuilder.VisibleWhen"/>, and isn't arranged by users.
    ///     Sections of several plugins are shown in the order the plugins started. <paramref name="build"/> is called once, right away.
    /// </summary>
    /// <example>
    ///     <code>
    ///     context.AddQuickMenuSection("Timer", section => section
    ///         .Status(elapsed)
    ///         .Description(taskName)
    ///         .VisibleWhen(isRunning)
    ///         .Action("Stop", StopAsync));
    ///     </code>
    /// </example>
    /// <param name="title">The section's title.</param>
    /// <param name="build">Sets the section's status, description, buttons and when it's shown.</param>
    public void AddQuickMenuSection(string title, Action<QuickMenuSectionBuilder> build);

    #endregion

    #region Settings

    /// <summary>
    ///     Gets the plugin's settings, stored under its <see cref="PluginInfo.Id"/> in <c>settings.json</c>.
    ///     Missing options are written with their defaults, which are the property initializers of <typeparamref name="T"/>.
    ///     Call it once and keep the result; it stays up to date when the file is edited.
    /// </summary>
    /// <typeparam name="T">A class with a property per option, serialized as JSON.</typeparam>
    public IPluginSettings<T> GetSettings<T>()
        where T : class, new();

    /// <summary>
    ///     Gets a secret of the plugin's, such as an API token, to read or to bind a <see cref="TextBoxBuilder.Secret"/> box to.
    ///     Sill keeps it encrypted for the Windows account in a separate file on this PC, never in <c>settings.json</c>: it isn't part of an exported setup, <c>sill setting</c> can't read it, and other plugins don't get it.
    ///     It's empty until it's set, and setting it to empty removes it. Call it once per name and keep the result.
    /// </summary>
    /// <remarks>
    ///     It protects the secret from files and setups, not from programs that run as the same Windows account, which can do what Sill does.
    /// </remarks>
    /// <param name="name">A name that never changes, of letters and digits starting with a letter, such as <c>ApiToken</c>.</param>
    public IValue<string> GetSecret(string name);

    #endregion

    #region Threading and Messages

    /// <summary>
    ///     Shows a Windows notification from Sill's tray icon. Can be called from any thread, at any time.
    /// </summary>
    /// <param name="title">The notification's first line.</param>
    /// <param name="text">The notification's text.</param>
    /// <param name="kind">The icon shown with it.</param>
    public void Notify(string title, string text, NotificationKind kind = NotificationKind.None);

    /// <summary>
    ///     Runs <paramref name="work"/> on Sill's UI thread, soon after this returns.
    ///     Can be called from any thread, at any time, such as from a thread or timer the plugin started to update a <see cref="State{T}"/> its page shows.
    /// </summary>
    /// <remarks>
    ///     If <paramref name="work"/> throws, Sill logs the error, shows it as a notification, and keeps running.
    ///     An exception that escapes a thread the plugin started can't be caught that way, and ends Sill. Work posted before the plugin was turned off doesn't run.
    /// </remarks>
    /// <param name="work">What to do on the UI thread.</param>
    public void Post(Action work);

    /// <summary>
    ///     Adds an entry to the Sill noticed timeline, under the plugin's name, for something the plugin noticed or did on its own, such as a setting it restored.
    ///     It's kept on this PC for 90 days, also when notifications are off. Can be called from any thread, at any time.
    /// </summary>
    /// <param name="title">What happened, as a short sentence.</param>
    /// <param name="text">More about it, or empty.</param>
    /// <param name="isWarning">Whether it left the PC or account less protected, rather than being dealt with.</param>
    public void Note(string title, string text, bool isWarning = false);

    /// <summary>
    ///     Shows <paramref name="problem"/> on Home, among what needs the user's attention, with a link to the plugin's page; <see langword="null"/> removes it again.
    ///     Use it for something the user can fix that keeps the plugin from working, such as a program it needs that isn't installed. Can be called from any thread, at any time.
    /// </summary>
    /// <param name="problem">What's wrong and how to fix it, as a sentence or two; <see langword="null"/> when nothing is.</param>
    public void ReportProblem(string? problem);

    #endregion

    #region Scores

    /// <summary>
    ///     Adds a recommendation to the Privacy or Security score, for a setting of the user's own account that the plugin changes.
    ///     The score counts it, and applies it with the others on Apply all. It's removed when the plugin is turned off.
    ///     When the plugin changes the setting itself, such as with a switch on its page, call <see cref="RefreshScores"/>.
    /// </summary>
    /// <param name="score">The score it counts toward.</param>
    /// <param name="title">What it is, such as <c>Hide my recent files</c>.</param>
    /// <param name="actionLabel">The text of the button that applies it, such as <c>Turn on</c>.</param>
    /// <param name="isApplied">Whether it's applied now. Called on the UI thread whenever a score is shown, so keep it quick.</param>
    /// <param name="apply">Applies it, on the UI thread. What it throws is shown as the reason it wasn't applied.</param>
    public void AddRecommendation(RecommendationScore score, string title, string actionLabel, Func<bool> isApplied, Action apply);

    /// <summary>
    ///     Reads the scores again, after what one of the plugin's recommendations is about changed. Call it on the UI thread.
    /// </summary>
    public void RefreshScores();

    #endregion
}
