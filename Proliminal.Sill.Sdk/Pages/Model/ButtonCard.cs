using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with a button that runs an action.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ButtonCard : CardElement
{
    internal ButtonCard(CardInfo info, IReadOnlyValue<string> label, PluginAction onClick)
        : base(info)
    {
        Label = label;
        OnClick = onClick;
    }

    /// <summary>The button's text, which can change, as from "Check for updates" to "Update".</summary>
    public IReadOnlyValue<string> Label { get; }

    /// <summary>What the button does.</summary>
    public PluginAction OnClick { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Label);
}
