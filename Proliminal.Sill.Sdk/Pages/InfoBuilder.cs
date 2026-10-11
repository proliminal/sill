using Proliminal.Sill.Sdk.Pages.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card without a control of its own. See <see cref="CardsBuilder{TSelf}.Info"/>.
/// </summary>
public sealed class InfoBuilder : CardBuilder<InfoBuilder>
{
    internal InfoBuilder(string title)
        : base(title)
    {
    }
}
