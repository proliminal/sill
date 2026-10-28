using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents an extra button on a card, left of its control, such as Install next to a switch for something that's missing.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class CardAction
{
    /// <summary>
    ///     Gets the button's text.
    /// </summary>
    public string Label { get; }

    /// <summary>
    ///     Gets the action the button runs.
    /// </summary>
    public PluginAction OnClick { get; }

    /// <summary>
    ///     Gets the condition the button is shown on, or <see langword="null"/> when it's always shown.
    /// </summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    /// <summary>
    ///     Gets the condition the button is enabled on, or <see langword="null"/> when it's always enabled.
    /// </summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }

    internal CardAction(string label, PluginAction onClick, IReadOnlyValue<bool>? visibleWhen, IReadOnlyValue<bool>? enabledWhen)
    {
        Label = label;
        OnClick = onClick;
        VisibleWhen = visibleWhen;
        EnabledWhen = enabledWhen;
    }
}
