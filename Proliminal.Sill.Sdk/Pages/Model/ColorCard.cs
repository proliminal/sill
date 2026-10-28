using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a color picker, which offers dark presets, a typed-in color and "Default".
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ColorCard : CardElement
{
    /// <summary>
    ///     Gets the color as <c>#RRGGBB</c>, or <see langword="null"/> for the default.
    /// </summary>
    public IValue<string?> Value { get; }

    internal ColorCard(CardInfo info, IValue<string?> value)
        : base(info)
        => Value = value;

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
