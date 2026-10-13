using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     A value a read-only card shows, as a value cards take, which can't be set.
/// </summary>
internal sealed class DisplayedValue<T>(IReadOnlyValue<T> source) : IValue<T>
{
    public T Value
        => source.Value;

    public bool IsEnabled
        => true;

    public event Action? Changed
    {
        add => source.Changed += value;
        remove => source.Changed -= value;
    }

    public Task SetAsync(T value)
        => throw new InvalidOperationException("A read-only card's value can't be changed from the card.");

    public void Refresh()
        => source.Refresh();
}
