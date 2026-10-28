namespace Proliminal.Sill.Sdk;

/// <summary>
///     Represents a container for a group of pages that share one entry in the settings window's navigation bar.
/// </summary>
/// <example>
///     <code>
///     private static readonly PageCategory _work = new("Work", Glyph.Work, "Timers, tickets and the projects they're for.");
///
///     context.SetPage(Glyph.Clock, _work, "What's running now, and what ran today.", page => page ...);
///     </code>
/// </example>
public sealed class PageCategory
{
    /// <summary>
    ///     Gets the category name in the navigation bar, and the title of its page.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets the category icon in the navigation bar.
    /// </summary>
    public Glyph Icon { get; }

    /// <summary>
    ///     Gets a paragraph at the top of the category page, stating what it contains.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Creates a new instance of <see cref="PageCategory"/>. It's shown once a page is put in it.
    /// </summary>
    /// <param name="title">The name in the navigation bar, and the title of its page.</param>
    /// <param name="icon">The icon in the navigation bar.</param>
    /// <param name="description">A paragraph at the top of the category page, stating what it contains.</param>
    public PageCategory(string title, Glyph icon, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        Title = title;
        Icon = icon;
        Description = description;
    }

    /// <inheritdoc />
    public override string ToString()
        => Title;
}
