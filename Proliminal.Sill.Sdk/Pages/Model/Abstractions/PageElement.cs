using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model.Abstractions;

/// <summary>
///     Something on a page.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class PageElement
{
    private protected PageElement(IReadOnlyValue<bool>? visibleWhen)
        => VisibleWhen = visibleWhen;

    /// <summary>The element is only shown while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    internal virtual IEnumerable<IReadOnlyValue> GetValues()
        => VisibleWhen is null ? [] : [VisibleWhen];
}
