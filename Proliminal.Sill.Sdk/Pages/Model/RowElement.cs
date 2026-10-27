using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Cards side by side, each as wide as the others, in a popup. A card that's hidden leaves its width to the others.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class RowElement : PageElement
{
    internal RowElement(IReadOnlyList<PageElement> elements)
        : base(null)
        => Elements = elements;

    /// <summary>The cards, from left to right.</summary>
    public IReadOnlyList<PageElement> Elements { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Concat(Elements.SelectMany(element => element.GetValues()));
}
