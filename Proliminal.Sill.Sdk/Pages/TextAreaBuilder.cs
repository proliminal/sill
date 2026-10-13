using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card with a box for text of several lines. See <see cref="CardsBuilder{TSelf}.TextArea"/>.
/// </summary>
public sealed class TextAreaBuilder : CardBuilder<TextAreaBuilder>
{
    private string? _placeholder;
    private int _lines = 4;
    private int _maxLength;
    private bool _isMonospace;

    internal TextAreaBuilder(string title)
        : base(title)
    {
    }

    /// <summary>
    ///     Shows <paramref name="placeholder"/> in the box while it's empty, such as an example of what goes in it.
    /// </summary>
    /// <param name="placeholder">A few words, or an example.</param>
    public TextAreaBuilder Placeholder(string placeholder)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(placeholder);

        _placeholder = placeholder;
        return this;
    }

    /// <summary>
    ///     Makes the box <paramref name="lines"/> lines high; longer text scrolls. By default it's 4.
    /// </summary>
    /// <param name="lines">From 2 to 20.</param>
    public TextAreaBuilder Lines(int lines)
    {
        if (lines is < 2 or > 20)
            throw new ArgumentOutOfRangeException(nameof(lines), lines, "A text area is 2 to 20 lines high.");

        _lines = lines;
        return this;
    }

    /// <summary>
    ///     Stops the text at <paramref name="maxLength"/> characters. By default there's no limit.
    /// </summary>
    /// <param name="maxLength">The most characters, at least 1.</param>
    public TextAreaBuilder MaxLength(int maxLength)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, 1);

        _maxLength = maxLength;
        return this;
    }

    /// <summary>
    ///     Shows the text in a monospaced font, for code, commands or lists where columns line up.
    /// </summary>
    public TextAreaBuilder Monospace()
    {
        _isMonospace = true;
        return this;
    }

    internal TextAreaCard Build(IValue<string> value)
        => new(Info, value, _placeholder, _lines, _maxLength, _isMonospace);
}
