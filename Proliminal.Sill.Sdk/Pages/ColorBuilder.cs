using Proliminal.Sill.Sdk.Pages.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card with a color picker. See <see cref="CardsBuilder{TSelf}.Color"/>.
/// </summary>
public sealed class ColorBuilder : CardBuilder<ColorBuilder>
{
    internal ColorBuilder(string title)
        : base(title)
    {
    }
}
