using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with an on/off switch.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ToggleCard : CardElement
{
    internal ToggleCard(CardInfo info, IValue<bool> value)
        : base(info)
        => Value = value;

    /// <summary>Whether it's on.</summary>
    public IValue<bool> Value { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
