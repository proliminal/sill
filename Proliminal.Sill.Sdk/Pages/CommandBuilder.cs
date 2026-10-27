using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a command in a popup's footer: the key that presses it, whether it closes the popup, and when it's shown
///     and enabled. See <see cref="PopupBuilder.Command(string, PluginAction, Action{CommandBuilder}?)"/>.
/// </summary>
public sealed class CommandBuilder
{
    // Ctrl with these edits text in the popup's boxes, as anywhere else.
    private const string RESERVED_KEYS = "ACVXYZ";

    private bool _isDefault;
    private char? _key;
    private bool _closes;
    private IReadOnlyValue<bool>? _visibleWhen;
    private IReadOnlyValue<bool>? _enabledWhen;

    internal CommandBuilder()
    {
    }

    /// <summary>
    ///     Lets Enter press it, and shows it as the popup's main button. When more than one command is the default, Enter
    ///     presses the first that's shown and enabled. In a box of several lines, Ctrl+Enter does.
    /// </summary>
    public CommandBuilder Default()
    {
        _isDefault = true;
        return this;
    }

    /// <summary>
    ///     Lets Ctrl and <paramref name="letter"/> press it, such as Ctrl+D for Discard. When more than one command has the
    ///     same letter, it presses the first that's shown and enabled.
    /// </summary>
    /// <param name="letter">A letter from <c>A</c> to <c>Z</c>, except A, C, V, X, Y and Z, which edit text.</param>
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
    ///     Closes the popup once the command is done, unless it threw or the user pinned the popup, which keeps it open.
    /// </summary>
    public CommandBuilder Closes()
    {
        _closes = true;
        return this;
    }

    /// <summary>
    ///     Only shows the button, and only lets its key press it, while <paramref name="condition"/> is
    ///     <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the button is shown.</param>
    public CommandBuilder VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    /// <summary>
    ///     Only enables the button, and only lets its key press it, while <paramref name="condition"/> is
    ///     <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the button is enabled.</param>
    public CommandBuilder EnabledWhen(IReadOnlyValue<bool> condition)
    {
        _enabledWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    internal PopupCommand Build(string label, PluginAction run)
        => new(label, run, _isDefault, _key, _closes, _visibleWhen, _enabledWhen);
}
