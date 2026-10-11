using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model.Abstractions;

/// <summary>
///     A row on the page, as in Windows Settings: an icon, title and description on the left and a control on the right.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class CardElement : PageElement
{
    private protected CardElement(CardInfo info)
        : base(info.VisibleWhen)
    {
        Title = info.Title;
        Description = info.Description;
        Icon = info.Icon;
        EnabledWhen = info.EnabledWhen;
        Status = info.Status;
        Actions = info.Actions;
    }

    /// <summary>The card's title.</summary>
    public string Title { get; }

    /// <summary>Text under the title, if any.</summary>
    public IReadOnlyValue<string>? Description { get; }

    /// <summary>The icon left of the title, if any.</summary>
    public Glyph? Icon { get; }

    /// <summary>The control is only enabled while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }

    /// <summary>Text left of the control, such as <c>Installed</c>, if any. It's hidden while it's empty.</summary>
    public IReadOnlyValue<string?>? Status { get; }

    /// <summary>Extra buttons left of the control, in order.</summary>
    public IReadOnlyList<CardAction> Actions { get; }

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

        foreach (var action in Actions)
        {
            if (action.VisibleWhen is not null)
                yield return action.VisibleWhen;

            if (action.EnabledWhen is not null)
                yield return action.EnabledWhen;
        }
    }
}
