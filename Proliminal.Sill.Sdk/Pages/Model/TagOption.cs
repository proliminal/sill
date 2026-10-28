using System.ComponentModel;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a tag that can be picked on a <see cref="TagsCard"/>.
/// </summary>
/// <param name="Label">The text shown for the tag.</param>
/// <param name="Color">The tag's color as <c>#RRGGBB</c>, or <see langword="null"/> for the accent color.</param>
/// <param name="Group">The header the tag is listed under, or <see langword="null"/> for none.</param>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed record TagOption(string Label, string? Color, string? Group);
