using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Values;

namespace Proliminal.Sill.Sdk.Pages.Abstractions;

/// <summary>
///     Represents a builder for a card with a text box: what's shown while it's empty, how long the text can be, its font and when the value changes.
///     See <see cref="TextBoxBuilder"/> and <see cref="TextAreaBuilder"/>.
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
    ///     Sets the text shown in the box while it's empty, such as an example of what goes in it.
    /// </summary>
    /// <param name="placeholder">A few words, or an example.</param>
    public TSelf Placeholder(string placeholder)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(placeholder);

        _placeholder = placeholder;
        return Self;
    }

    /// <summary>
    ///     Sets the maximum number of characters. By default there's no limit.
    /// </summary>
    /// <param name="maxLength">The maximum number of characters, at least 1.</param>
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
    ///     Sets the value on every change while typing, instead of when the box loses focus, for text whose result shows elsewhere on the page as it's typed, such as a conversion.
    ///     Esc no longer restores the previous text.
    /// </summary>
    /// <remarks>
    ///     The value is set on every key press, so setting it should be quick and shouldn't throw for text that's only partly typed.
    ///     Show what's wrong with <see cref="CardBuilder{TSelf}.Status(IReadOnlyValue{string?})"/> instead, as a thrown error becomes a notification.
    /// </remarks>
    public TSelf ChangesWhileTyping()
    {
        _changesWhileTyping = true;
        return Self;
    }

    /// <summary>
    ///     Shows the text only, to select and copy, such as a result the plugin keeps in a <see cref="State{T}"/>. The value isn't changed from the box.
    ///     A card given an <see cref="IReadOnlyValue{T}"/> is already read-only.
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
