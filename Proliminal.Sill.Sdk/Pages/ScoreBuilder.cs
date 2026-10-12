using System.Globalization;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a score card: what its ring says, when it turns from red to yellow to green, and the recommendations that
///     raise it. See <see cref="CardsBuilder{TSelf}.Score"/>. Its <see cref="Grade"/> is shown in bold under its title,
///     followed by its <see cref="CardBuilder{TSelf}.Status"/>, and its description under that.
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
    ///     Sets the text in the middle of the ring. A percentage by default.
    /// </summary>
    /// <param name="format">The text for a score from 0 to 1.</param>
    public ScoreBuilder Format(Func<double, string> format)
    {
        _format = format ?? throw new ArgumentNullException(nameof(format));
        return this;
    }

    /// <summary>
    ///     Sets a word for the score, such as <c>Good</c>, shown in bold before the status, so it reads as the score's
    ///     grade rather than as part of a sentence.
    /// </summary>
    /// <param name="grade">The grade, or <see langword="null"/> or empty for none.</param>
    public ScoreBuilder Grade(IReadOnlyValue<string?> grade)
    {
        _grade = grade ?? throw new ArgumentNullException(nameof(grade));
        return this;
    }

    /// <summary>
    ///     Sets from which score the ring is green, and from which it's yellow rather than red. 0.6 and 0.3 by default.
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
    ///     Lists what would raise the score under the card, each with a button that applies it. The first few are shown,
    ///     the rest behind Show all. The list is hidden while it's empty.
    /// </summary>
    /// <param name="recommendations">The recommendations not applied yet, most important first.</param>
    public ScoreBuilder Recommendations(IReadOnlyValue<IReadOnlyList<ScoreRecommendation>> recommendations)
    {
        _recommendations = recommendations ?? throw new ArgumentNullException(nameof(recommendations));
        return this;
    }

    /// <summary>
    ///     Adds a prominent button right of the score, such as Apply all, shown while there are
    ///     <see cref="Recommendations"/>, or always without them.
    /// </summary>
    /// <param name="label">The button's text.</param>
    /// <param name="onClick">What the button does. The button is disabled until its task is done.</param>
    public ScoreBuilder ApplyAll(string label, PluginAction onClick)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);
        ArgumentNullException.ThrowIfNull(onClick);

        _applyAll = (label, onClick);
        return this;
    }

    /// <summary>
    ///     Adds a prominent button whose action is done right away. See <see cref="ApplyAll(string, PluginAction)"/>.
    /// </summary>
    /// <param name="label">The button's text.</param>
    /// <param name="onClick">What the button does.</param>
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
