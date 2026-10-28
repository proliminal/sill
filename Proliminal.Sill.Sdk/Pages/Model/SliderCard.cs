using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a slider over a range of numbers.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class SliderCard : CardElement
{
    /// <summary>
    ///     Gets the number the slider is at. It only changes when the slider is released.
    /// </summary>
    public IValue<double> Value { get; }

    /// <summary>
    ///     Gets the lowest number.
    /// </summary>
    public double Minimum { get; }

    /// <summary>
    ///     Gets the highest number.
    /// </summary>
    public double Maximum { get; }

    /// <summary>
    ///     Gets the step size the slider snaps to, or 0 when it doesn't snap.
    /// </summary>
    public double Step { get; }

    /// <summary>
    ///     Gets the function that formats a number as the text next to the slider.
    /// </summary>
    public Func<double, string> Format { get; }

    internal SliderCard(CardInfo info, IValue<double> value, double minimum, double maximum, double step, Func<double, string> format)
        : base(info)
    {
        Value = value;
        Minimum = minimum;
        Maximum = maximum;
        Step = step;
        Format = format;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
