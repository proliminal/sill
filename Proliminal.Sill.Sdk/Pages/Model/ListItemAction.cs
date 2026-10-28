using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a small icon button on one row of a list, such as Remove.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ListItemAction
{
    /// <summary>
    ///     Gets the button's icon.
    /// </summary>
    public Glyph Icon { get; }

    /// <summary>
    ///     Gets what the button does, shown as its tooltip and read out by screen readers.
    /// </summary>
    public string Label { get; }

    /// <summary>
    ///     Gets the action the button runs for this row's item.
    /// </summary>
    public PluginAction OnClick { get; }

    /// <summary>
    ///     Gets whether the button can be used for this row's item, such as Open for a folder that exists.
    /// </summary>
    public bool IsEnabled { get; }

    /// <summary>
    ///     Gets the condition the button is enabled on for every row, or <see langword="null"/> when it's always enabled.
    /// </summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }

    internal ListItemAction(Glyph icon, string label, PluginAction onClick, bool isEnabled, IReadOnlyValue<bool>? enabledWhen)
    {
        Icon = icon;
        Label = label;
        OnClick = onClick;
        IsEnabled = isEnabled;
        EnabledWhen = enabledWhen;
    }
}
