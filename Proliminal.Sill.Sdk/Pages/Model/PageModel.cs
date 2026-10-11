using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

// What a PageBuilder describes, for Sill to draw. Plugins never create these; they only see them through the builders.

/// <summary>
///     A described page: its elements from top to bottom.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PageModel
{
    internal PageModel(IReadOnlyList<PageElement> elements)
        => Elements = elements;

    /// <summary>The page's elements, from top to bottom.</summary>
    public IReadOnlyList<PageElement> Elements { get; }

    /// <summary>
    ///     Every value on the page, including the conditions cards are shown and enabled on, to refresh them together.
    /// </summary>
    public IEnumerable<IReadOnlyValue> GetValues()
    {
        foreach (var element in Elements)
        {
            foreach (var value in element.GetValues())
                yield return value;
        }
    }
}
