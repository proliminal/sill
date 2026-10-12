using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with a keyboard shortcut that's changed by pressing the new keys. Only Sill's own pages have it for now.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ShortcutCard : CardElement
{
    internal ShortcutCard(CardInfo info, IValue<string> value)
        : base(info)
        => Value = value;

    /// <summary>The shortcut as Sill writes it, such as <c>Ctrl+Shift+Alt+P</c>, or empty when it's off.</summary>
    public IValue<string> Value { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
