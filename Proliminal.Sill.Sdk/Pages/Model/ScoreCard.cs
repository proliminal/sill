using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A large card with a score as a ring, its grade and description, and the recommendations that raise it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ScoreCard : CardElement
{
    internal ScoreCard(CardInfo info, IReadOnlyValue<double> value, Func<double, string> format, double good, double fair,
        IReadOnlyValue<IReadOnlyList<ScoreRecommendation>>? recommendations, string? applyAllLabel, PluginAction? applyAll)
        : base(info)
    {
        Value = value;
        Format = format;
        Good = good;
        Fair = fair;
        Recommendations = recommendations;
        ApplyAllLabel = applyAllLabel;
        ApplyAll = applyAll;
    }

    /// <summary>The score, from 0 to 1.</summary>
    public IReadOnlyValue<double> Value { get; }

    /// <summary>The text in the middle of the ring for a score.</summary>
    public Func<double, string> Format { get; }

    /// <summary>The lowest score that's green.</summary>
    public double Good { get; }

    /// <summary>The lowest score that's yellow; lower is red.</summary>
    public double Fair { get; }

    /// <summary>What would raise the score, if listed.</summary>
    public IReadOnlyValue<IReadOnlyList<ScoreRecommendation>>? Recommendations { get; }

    /// <summary>The prominent button's text, if it has one.</summary>
    public string? ApplyAllLabel { get; }

    /// <summary>What the prominent button does, if it has one.</summary>
    public PluginAction? ApplyAll { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
    {
        foreach (var value in base.GetValues())
            yield return value;

        yield return Value;

        if (Recommendations is not null)
            yield return Recommendations;
    }
}
