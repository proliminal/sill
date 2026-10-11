using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A small icon button on one row of a list, such as Remove.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ListItemAction
{
    internal ListItemAction(Glyph icon, string label, PluginAction onClick, bool isEnabled, IReadOnlyValue<bool>? enabledWhen)
    {
        Icon = icon;
        Label = label;
        OnClick = onClick;
        IsEnabled = isEnabled;
        EnabledWhen = enabledWhen;
    }

    /// <summary>The button's icon.</summary>
    public Glyph Icon { get; }

    /// <summary>What the button does, shown as its tooltip and read out by screen readers.</summary>
    public string Label { get; }

    /// <summary>What the button does, for this row's item.</summary>
    public PluginAction OnClick { get; }

    /// <summary>Whether the button can be used for this row's item, such as Open for a folder that exists.</summary>
    public bool IsEnabled { get; }

    /// <summary>The button is only enabled while this is <see langword="true"/>, for every row, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }
}
