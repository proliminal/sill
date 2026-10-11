using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a list's rows: their description, icon and buttons. See <see cref="CardsBuilder{TSelf}.List"/>.
/// </summary>
/// <typeparam name="T">The type of the list's items.</typeparam>
public sealed class ListBuilder<T>
{
    private readonly List<(Glyph Icon, string Label, Func<T, IActionContext, Task> OnClick, ListActionBuilder<T> Builder)> _actions = [];
    private Func<T, string?>? _description;
    private Func<T, Glyph?>? _icon;
    private IReadOnlyValue<bool>? _visibleWhen;

    internal ListBuilder()
    {
    }

    /// <summary>
    ///     Shows what <paramref name="description"/> returns under a row's title, or nothing when it returns
    ///     <see langword="null"/>.
    /// </summary>
    /// <param name="description">The text for an item.</param>
    public ListBuilder<T> Description(Func<T, string?> description)
    {
        _description = description ?? throw new ArgumentNullException(nameof(description));
        return this;
    }

    /// <summary>
    ///     Shows the icon <paramref name="icon"/> returns left of a row's title, or none when it returns
    ///     <see langword="null"/>.
    /// </summary>
    /// <param name="icon">The icon for an item.</param>
    public ListBuilder<T> Icon(Func<T, Glyph?> icon)
    {
        _icon = icon ?? throw new ArgumentNullException(nameof(icon));
        return this;
    }

    /// <summary>
    ///     Adds a small icon button to every row, right of the ones added before.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Action(Glyph.Delete, "Remove from PATH", (entry, _) => RemoveAsync(entry), action => action
    ///         .EnabledWhen(isIdle))
    ///     </code>
    /// </example>
    /// <param name="icon">The button's icon.</param>
    /// <param name="label">What it does, shown as its tooltip and read out by screen readers.</param>
    /// <param name="onClick">What it does for a row's item. The button is disabled until its task is done.</param>
    /// <param name="build">Sets when the button is enabled.</param>
    public ListBuilder<T> Action(Glyph icon, string label, Func<T, IActionContext, Task> onClick, Action<ListActionBuilder<T>>? build = null)
    {
        if (icon.Text is null)
            throw new ArgumentException("Use one of Glyph's icons, or Glyph.FromCodepoint.", nameof(icon));

        ArgumentException.ThrowIfNullOrWhiteSpace(label);
        ArgumentNullException.ThrowIfNull(onClick);

        var builder = new ListActionBuilder<T>();
        build?.Invoke(builder);

        _actions.Add((icon, label, onClick, builder));
        return this;
    }

    /// <summary>
    ///     Adds a button whose action is done right away.
    ///     See <see cref="Action(Glyph, string, Func{T, IActionContext, Task}, Action{ListActionBuilder{T}}?)"/>.
    /// </summary>
    /// <param name="icon">The button's icon.</param>
    /// <param name="label">What it does, shown as its tooltip and read out by screen readers.</param>
    /// <param name="onClick">What it does for a row's item.</param>
    /// <param name="build">Sets when the button is enabled.</param>
    public ListBuilder<T> Action(Glyph icon, string label, Action<T, IActionContext> onClick, Action<ListActionBuilder<T>>? build = null)
    {
        ArgumentNullException.ThrowIfNull(onClick);

        return Action(icon, label, (item, context) =>
        {
            onClick(item, context);
            return Task.CompletedTask;
        }, build);
    }

    /// <summary>
    ///     Only shows the list while <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the list is shown.</param>
    public ListBuilder<T> VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    internal ListElement Build(IReadOnlyValue<IReadOnlyList<T>> items, Func<T, string> title)
    {
        var actions = _actions.ToArray();
        var description = _description;
        var icon = _icon;

        var rows = items.Map(list => (IReadOnlyList<ListItem>)[.. list.Select(item => new ListItem(
            title(item),
            description?.Invoke(item),
            icon?.Invoke(item),
            [.. actions.Select(action => action.Builder.Build(item, action.Icon, action.Label, action.OnClick))]))]);

        IReadOnlyList<IReadOnlyValue<bool>> conditions = [.. actions.Select(action => action.Builder.Condition).OfType<IReadOnlyValue<bool>>().Distinct()];

        return new ListElement(rows, conditions, _visibleWhen);
    }
}
