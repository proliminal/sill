using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a button in a popup's footer, and the key that presses it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PopupCommand
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
    ///     Gets whether Enter presses the button, when it's the first such command that's shown and enabled.
    /// </summary>
    public bool IsDefault { get; }

    /// <summary>
    ///     Gets the letter that presses the button with Ctrl, from <c>A</c> to <c>Z</c>, if any.
    /// </summary>
    public char? Key { get; }

    /// <summary>
    ///     Gets whether the popup closes once the action completes without an error.
    /// </summary>
    public bool ClosesPopup { get; }

    /// <summary>
    ///     Gets the condition the button is shown on, or <see langword="null"/> when it's always shown.
    /// </summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    /// <summary>
    ///     Gets the condition the button is enabled on, or <see langword="null"/> when it's always enabled.
    /// </summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }

    internal PopupCommand(string label, PluginAction onClick, bool isDefault, char? key, bool closesPopup, IReadOnlyValue<bool>? visibleWhen, IReadOnlyValue<bool>? enabledWhen)
    {
        Label = label;
        OnClick = onClick;
        IsDefault = isDefault;
        Key = key;
        ClosesPopup = closesPopup;
        VisibleWhen = visibleWhen;
        EnabledWhen = enabledWhen;
    }
}
