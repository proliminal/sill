using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a section of the quick menu, above its tiles: a status right of its title, a line under it with buttons on the right, and when it's shown.
///     A section has no icon.
///     See <see cref="IPluginContext.AddQuickMenuSection"/>.
/// </summary>
public sealed class QuickMenuSectionBuilder
{
    private readonly List<CardAction> _actions = [];
    private IReadOnlyValue<string?>? _status;
    private IReadOnlyValue<string>? _description;
    private IReadOnlyValue<bool>? _visibleWhen;

    private QuickMenuSectionBuilder()
    {
    }

    /// <summary>
    ///     Shows <paramref name="status"/> right of the title, such as how long something has been running.
    ///     It updates while the menu is open, and is hidden while it's <see langword="null"/> or empty.
    /// </summary>
    /// <param name="status">A few words.</param>
    public QuickMenuSectionBuilder Status(IReadOnlyValue<string?> status)
    {
        _status = status ?? throw new ArgumentNullException(nameof(status));
        return this;
    }

    /// <summary>
    ///     Shows <paramref name="description"/> under the title, such as what's running.
    /// </summary>
    /// <param name="description">A sentence, or a few words.</param>
    public QuickMenuSectionBuilder Description(IReadOnlyValue<string> description)
    {
        _description = description ?? throw new ArgumentNullException(nameof(description));
        return this;
    }

    /// <summary>
    ///     Adds a button to the section, after the buttons added before it.
    ///     As with a tile, it runs once the menu has closed. If it throws, Sill shows the error as a notification.
    /// </summary>
    /// <param name="label">The button text, such as <c>Stop</c>.</param>
    /// <param name="onClick">What the button does.</param>
    /// <param name="build">Sets when the button is shown and enabled.</param>
    public QuickMenuSectionBuilder Action(string label, PluginAction onClick, Action<ActionBuilder>? build = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);
        ArgumentNullException.ThrowIfNull(onClick);

        var builder = new ActionBuilder();
        build?.Invoke(builder);

        _actions.Add(builder.Build(label, onClick));
        return this;
    }

    /// <inheritdoc cref="Action(string, PluginAction, Action{ActionBuilder}?)"/>
    public QuickMenuSectionBuilder Action(string label, Action<IActionContext> onClick, Action<ActionBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(onClick);

        return Action(label, context =>
        {
            onClick(context);
            return Task.CompletedTask;
        }, build);
    }

    /// <summary>
    ///     Shows the section only while <paramref name="condition"/> is <see langword="true"/>, such as while something is running.
    ///     While it's hidden, the menu leaves no space for it.
    /// </summary>
    /// <param name="condition">When the section is shown.</param>
    public QuickMenuSectionBuilder VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    internal static QuickMenuSectionModel Build(string title, Action<QuickMenuSectionBuilder> build)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentNullException.ThrowIfNull(build);

        var builder = new QuickMenuSectionBuilder();
        build(builder);

        return new QuickMenuSectionModel(title, builder._status, builder._description, builder._visibleWhen, builder._actions.ToArray());
    }
}
