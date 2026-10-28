using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card with a text box under its description, of one or more lines.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TextAreaCard : CardElement
{
    /// <summary>
    ///     Gets the text, with lines separated by <c>\n</c>.
    ///     It changes when the box loses focus, or with each keystroke when <see cref="ChangesWhileTyping"/> is set.
    /// </summary>
    public IValue<string> Value { get; }

    /// <summary>
    ///     Gets the text shown in the box while it's empty, if any.
    /// </summary>
    public string? Placeholder { get; }

    /// <summary>
    ///     Gets the height of the box in lines; longer text scrolls. A box of 1 line doesn't accept line breaks.
    /// </summary>
    public int Lines { get; }

    /// <summary>
    ///     Gets the maximum number of characters, or 0 when there's no limit.
    /// </summary>
    public int MaxLength { get; }

    /// <summary>
    ///     Gets whether the text is shown in a monospaced font, as code is.
    /// </summary>
    public bool IsMonospace { get; }

    /// <summary>
    ///     Gets whether the text changes with each keystroke, instead of when the box loses focus.
    /// </summary>
    public bool ChangesWhileTyping { get; }

    /// <summary>
    ///     Gets whether the text can only be selected and copied, not changed, such as for a result.
    /// </summary>
    public bool IsReadOnly { get; }

    /// <summary>
    ///     Gets whether the text is masked and can't be copied, such as for a password or a token. Only for a box of 1 line.
    /// </summary>
    public bool IsSecret { get; }

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

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
