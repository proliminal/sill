using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with a slider over a range of numbers.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class SliderCard : CardElement
{
    internal SliderCard(CardInfo info, IValue<double> value, double minimum, double maximum, double step, Func<double, string> format)
        : base(info)
    {
        Value = value;
        Minimum = minimum;
        Maximum = maximum;
        Step = step;
        Format = format;
    }

    /// <summary>The number the slider is at. It's only changed when the slider is let go.</summary>
    public IValue<double> Value { get; }

    /// <summary>The lowest number.</summary>
    public double Minimum { get; }

    /// <summary>The highest number.</summary>
    public double Maximum { get; }

    /// <summary>The steps the slider snaps to, or 0 when it doesn't.</summary>
    public double Step { get; }

    /// <summary>Shows a number as text next to the slider.</summary>
    public Func<double, string> Format { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
