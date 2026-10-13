using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with a box for text of several lines under its description.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TextAreaCard : CardElement
{
    internal TextAreaCard(CardInfo info, IValue<string> value, string? placeholder, int lines, int maxLength, bool isMonospace)
        : base(info)
    {
        Value = value;
        Placeholder = placeholder;
        Lines = lines;
        MaxLength = maxLength;
        IsMonospace = isMonospace;
    }

    /// <summary>The text, with lines separated by <c>\n</c>. It's only changed when the box loses focus.</summary>
    public IValue<string> Value { get; }

    /// <summary>Text shown in the box while it's empty, if any.</summary>
    public string? Placeholder { get; }

    /// <summary>How many lines the box is high; longer text scrolls.</summary>
    public int Lines { get; }

    /// <summary>The most characters the text can have, or 0 when there's no limit.</summary>
    public int MaxLength { get; }

    /// <summary>Whether the text is shown in a monospaced font, as code is.</summary>
    public bool IsMonospace { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
