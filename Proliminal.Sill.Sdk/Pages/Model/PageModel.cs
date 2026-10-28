using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a described page and its elements, from top to bottom.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PageModel
{
    /// <summary>
    ///     Gets the page's elements, from top to bottom.
    /// </summary>
    public IReadOnlyList<PageElement> Elements { get; }

    /// <summary>
    ///     Gets other words people search for the page by, besides its title and content.
    /// </summary>
    public IReadOnlyList<string> Keywords { get; }

    internal PageModel(IReadOnlyList<PageElement> elements, IReadOnlyList<string> keywords)
    {
        Elements = elements;
        Keywords = keywords;
    }

    /// <summary>
    ///     Gets every value on the page, including the conditions cards are shown and enabled on, so they can be refreshed together.
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
