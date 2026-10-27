using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A described section of the quick menu, above its tiles: a title with a status, and a line under it with buttons
///     beside it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class QuickMenuSectionModel
{
    internal QuickMenuSectionModel(string title, IReadOnlyValue<string?>? status, IReadOnlyValue<string>? description, IReadOnlyValue<bool>? visibleWhen,
        IReadOnlyList<CardAction> actions)
    {
        Title = title;
        Status = status;
        Description = description;
        VisibleWhen = visibleWhen;
        Actions = actions;
    }

    /// <summary>The section's title.</summary>
    public string Title { get; }

    /// <summary>Text right of the title, such as how long something has run, if any. It's hidden while it's empty.</summary>
    public IReadOnlyValue<string?>? Status { get; }

    /// <summary>A line under the title, if any.</summary>
    public IReadOnlyValue<string>? Description { get; }

    /// <summary>The section is only shown while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    /// <summary>The buttons under it, in order.</summary>
    public IReadOnlyList<CardAction> Actions { get; }

    /// <summary>
    ///     Every value in the section, including the conditions it and its buttons are shown and enabled on.
    /// </summary>
    public IEnumerable<IReadOnlyValue> GetValues()
    {
        if (Status is not null)
            yield return Status;

        if (Description is not null)
            yield return Description;

        if (VisibleWhen is not null)
            yield return VisibleWhen;

        foreach (var action in Actions)
        {
            if (action.VisibleWhen is not null)
                yield return action.VisibleWhen;

            if (action.EnabledWhen is not null)
                yield return action.EnabledWhen;
        }
    }
}
