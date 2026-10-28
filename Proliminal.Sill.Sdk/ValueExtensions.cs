using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Values;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     A set of extension methods for deriving values from others or creating them from other objects.
/// </summary>
public static class ValueExtensions
{
    #region Derived Values

    /// <summary>
    ///     Gets a value computed from <paramref name="source"/>, which changes with it.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .VisibleWhen(saveLocation.Map(location => location == SaveLocation.Custom))
    ///     </code>
    /// </example>
    /// <param name="source">The value to compute from.</param>
    /// <param name="map">Computes the value.</param>
    public static IReadOnlyValue<TResult> Map<T, TResult>(this IReadOnlyValue<T> source, Func<T, TResult> map)
        => new MappedValue<T, TResult>(source, map);

    /// <summary>
    ///     Gets a value computed from <paramref name="first"/> and <paramref name="second"/>, which changes with either.
    /// </summary>
    /// <example>
    ///     <code>
    ///     var status = isInstalled.Combine(installing, (installed, name) => installed ? "Installed" : name is null ? null : "Installing…");
    ///     </code>
    /// </example>
    /// <param name="first">One value to compute from.</param>
    /// <param name="second">The other value to compute from.</param>
    /// <param name="combine">Computes the value.</param>
    public static IReadOnlyValue<TResult> Combine<T1, T2, TResult>(this IReadOnlyValue<T1> first, IReadOnlyValue<T2> second, Func<T1, T2, TResult> combine)
        => new CombinedValue<T1, T2, TResult>(first, second, combine);

    /// <summary>
    ///     Gets <paramref name="source"/> as another type, both ways, such as a whole number for a slider that works with <see cref="double"/>.
    /// </summary>
    /// <param name="source">The value to convert.</param>
    /// <param name="to">Converts a value of <paramref name="source"/>.</param>
    /// <param name="from">Converts a new value back for <paramref name="source"/>.</param>
    public static IValue<TResult> Convert<T, TResult>(this IValue<T> source, Func<T, TResult> to, Func<TResult, T> from)
        => new ConvertedValue<T, TResult>(source, to, from);

    /// <summary>
    ///     Gets whether <paramref name="source"/> is <paramref name="expected"/>.
    /// </summary>
    /// <param name="source">The value to compare.</param>
    /// <param name="expected">The value it's compared with.</param>
    public static IReadOnlyValue<bool> IsEqualTo<T>(this IReadOnlyValue<T> source, T expected)
        => source.Map(value => EqualityComparer<T>.Default.Equals(value, expected));

    /// <summary>
    ///     Gets the opposite value of <paramref name="source"/> by simply inverting it. For example, a card is only shown while a setting is <see langword="false"/>.
    /// </summary>
    /// <param name="source">The value to invert.</param>
    public static IReadOnlyValue<bool> Invert(this IReadOnlyValue<bool> source)
        => source.Map(value => !value);

    #endregion

    #region Factories

    /// <summary>
    ///     Creates a value that represents an option of the settings, for a card to show and change. Changing it saves the settings, and edits to the file show up.
    /// </summary>
    /// <example>
    ///     <code>
    ///     var loud = settings.Value(s => s.Loud, (s, value) => s.Loud = value);
    ///     </code>
    /// </example>
    /// <param name="settings">The plugin's settings.</param>
    /// <param name="get">Reads the option.</param>
    /// <param name="set">Changes the option.</param>
    public static IValue<TValue> Value<T, TValue>(this IPluginSettings<T> settings, Func<T, TValue> get, Action<T, TValue> set)
        where T : class, new()
        => new SettingsValue<T, TValue>(settings, get, set);

    #endregion
}
