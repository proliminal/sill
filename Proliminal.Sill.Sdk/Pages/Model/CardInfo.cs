using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     What every card has, as collected by a card's builder.
/// </summary>
internal sealed record CardInfo(string Title, IReadOnlyValue<string>? Description, Glyph? Icon, IReadOnlyValue<bool>? VisibleWhen, IReadOnlyValue<bool>? EnabledWhen,
    IReadOnlyValue<string?>? Status, IReadOnlyList<CardAction> Actions);
