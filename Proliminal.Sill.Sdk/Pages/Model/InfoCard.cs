using System.ComponentModel;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card without a control of its own, showing only its status and extra buttons, if any.
///     For example, whether something is installed, and a button that installs it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class InfoCard : CardElement
{
    internal InfoCard(CardInfo info)
        : base(info)
    {
    }
}
