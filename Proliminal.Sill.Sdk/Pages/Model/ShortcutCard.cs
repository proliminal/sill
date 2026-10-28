using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a keyboard shortcut, changed by pressing the new keys.
///     Only Sill's own pages use it for now.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ShortcutCard : CardElement
{
    /// <summary>
    ///     Gets the shortcut as Sill writes it, such as <c>Ctrl+Shift+Alt+P</c>, or empty when it's off.
    /// </summary>
    public IValue<string> Value { get; }

    internal ShortcutCard(CardInfo info, IValue<string> value)
        : base(info)
        => Value = value;

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
