using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a dropdown of options.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PickerCard : CardElement
{
    /// <summary>
    ///     Gets the options' labels, in order. They can change, such as when they're the installed fonts.
    /// </summary>
    public IReadOnlyValue<IReadOnlyList<string>> Options { get; }

    /// <summary>
    ///     Gets the index of the selected option in <see cref="Options"/>, or -1 when the value matches none.
    /// </summary>
    public IValue<int> SelectedIndex { get; }

    internal PickerCard(CardInfo info, IReadOnlyValue<IReadOnlyList<string>> options, IValue<int> selectedIndex)
        : base(info)
    {
        Options = options;
        SelectedIndex = selectedIndex;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Options).Append(SelectedIndex);
}
