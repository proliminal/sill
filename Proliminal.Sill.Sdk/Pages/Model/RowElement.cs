using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents cards side by side in a popup, each of equal width.
///     A hidden card gives its width to the others.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class RowElement : PageElement
{
    /// <summary>
    ///     Gets the cards, from left to right.
    /// </summary>
    public IReadOnlyList<PageElement> Elements { get; }

    internal RowElement(IReadOnlyList<PageElement> elements)
        : base(null)
        => Elements = elements;

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Concat(Elements.SelectMany(element => element.GetValues()));
}
