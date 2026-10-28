using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Values;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     Implements factories for creating values for cards to bind to, other than settings, which come from <see cref="IPluginSettings{T}"/>.
/// </summary>
public static class Value
{
    /// <summary>
    ///     Creates a value kept in memory, which the plugin can also change through <see cref="State{T}.Value"/>. It starts
    ///     over each time Sill starts.
    /// </summary>
    /// <param name="initial">The value it starts with.</param>
    public static State<T> State<T>(T initial)
        => new(initial);

    /// <summary>
    ///     Creates a value kept outside Sill, such as a Windows setting.
    ///     It's read with <paramref name="get"/> now and again on <see cref="IReadOnlyValue.Refresh"/>, as when the settings window comes to the front.
    ///     Setting it shows the new value right away, disables its cards while <paramref name="set"/> runs, and reads it back afterwards, also
    ///     when <paramref name="set"/> failed.
    /// </summary>
    /// <param name="get">Reads the value.</param>
    /// <param name="set">Applies a new value.</param>
    /// <param name="isAvailable">Whether the value can be changed now, as when what it changes is installed.</param>
    public static IValue<T> External<T>(Func<T> get, Func<T, Task> set, Func<bool>? isAvailable = null)
        => new ExternalValue<T>(get, set, isAvailable);

    /// <inheritdoc cref="External{T}(Func{T}, Func{T, Task}, Func{bool}?)"/>
    public static IValue<T> External<T>(Func<T> get, Action<T> set, Func<bool>? isAvailable = null)
        => new ExternalValue<T>(get, value =>
        {
            set(value);
            return Task.CompletedTask;
        }, isAvailable);

    /// <summary>
    ///     Creates a value kept outside Sill that cards only show, or are shown or enabled on, such as whether an app is installed.
    ///     It's read with <paramref name="get"/> now and again on <see cref="IReadOnlyValue.Refresh"/>, as when the settings window comes to the front.
    /// </summary>
    /// <param name="get">Reads the value.</param>
    public static IReadOnlyValue<T> External<T>(Func<T> get)
        => new ReadOnlyExternalValue<T>(get ?? throw new ArgumentNullException(nameof(get)));
}
