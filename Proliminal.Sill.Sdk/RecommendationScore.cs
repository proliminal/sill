using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     Represents the score of Sill's that a recommendation counts toward. See <see cref="IPluginContext.AddRecommendation"/>.
/// </summary>
public enum RecommendationScore
{
    /// <summary>
    ///     The Privacy score: what the account shares, and with whom.
    /// </summary>
    Privacy,

    /// <summary>
    ///     The Security score: how well the account and the PC are protected.
    /// </summary>
    Security,
}
