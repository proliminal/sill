using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a command in a popup's footer: its shortcut key, whether it closes the popup, and when it's shown and enabled.
///     See <see cref="PopupBuilder.Command(string, PluginAction, Action{CommandBuilder}?)"/>.
/// </summary>
public sealed class CommandBuilder
{
    private const string RESERVED_KEYS = "ACVXYZ";

    private bool _isDefault;
    private char? _key;
    private bool _closesPopup;
    private IReadOnlyValue<bool>? _visibleWhen;
    private IReadOnlyValue<bool>? _enabledWhen;

    internal CommandBuilder()
    {
    }

    /// <summary>
    ///     Makes the command the popup's main button, which Enter presses.
    ///     When more than one command is the default, Enter presses the first one that's shown and enabled. In a multi-line box, Ctrl+Enter presses it instead.
    /// </summary>
    public CommandBuilder IsDefault()
    {
        _isDefault = true;
        return this;
    }

    /// <summary>
    ///     Lets Ctrl and <paramref name="letter"/> press the command, such as Ctrl+D for Discard.
    ///     When more than one command has the same letter, the shortcut presses the first one that's shown and enabled.
    /// </summary>
    /// <param name="letter">A letter from <c>A</c> to <c>Z</c>, except A, C, V, X, Y and Z, which are used for editing text.</param>
    public CommandBuilder Key(char letter)
    {
        letter = char.ToUpperInvariant(letter);

        if (letter is < 'A' or > 'Z')
            throw new ArgumentException("Use a letter from A to Z.", nameof(letter));

        if (RESERVED_KEYS.Contains(letter))
            throw new ArgumentException($"Ctrl+{letter} edits text in the popup's boxes; use another letter.", nameof(letter));

        _key = letter;
        return this;
    }

    /// <summary>
    ///     Closes the popup once the command is done, unless the command threw or the user pinned the popup.
    /// </summary>
    public CommandBuilder ClosesPopup()
    {
        _closesPopup = true;
        return this;
    }

    /// <summary>
    ///     Shows the button, and lets its key press it, only while <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the button is shown.</param>
    public CommandBuilder VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    /// <summary>
    ///     Enables the button, and lets its key press it, only while <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the button is enabled.</param>
    public CommandBuilder EnabledWhen(IReadOnlyValue<bool> condition)
    {
        _enabledWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    internal PopupCommand Build(string label, PluginAction onClick)
        => new(label, onClick, _isDefault, _key, _closesPopup, _visibleWhen, _enabledWhen);
}
