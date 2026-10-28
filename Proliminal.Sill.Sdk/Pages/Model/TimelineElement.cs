using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a timeline of events, newest first and grouped by day, with ongoing events above it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TimelineElement : PageElement
{
    /// <summary>
    ///     Gets the items, in any order. They're rebuilt each time the list changes.
    /// </summary>
    public IReadOnlyValue<IReadOnlyList<TimelineItem>> Items { get; }

    /// <summary>
    ///     Gets the text shown while the timeline is empty, if any.
    /// </summary>
    public string? EmptyText { get; }

    internal TimelineElement(IReadOnlyValue<IReadOnlyList<TimelineItem>> items, string? emptyText, IReadOnlyValue<bool>? visibleWhen)
        : base(visibleWhen)
    {
        Items = items;
        EmptyText = emptyText;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Items);
}
