using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model.Abstractions;

/// <summary>
///     Represents a card on a page, as in Windows Settings: an icon, title and description on the left and a control on the right.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class CardElement : PageElement
{
    /// <summary>
    ///     Gets the card's title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets the text under the title, if any.
    /// </summary>
    public IReadOnlyValue<string>? Description { get; }

    /// <summary>
    ///     Gets the icon left of the title, if any.
    /// </summary>
    public Glyph? Icon { get; }

    /// <summary>
    ///     Gets the condition the control is enabled on, or <see langword="null"/> when it's always enabled.
    /// </summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }

    /// <summary>
    ///     Gets the text left of the control, such as <c>Installed</c>, if any. It's hidden while it's empty.
    /// </summary>
    public IReadOnlyValue<string?>? Status { get; }

    /// <summary>
    ///     Gets the color of <see cref="Status"/>, or <see langword="null"/> for the default status color.
    /// </summary>
    public IReadOnlyValue<Tone>? StatusTone { get; }

    /// <summary>
    ///     Gets the extra buttons left of the control, in order.
    /// </summary>
    public IReadOnlyList<CardAction> Actions { get; }

    /// <summary>
    ///     Gets other words people search for the card by, besides its title and description.
    /// </summary>
    public IReadOnlyList<string> Keywords { get; }

    /// <summary>
    ///     Gets the lines inside the card under its description, if any.
    /// </summary>
    public IReadOnlyValue<IReadOnlyList<CardLine>>? Lines { get; }

    private protected CardElement(CardInfo info)
        : base(info.VisibleWhen)
    {
        Title = info.Title;
        Description = info.Description;
        Icon = info.Icon;
        EnabledWhen = info.EnabledWhen;
        Status = info.Status;
        StatusTone = info.StatusTone;
        Lines = info.Lines;
        Actions = info.Actions;
        Keywords = info.Keywords;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
    {
        foreach (var value in base.GetValues())
            yield return value;

        if (Description is not null)
            yield return Description;

        if (EnabledWhen is not null)
            yield return EnabledWhen;

        if (Status is not null)
            yield return Status;

        if (StatusTone is not null)
            yield return StatusTone;

        if (Lines is not null)
            yield return Lines;

        foreach (var action in Actions)
        {
            if (action.VisibleWhen is not null)
                yield return action.VisibleWhen;

            if (action.EnabledWhen is not null)
                yield return action.EnabledWhen;
        }
    }
}
