using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets when an extra button on a card is shown and enabled. See
///     <see cref="CardBuilder{TSelf}.Action(string, PluginAction, Action{ActionBuilder}?)"/>.
/// </summary>
public sealed class ActionBuilder
{
    private IReadOnlyValue<bool>? _visibleWhen;
    private IReadOnlyValue<bool>? _enabledWhen;

    internal ActionBuilder()
    {
    }

    /// <summary>
    ///     Only shows the button while <paramref name="condition"/> is <see langword="true"/>, such as an Install button
    ///     while what it installs is missing.
    /// </summary>
    /// <param name="condition">When the button is shown.</param>
    public ActionBuilder VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    /// <summary>
    ///     Only enables the button while <paramref name="condition"/> is <see langword="true"/>. It's also disabled while
    ///     its action runs.
    /// </summary>
    /// <param name="condition">When the button is enabled.</param>
    public ActionBuilder EnabledWhen(IReadOnlyValue<bool> condition)
    {
        _enabledWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    internal CardAction Build(string label, PluginAction onClick)
        => new(label, onClick, _visibleWhen, _enabledWhen);
}
