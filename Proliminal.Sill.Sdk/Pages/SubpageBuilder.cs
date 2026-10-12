using Proliminal.Sill.Sdk.Pages.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card that opens a subpage. See <see cref="CardsBuilder{TSelf}.Subpage"/>.
/// </summary>
public sealed class SubpageBuilder : CardBuilder<SubpageBuilder>
{
    internal SubpageBuilder(string title)
        : base(title)
    {
    }
}
