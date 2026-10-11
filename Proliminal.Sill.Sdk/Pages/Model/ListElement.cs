using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A compact card per item of a list that can change, such as the folders on <c>PATH</c>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ListElement : PageElement
{
    internal ListElement(IReadOnlyValue<IReadOnlyList<ListItem>> items, IReadOnlyList<IReadOnlyValue<bool>> conditions, IReadOnlyValue<bool>? visibleWhen)
        : base(visibleWhen)
    {
        Items = items;
        Conditions = conditions;
    }

    /// <summary>The rows, from top to bottom. They're described again each time the list changes.</summary>
    public IReadOnlyValue<IReadOnlyList<ListItem>> Items { get; }

    /// <summary>The conditions the rows' buttons are enabled on, shared by every row.</summary>
    public IReadOnlyList<IReadOnlyValue<bool>> Conditions { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Items).Concat(Conditions);
}
