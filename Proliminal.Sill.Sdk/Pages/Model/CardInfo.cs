using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

internal sealed record CardInfo(string Title, IReadOnlyValue<string>? Description, Glyph? Icon, IReadOnlyValue<bool>? VisibleWhen, IReadOnlyValue<bool>? EnabledWhen,
    IReadOnlyValue<string?>? Status, IReadOnlyValue<Tone>? StatusTone, IReadOnlyList<CardAction> Actions, IReadOnlyValue<IReadOnlyList<CardLine>>? Lines, IReadOnlyList<string> Keywords);
