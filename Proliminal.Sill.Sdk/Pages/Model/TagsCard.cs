using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a dropdown that picks any number of tags, which are shown under its description too.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TagsCard : CardElement
{
    /// <summary>
    ///     Gets the tags that can be picked, in order. They can change, such as when they're read from a web service.
    /// </summary>
    public IReadOnlyValue<IReadOnlyList<TagOption>> Options { get; }

    /// <summary>
    ///     Gets the indices in <see cref="Options"/> of the picked tags, in the order they were picked.
    /// </summary>
    public IValue<IReadOnlyList<int>> SelectedIndices { get; }

    /// <summary>
    ///     Gets the text in the list's search box while it's empty.
    /// </summary>
    public string Placeholder { get; }

    internal TagsCard(CardInfo info, IReadOnlyValue<IReadOnlyList<TagOption>> options, IValue<IReadOnlyList<int>> selectedIndices, string placeholder)
        : base(info)
    {
        Options = options;
        SelectedIndices = selectedIndices;
        Placeholder = placeholder;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Options).Append(SelectedIndices);
}
