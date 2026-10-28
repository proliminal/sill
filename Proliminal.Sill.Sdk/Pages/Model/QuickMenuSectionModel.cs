using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a described section of the quick menu, above its tiles: a title with a status, and a line under it with buttons beside it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class QuickMenuSectionModel
{
    /// <summary>
    ///     Gets the section's title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets the text right of the title, such as how long something has been running, if any. It's hidden while it's empty.
    /// </summary>
    public IReadOnlyValue<string?>? Status { get; }

    /// <summary>
    ///     Gets the line under the title, if any.
    /// </summary>
    public IReadOnlyValue<string>? Description { get; }

    /// <summary>
    ///     Gets the condition the section is shown on, or <see langword="null"/> when it's always shown.
    /// </summary>
    public IReadOnlyValue<bool>? VisibleWhen { get; }

    /// <summary>
    ///     Gets the buttons under the title, in order.
    /// </summary>
    public IReadOnlyList<CardAction> Actions { get; }

    internal QuickMenuSectionModel(string title, IReadOnlyValue<string?>? status, IReadOnlyValue<string>? description, IReadOnlyValue<bool>? visibleWhen,
        IReadOnlyList<CardAction> actions)
    {
        Title = title;
        Status = status;
        Description = description;
        VisibleWhen = visibleWhen;
        Actions = actions;
    }

    /// <summary>
    ///     Gets every value in the section, including the conditions the section and its buttons are shown and enabled on.
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
