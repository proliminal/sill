using System.ComponentModel;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with a chevron that opens a subpage, titled as the card, as in Windows Settings.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class SubpageCard : CardElement
{
    internal SubpageCard(CardInfo info, PageModel page)
        : base(info)
        => Page = page;

    /// <summary>The subpage. Its values are refreshed while it's shown, not with the card's page.</summary>
    public PageModel Page { get; }
}
