using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages.Abstractions;

/// <summary>
///     Sets up a card with a box for text: what's shown while it's empty, how long the text can be, its font and when the
///     value changes. See <see cref="TextBoxBuilder"/> and <see cref="TextAreaBuilder"/>.
/// </summary>
/// <typeparam name="TSelf">The builder itself, so calls can be chained.</typeparam>
public abstract class TextCardBuilder<TSelf> : CardBuilder<TSelf>
    where TSelf : TextCardBuilder<TSelf>
{
    private string? _placeholder;
    private int _maxLength;
    private bool _isMonospace;
    private bool _changesWhileTyping;
    private bool _isReadOnly;

    private protected TextCardBuilder(string title)
        : base(title)
    {
    }

    /// <summary>
    ///     Shows <paramref name="placeholder"/> in the box while it's empty, such as an example of what goes in it.
    /// </summary>
    /// <param name="placeholder">A few words, or an example.</param>
    public TSelf Placeholder(string placeholder)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(placeholder);

        _placeholder = placeholder;
        return Self;
    }

    /// <summary>
    ///     Stops the text at <paramref name="maxLength"/> characters. By default there's no limit.
    /// </summary>
    /// <param name="maxLength">The most characters, at least 1.</param>
    public TSelf MaxLength(int maxLength)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, 1);

        _maxLength = maxLength;
        return Self;
    }

    /// <summary>
    ///     Shows the text in a monospaced font, for code, commands or lists where columns line up.
    /// </summary>
    public TSelf Monospace()
    {
        _isMonospace = true;
        return Self;
    }

    /// <summary>
    ///     Changes the value with each change while typing, instead of when the box loses focus, for text whose result
    ///     shows elsewhere on the page as it's typed, such as a conversion. Esc doesn't put anything back then.
    /// </summary>
    /// <remarks>
    ///     The value is set with every key, so setting it should be quick and shouldn't throw for text that's only half
    ///     typed: show what's wrong in a <see cref="CardBuilder{TSelf}.Status(IReadOnlyValue{string?})"/> instead, as a
    ///     thrown error is a notification.
    /// </remarks>
    public TSelf WhileTyping()
    {
        _changesWhileTyping = true;
        return Self;
    }

    /// <summary>
    ///     Only shows the text, to select and copy, such as a result the plugin keeps in a <see cref="State{T}"/>. The
    ///     value isn't changed from the box. A card given an <see cref="IReadOnlyValue{T}"/> is read-only already.
    /// </summary>
    public TSelf ReadOnly()
    {
        _isReadOnly = true;
        return Self;
    }

    private protected bool IsSecret { get; set; }

    private protected TextAreaCard Build(IValue<string> value, int lines)
        => new(Info, value, _placeholder, lines, _maxLength, _isMonospace, _changesWhileTyping, _isReadOnly, IsSecret);

    private protected TextAreaCard Build(IReadOnlyValue<string> value, int lines)
        => IsSecret
            ? throw new InvalidOperationException("A secret box needs a value it can change, not one it only shows.")
            : new(Info, new DisplayedValue<string>(value), _placeholder, lines, _maxLength, _isMonospace, changesWhileTyping: false, isReadOnly: true, isSecret: false);
}
