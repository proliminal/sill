using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a card with a multi-line text box.
///     See <see cref="CardsBuilder{TSelf}.TextArea(string, IValue{string}, Action{TextAreaBuilder}?)"/>.
/// </summary>
public sealed class TextAreaBuilder : TextCardBuilder<TextAreaBuilder>
{
    private int _lines = 4;

    internal TextAreaBuilder(string title)
        : base(title)
    {
    }

    /// <summary>
    ///     Sets the height of the box to <paramref name="lines"/> lines; longer text scrolls. By default, it's 4.
    /// </summary>
    /// <param name="lines">The number of lines, from 2 to 20.</param>
    public TextAreaBuilder Lines(int lines)
    {
        if (lines is < 2 or > 20)
            throw new ArgumentOutOfRangeException(nameof(lines), lines, "A text area is 2 to 20 lines high.");

        _lines = lines;
        return this;
    }

    internal TextAreaCard Build(IValue<string> value)
        => Build(value, _lines);

    internal TextAreaCard Build(IReadOnlyValue<string> value)
        => Build(value, _lines);
}
