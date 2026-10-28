using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Values;

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

    public void Refresh()
    {
    }
}
