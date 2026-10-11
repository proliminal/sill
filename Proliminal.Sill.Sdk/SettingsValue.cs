using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     One option of a plugin's settings. Created by <see cref="PluginSettingsExtensions.Value"/>.
/// </summary>
internal sealed class SettingsValue<T, TValue>(IPluginSettings<T> settings, Func<T, TValue> get, Action<T, TValue> set) : IValue<TValue>
    where T : class, new()
{
    public TValue Value
        => get(settings.Current);

    public bool IsEnabled
        => true;

    public event Action? Changed
    {
        add => settings.Changed += value;
        remove => settings.Changed -= value;
    }

    public Task SetAsync(TValue value)
    {
        if (!EqualityComparer<TValue>.Default.Equals(value, Value))
            settings.Update(s => set(s, value));

        return Task.CompletedTask;
    }

    // The file is watched, so the settings are always current.
    public void Refresh()
    {
    }
}
