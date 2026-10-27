using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A button in a popup's footer, and the key that presses it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PopupCommand
{
    internal PopupCommand(string label, PluginAction run, bool isDefault, char? key, bool closes, IReadOnlyValue<bool>? visibleWhen, IReadOnlyValue<bool>? enabledWhen)
    {
        Label = label;
        Run = run;
        IsDefault = isDefault;
        Key = key;
        Closes = closes;
        VisibleWhen = visibleWhen;
        EnabledWhen = enabledWhen;
    }

    /// <summary>The button's text.</summary>
    public string Label { get; }

    /// <summary>What the button does.</summary>
    public PluginAction Run { get; }

    /// <summary>Whether Enter presses it, when it's the first such command that's shown and enabled.</summary>
    public bool IsDefault { get; }

    /// <summary>The letter that presses it with Ctrl, from <c>A</c> to <c>Z</c>, if any.</summary>
    public char? Key { get; }

    /// <summary>Whether the popup closes once it's done without an error.</summary>
    public bool Closes { get; }

    /// <summary>The button is only shown while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    /// <summary>The button is only enabled while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }
}
