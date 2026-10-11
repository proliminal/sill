using System.ComponentModel;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     One row of a list: a compact card for one item.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ListItem
{
    internal ListItem(string title, string? description, Glyph? icon, IReadOnlyList<ListItemAction> actions)
    {
        Title = title;
        Description = description;
        Icon = icon;
        Actions = actions;
    }

    /// <summary>The row's title.</summary>
    public string Title { get; }

    /// <summary>Text under the title, if any.</summary>
    public string? Description { get; }

    /// <summary>The icon left of the title, if any.</summary>
    public Glyph? Icon { get; }

    /// <summary>The row's buttons, from left to right.</summary>
    public IReadOnlyList<ListItemAction> Actions { get; }
}
