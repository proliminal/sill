using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     What happened, newest first and by day, with what's still going on above it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TimelineElement : PageElement
{
    internal TimelineElement(IReadOnlyValue<IReadOnlyList<TimelineItem>> items, string? empty, IReadOnlyValue<bool>? visibleWhen)
        : base(visibleWhen)
    {
        Items = items;
        Empty = empty;
    }

    /// <summary>The items, in any order. They're described again each time the list changes.</summary>
    public IReadOnlyValue<IReadOnlyList<TimelineItem>> Items { get; }

    /// <summary>What shows while there's nothing on it, if anything.</summary>
    public string? Empty { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Items);
}
