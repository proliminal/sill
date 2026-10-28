using System.Globalization;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a score card: the text in its ring, the thresholds at which it turns from red to yellow to green, and the recommendations that raise it.
///     The <see cref="Grade"/> is shown in bold under the title, followed by the <see cref="CardBuilder{TSelf}.Status(IReadOnlyValue{string?})"/>, with the description below.
///     See <see cref="CardsBuilder{TSelf}.Score"/>.
/// </summary>
public sealed class ScoreBuilder : CardBuilder<ScoreBuilder>
{
    private Func<double, string> _format = score => score.ToString("P0", CultureInfo.CurrentCulture);
    private double _good = 0.6;
    private double _fair = 0.3;
    private IReadOnlyValue<IReadOnlyList<ScoreRecommendation>>? _recommendations;
    private (string Label, PluginAction OnClick)? _applyAll;
    private IReadOnlyValue<string?>? _grade;

    internal ScoreBuilder(string title)
        : base(title)
    {
    }

    /// <summary>
    ///     Sets the text in the middle of the ring. By default, it's a percentage.
    /// </summary>
    /// <param name="format">Gets the text for a score from 0 to 1.</param>
    public ScoreBuilder Format(Func<double, string> format)
    {
        _format = format ?? throw new ArgumentNullException(nameof(format));
        return this;
    }

    /// <summary>
    ///     Sets a word for the score, such as <c>Good</c>, shown in bold before the status so it reads as a grade rather than as part of a sentence.
    /// </summary>
    /// <param name="grade">The grade, or <see langword="null"/> or empty for none.</param>
    public ScoreBuilder Grade(IReadOnlyValue<string?> grade)
    {
        _grade = grade ?? throw new ArgumentNullException(nameof(grade));
        return this;
    }

    /// <summary>
    ///     Sets the lowest score at which the ring is green, and the lowest at which it's yellow rather than red. By default, these are 0.6 and 0.3.
    /// </summary>
    /// <param name="good">The lowest score that's green.</param>
    /// <param name="fair">The lowest score that's yellow.</param>
    public ScoreBuilder Thresholds(double good, double fair)
    {
        if (fair is < 0 or > 1 || good is < 0 or > 1 || fair > good)
            throw new ArgumentOutOfRangeException(nameof(fair), "Use scores from 0 to 1, with fair at most good.");

        _good = good;
        _fair = fair;
        return this;
    }

    /// <summary>
    ///     Lists the recommendations that would raise the score under the card, each with a button that applies it.
    ///     The first few are shown, and the rest are behind Show all. The list is hidden while it's empty.
    /// </summary>
    /// <param name="recommendations">The recommendations that aren't applied yet, most important first.</param>
    public ScoreBuilder Recommendations(IReadOnlyValue<IReadOnlyList<ScoreRecommendation>> recommendations)
    {
        _recommendations = recommendations ?? throw new ArgumentNullException(nameof(recommendations));
        return this;
    }

    /// <summary>
    ///     Adds a prominent button right of the score, such as Apply all.
    ///     It's shown while there are <see cref="Recommendations"/>, or always when none are set.
    /// </summary>
    /// <param name="label">The button text.</param>
    /// <param name="onClick">What the button does. The button is disabled until the returned task completes.</param>
    public ScoreBuilder ApplyAll(string label, PluginAction onClick)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);
        ArgumentNullException.ThrowIfNull(onClick);

        _applyAll = (label, onClick);
        return this;
    }

    /// <inheritdoc cref="ApplyAll(string, PluginAction)"/>
    public ScoreBuilder ApplyAll(string label, Action<IActionContext> onClick)
    {
        ArgumentNullException.ThrowIfNull(onClick);

        return ApplyAll(label, context =>
        {
            onClick(context);
            return Task.CompletedTask;
        });
    }

    internal ScoreCard Build(IReadOnlyValue<double> value)
        => new(Info, value, _format, _good, _fair, _recommendations, _applyAll?.Label, _applyAll?.OnClick, _grade);
}
