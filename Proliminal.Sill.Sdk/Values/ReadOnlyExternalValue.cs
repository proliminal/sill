using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Values;

internal sealed class ReadOnlyExternalValue<T>(Func<T> get) : IReadOnlyValue<T>
{
    private T _value = get();

    public T Value
        => _value;

    public event Action? Changed;

    public void Refresh()
    {
        _value = get();
        Changed?.Invoke();
    }
}
