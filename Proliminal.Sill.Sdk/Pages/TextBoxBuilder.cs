using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a card with a single-line text box.
///     See <see cref="CardsBuilder{TSelf}.TextBox(string, IValue{string}, Action{TextBoxBuilder}?)"/>.
/// </summary>
public sealed class TextBoxBuilder : TextCardBuilder<TextBoxBuilder>
{
    internal TextBoxBuilder(string title)
        : base(title)
    {
    }

    /// <summary>
    ///     Masks the text and prevents copying it, for a password or a token.
    ///     Bind it to a value from <see cref="IPluginContext.GetSecret"/>, which keeps it out of <c>settings.json</c>.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .TextBox("API token", context.GetSecret("ApiToken"), text => text
    ///         .Secret()
    ///         .Placeholder("Paste your token"))
    ///     </code>
    /// </example>
    public TextBoxBuilder Secret()
    {
        IsSecret = true;
        return this;
    }

    internal TextAreaCard Build(IValue<string> value)
        => Build(value, lines: 1);

    internal TextAreaCard Build(IReadOnlyValue<string> value)
        => Build(value, lines: 1);
}
