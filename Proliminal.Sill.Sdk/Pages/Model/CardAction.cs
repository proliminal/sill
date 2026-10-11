using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     An extra button on a card, left of its control, such as Install next to a switch for something that's missing.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class CardAction
{
    internal CardAction(string label, PluginAction onClick, IReadOnlyValue<bool>? visibleWhen, IReadOnlyValue<bool>? enabledWhen)
    {
        Label = label;
        OnClick = onClick;
        VisibleWhen = visibleWhen;
        EnabledWhen = enabledWhen;
    }

    /// <summary>The button's text.</summary>
    public string Label { get; }

    /// <summary>What the button does.</summary>
    public PluginAction OnClick { get; }

    /// <summary>The button is only shown while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    /// <summary>The button is only enabled while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }
}
