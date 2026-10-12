namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     A line inside a card, under its description, such as one thing a plugin uses. See
///     <see cref="Abstractions.CardBuilder{TSelf}.Details"/>.
/// </summary>
/// <param name="Title">What it is, in a few words.</param>
/// <param name="Text">More about it under the title, if anything.</param>
/// <param name="Icon">An icon left of it, if any; lines with none line up with those that have one.</param>
public sealed record CardDetail(string Title, string? Text = null, Glyph? Icon = null);
