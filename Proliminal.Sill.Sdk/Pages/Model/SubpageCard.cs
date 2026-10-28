using System.ComponentModel;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a chevron that opens a subpage titled after the card, as in Windows Settings.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class SubpageCard : CardElement
{
    /// <summary>
    ///     Gets the subpage. Its values are refreshed while it's shown, not along with the card's page.
    /// </summary>
    public PageModel Page { get; }

    internal SubpageCard(CardInfo info, PageModel page)
        : base(info)
        => Page = page;
}
