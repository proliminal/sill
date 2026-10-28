using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a large card with a score shown as a ring, its grade and description, and the recommendations that raise it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ScoreCard : CardElement
{
    /// <summary>
    ///     Gets the score, from 0 to 1.
    /// </summary>
    public IReadOnlyValue<double> Value { get; }

    /// <summary>
    ///     Gets the function that formats a score as the text in the middle of the ring.
    /// </summary>
    public Func<double, string> Format { get; }

    /// <summary>
    ///     Gets the lowest score that's shown as green.
    /// </summary>
    public double GoodThreshold { get; }

    /// <summary>
    ///     Gets the lowest score that's shown as yellow. Lower scores are red.
    /// </summary>
    public double FairThreshold { get; }

    /// <summary>
    ///     Gets the recommendations that would raise the score, if any are listed.
    /// </summary>
    public IReadOnlyValue<IReadOnlyList<ScoreRecommendation>>? Recommendations { get; }

    /// <summary>
    ///     Gets a word for the score, such as <c>Good</c>, shown in bold before the status, if set.
    /// </summary>
    public IReadOnlyValue<string?>? Grade { get; }

    /// <summary>
    ///     Gets the text of the prominent button, if there is one.
    /// </summary>
    public string? ApplyAllLabel { get; }

    /// <summary>
    ///     Gets the action the prominent button runs, if there is one.
    /// </summary>
    public PluginAction? ApplyAll { get; }

    internal ScoreCard(CardInfo info, IReadOnlyValue<double> value, Func<double, string> format, double good, double fair,
        IReadOnlyValue<IReadOnlyList<ScoreRecommendation>>? recommendations, string? applyAllLabel, PluginAction? applyAll,
        IReadOnlyValue<string?>? grade = null)
        : base(info)
    {
        Value = value;
        Format = format;
        GoodThreshold = good;
        FairThreshold = fair;
        Recommendations = recommendations;
        ApplyAllLabel = applyAllLabel;
        ApplyAll = applyAll;
        Grade = grade;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
    {
        foreach (var value in base.GetValues())
            yield return value;

        yield return Value;

        if (Recommendations is not null)
            yield return Recommendations;

        if (Grade is not null)
            yield return Grade;
    }
}
