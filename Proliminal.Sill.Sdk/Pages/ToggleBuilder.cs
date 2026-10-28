using Proliminal.Sill.Sdk.Pages.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a card with an on/off switch.
///     See <see cref="CardsBuilder{TSelf}.Toggle"/>.
/// </summary>
public sealed class ToggleBuilder : CardBuilder<ToggleBuilder>
{
    internal ToggleBuilder(string title)
        : base(title)
    {
    }
}
