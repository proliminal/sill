using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a button that runs an action.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ButtonCard : CardElement
{
    /// <summary>
    ///     Gets the button's text, which can change, such as from "Check for updates" to "Update".
    /// </summary>
    public IReadOnlyValue<string> Label { get; }

    /// <summary>
    ///     Gets the action the button runs.
    /// </summary>
    public PluginAction OnClick { get; }

    internal ButtonCard(CardInfo info, IReadOnlyValue<string> label, PluginAction onClick)
        : base(info)
    {
        Label = label;
        OnClick = onClick;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Label);
}
