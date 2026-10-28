namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a line inside a card, under its description, such as one capability a plugin uses.
///     See <see cref="Abstractions.CardBuilder{TSelf}.Lines"/>.
/// </summary>
/// <param name="Title">What the line is about, in a few words.</param>
/// <param name="Text">More information under the title, if any.</param>
/// <param name="Icon">The icon left of the line, if any. Lines without an icon are aligned with those that have one.</param>
public sealed record CardLine(string Title, string? Text = null, Glyph? Icon = null);
