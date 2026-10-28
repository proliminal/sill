using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with an on/off switch.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ToggleCard : CardElement
{
    /// <summary>
    ///     Gets whether the switch is on.
    /// </summary>
    public IValue<bool> Value { get; }

    internal ToggleCard(CardInfo info, IValue<bool> value)
        : base(info)
        => Value = value;

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
