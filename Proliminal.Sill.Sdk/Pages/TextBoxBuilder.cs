using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card with a box for one line of text. See
///     <see cref="CardsBuilder{TSelf}.TextBox(string, IValue{string}, Action{TextBoxBuilder}?)"/>.
/// </summary>
public sealed class TextBoxBuilder : TextCardBuilder<TextBoxBuilder>
{
    internal TextBoxBuilder(string title)
        : base(title)
    {
    }

    internal TextAreaCard Build(IValue<string> value)
        => Build(value, lines: 1);

    internal TextAreaCard Build(IReadOnlyValue<string> value)
        => Build(value, lines: 1);
}
