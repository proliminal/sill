using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with a dropdown of options.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PickerCard : CardElement
{
    internal PickerCard(CardInfo info, IReadOnlyValue<IReadOnlyList<string>> options, IValue<int> selectedIndex)
        : base(info)
    {
        Options = options;
        SelectedIndex = selectedIndex;
    }

    /// <summary>The options' labels, in order. They can change, as when they're the fonts that are installed.</summary>
    public IReadOnlyValue<IReadOnlyList<string>> Options { get; }

    /// <summary>The index of the chosen option in <see cref="Options"/>, or -1 when the value matches none.</summary>
    public IValue<int> SelectedIndex { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Options).Append(SelectedIndex);
}
