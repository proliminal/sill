using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a list that can change, shown as a compact card per item, such as the folders on <c>PATH</c>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ListElement : PageElement
{
    /// <summary>
    ///     Gets the rows, from top to bottom. They're rebuilt each time the list changes.
    /// </summary>
    public IReadOnlyValue<IReadOnlyList<ListItem>> Items { get; }

    /// <summary>
    ///     Gets the conditions the rows' buttons are enabled on, shared by every row.
    /// </summary>
    public IReadOnlyList<IReadOnlyValue<bool>> ActionConditions { get; }

    internal ListElement(IReadOnlyValue<IReadOnlyList<ListItem>> items, IReadOnlyList<IReadOnlyValue<bool>> actionConditions, IReadOnlyValue<bool>? visibleWhen)
        : base(visibleWhen)
    {
        Items = items;
        ActionConditions = actionConditions;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Items).Concat(ActionConditions);
}
