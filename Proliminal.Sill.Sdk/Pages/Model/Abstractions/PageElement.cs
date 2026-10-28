using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model.Abstractions;

/// <summary>
///     Represents an element on a page.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class PageElement
{
    /// <summary>
    ///     Gets the condition the element is shown on, or <see langword="null"/> when it's always shown.
    /// </summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    private protected PageElement(IReadOnlyValue<bool>? visibleWhen)
        => VisibleWhen = visibleWhen;

    internal virtual IEnumerable<IReadOnlyValue> GetValues()
        => VisibleWhen is null ? [] : [VisibleWhen];
}
