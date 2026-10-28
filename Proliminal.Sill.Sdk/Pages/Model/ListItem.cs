using System.ComponentModel;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents one row of a list, shown as a compact card.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ListItem
{
    /// <summary>
    ///     Gets the row's title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets the text under the title, if any.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    ///     Gets the icon left of the title, if any.
    /// </summary>
    public Glyph? Icon { get; }

    /// <summary>
    ///     Gets the row's buttons, from left to right.
    /// </summary>
    public IReadOnlyList<ListItemAction> Actions { get; }

    internal ListItem(string title, string? description, Glyph? icon, IReadOnlyList<ListItemAction> actions)
    {
        Title = title;
        Description = description;
        Icon = icon;
        Actions = actions;
    }
}
