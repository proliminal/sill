using Proliminal.Sill.Sdk.Pages.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card with a keyboard shortcut. See <see cref="CardsBuilder{TSelf}.Shortcut"/>.
/// </summary>
internal sealed class ShortcutBuilder : CardBuilder<ShortcutBuilder>
{
    internal ShortcutBuilder(string title)
        : base(title)
    {
    }
}
