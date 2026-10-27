using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card with a box for text under its description, of one line or several.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TextAreaCard : CardElement
{
    internal TextAreaCard(CardInfo info, IValue<string> value, string? placeholder, int lines, int maxLength, bool isMonospace, bool changesWhileTyping, bool isReadOnly, bool isSecret)
        : base(info)
    {
        IsSecret = isSecret;
        Value = value;
        Placeholder = placeholder;
        Lines = lines;
        MaxLength = maxLength;
        IsMonospace = isMonospace;
        ChangesWhileTyping = changesWhileTyping;
        IsReadOnly = isReadOnly;
    }

    /// <summary>
    ///     The text, with lines separated by <c>\n</c>. It's changed when the box loses focus, or with each change while
    ///     <see cref="ChangesWhileTyping"/>.
    /// </summary>
    public IValue<string> Value { get; }

    /// <summary>Text shown in the box while it's empty, if any.</summary>
    public string? Placeholder { get; }

    /// <summary>How many lines the box is high; longer text scrolls. A box of 1 line takes no line breaks.</summary>
    public int Lines { get; }

    /// <summary>The most characters the text can have, or 0 when there's no limit.</summary>
    public int MaxLength { get; }

    /// <summary>Whether the text is shown in a monospaced font, as code is.</summary>
    public bool IsMonospace { get; }

    /// <summary>Whether the text is changed with each change while typing, instead of when the box loses focus.</summary>
    public bool ChangesWhileTyping { get; }

    /// <summary>Whether the text can only be selected and copied, not changed, as for a result.</summary>
    public bool IsReadOnly { get; }

    /// <summary>Whether the text is shown as dots and can't be copied, as for a password or a token. Only for a box of 1 line.</summary>
    public bool IsSecret { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
