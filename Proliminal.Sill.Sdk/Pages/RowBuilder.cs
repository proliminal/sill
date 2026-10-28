using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for cards side by side, from left to right.
///     See <see cref="PopupBuilder.Row"/>.
/// </summary>
public sealed class RowBuilder : CardsBuilder<RowBuilder>
{
    private readonly List<PageElement> _elements = [];

    internal RowBuilder()
    {
    }

    private protected override void Add(PageElement element)
        => _elements.Add(element);

    internal RowElement Build()
        => _elements.Count >= 2
            ? new RowElement(_elements.ToArray())
            : throw new InvalidOperationException("A row holds two cards or more; put a single card in the popup itself.");
}
