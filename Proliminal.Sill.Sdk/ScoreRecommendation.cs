using Proliminal.Sill.Sdk.Pages;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     Represents something that raises a score, shown under a score card with a button that applies it, as Windows Settings shows
///     energy recommendations. See <see cref="ScoreBuilder.Recommendations"/>.
/// </summary>
/// <param name="Title">What it is, such as <c>Personalized ads</c>.</param>
/// <param name="ActionLabel">The text of the button that applies it, such as <c>Turn off</c>.</param>
/// <param name="Apply">What the button does. The button is disabled until its task is done.</param>
/// <param name="Description">Text under the title, such as the page it's on, if any.</param>
/// <param name="Icon">The icon left of the title, if any.</param>
public sealed record ScoreRecommendation(string Title, string ActionLabel, PluginAction Apply, string? Description = null, Glyph? Icon = null);
