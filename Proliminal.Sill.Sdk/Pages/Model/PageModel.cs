using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A described page: its elements from top to bottom.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PageModel
{
    internal PageModel(IReadOnlyList<PageElement> elements, IReadOnlyList<string> keywords)
    {
        Elements = elements;
        Keywords = keywords;
    }

    /// <summary>The page's elements, from top to bottom.</summary>
    public IReadOnlyList<PageElement> Elements { get; }

    /// <summary>Other words people search for the page by, besides its title and what's on it.</summary>
    public IReadOnlyList<string> Keywords { get; }

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
