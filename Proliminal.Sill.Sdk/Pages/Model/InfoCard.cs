using System.ComponentModel;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card without a control of its own: only its status and extra buttons, if any, such as whether something is
///     installed and a button that installs it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class InfoCard : CardElement
{
    internal InfoCard(CardInfo info)
        : base(info)
    {
    }
}
