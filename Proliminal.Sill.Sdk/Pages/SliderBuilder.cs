using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a card with a slider.
///     See <see cref="CardsBuilder{TSelf}.Slider(string, IValue{double}, double, double, Action{SliderBuilder}?)"/>.
/// </summary>
public sealed class SliderBuilder : CardBuilder<SliderBuilder>
{
    private readonly double _minimum;
    private readonly double _maximum;
    private double _step;
    private Func<double, string> _format;

    internal SliderBuilder(string title, double minimum, double maximum, double step, Func<double, string> format)
        : base(title)
    {
        if (!double.IsFinite(minimum) || !double.IsFinite(maximum) || minimum >= maximum)
            throw new ArgumentException($"The slider \"{title}\" needs a minimum below its maximum; it has {minimum} to {maximum}.");

        _minimum = minimum;
        _maximum = maximum;
        _step = step;
        _format = format;
    }

    /// <summary>
    ///     Snaps the slider to multiples of <paramref name="step"/> from the minimum. A step of 0 lets it move freely.
    /// </summary>
    /// <param name="step">The distance between the values the slider can take.</param>
    public SliderBuilder Step(double step)
    {
        if (!double.IsFinite(step) || step < 0 || step > _maximum - _minimum)
            throw new ArgumentOutOfRangeException(nameof(step), step, "The step must be 0, or at most the slider's range.");

        _step = step;
        return this;
    }

    /// <summary>
    ///     Sets how the number next to the slider is shown, such as <c>50%</c>. By default, it's shown with up to two decimals.
    /// </summary>
    /// <param name="format">Converts a number to text.</param>
    public SliderBuilder Format(Func<double, string> format)
    {
        _format = format ?? throw new ArgumentNullException(nameof(format));
        return this;
    }

    internal SliderCard Build(IValue<double> value)
        => new(Info, value, _minimum, _maximum, _step, _format);
}
