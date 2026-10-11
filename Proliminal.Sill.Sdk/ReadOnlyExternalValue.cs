using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     A value kept outside Sill that cards only show. Created by <see cref="Values.External{T}(Func{T})"/>.
/// </summary>
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
