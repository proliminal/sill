using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     Binds cards to options in a plugin's settings.
/// </summary>
public static class PluginSettingsExtensions
{
    /// <summary>
    ///     One option of the settings, for a card to show and change. Changing it saves the settings, and edits to the
    ///     file show up.
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
}
