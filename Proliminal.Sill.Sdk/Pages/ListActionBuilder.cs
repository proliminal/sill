using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets when a button on a list's rows is enabled. See
///     <see cref="ListBuilder{T}.Action(Glyph, string, Func{T, IActionContext, Task}, Action{ListActionBuilder{T}}?)"/>.
/// </summary>
/// <typeparam name="T">The type of the list's items.</typeparam>
public sealed class ListActionBuilder<T>
{
    private Func<T, bool>? _isEnabled;
    private IReadOnlyValue<bool>? _enabledWhen;

    internal ListActionBuilder()
    {
    }

    /// <summary>
    ///     Only enables the button on rows whose item <paramref name="isEnabled"/> is <see langword="true"/> for, such as
    ///     Open for a folder that exists.
    /// </summary>
    /// <param name="isEnabled">Whether the button can be used for an item.</param>
    public ListActionBuilder<T> EnabledWhen(Func<T, bool> isEnabled)
    {
        _isEnabled = isEnabled ?? throw new ArgumentNullException(nameof(isEnabled));
        return this;
    }

    /// <summary>
    ///     Only enables the button, on every row, while <paramref name="condition"/> is <see langword="true"/>, such as
    ///     while no other change is being made.
    /// </summary>
    /// <param name="condition">When the button is enabled.</param>
    public ListActionBuilder<T> EnabledWhen(IReadOnlyValue<bool> condition)
    {
        _enabledWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    internal IReadOnlyValue<bool>? Condition
        => _enabledWhen;

    internal ListItemAction Build(T item, Glyph icon, string label, Func<T, IActionContext, Task> onClick)
        => new(icon, label, context => onClick(item, context), _isEnabled?.Invoke(item) ?? true, _enabledWhen);
}
