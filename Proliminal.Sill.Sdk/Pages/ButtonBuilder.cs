using Proliminal.Sill.Sdk.Pages.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card with a button. See <see cref="CardsBuilder{TSelf}.Button(string, string, PluginAction, Action{ButtonBuilder}?)"/>.
/// </summary>
public sealed class ButtonBuilder : CardBuilder<ButtonBuilder>
{
    internal ButtonBuilder(string title)
        : base(title)
    {
    }
}
