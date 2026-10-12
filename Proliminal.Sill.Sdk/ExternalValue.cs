using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     A value kept outside Sill. Created by <see cref="Values.External{T}(Func{T}, Func{T, Task}, Func{bool}?)"/>.
/// </summary>
internal sealed class ExternalValue<T>(Func<T> get, Func<T, Task> set, Func<bool>? isAvailable) : IValue<T>
{
    private T _value = get();
    private bool _isBusy;

    public T Value
        => _value;

    public bool IsEnabled
        => !_isBusy && (isAvailable?.Invoke() ?? true);

    public event Action? Changed;

    public async Task SetAsync(T value)
    {
        if (_isBusy || EqualityComparer<T>.Default.Equals(value, _value))
            return;

        _value = value;
        _isBusy = true;
        Changed?.Invoke();

        try
        {
            await set(value);
        }
        finally
        {
            _isBusy = false;

            Refresh();
        }
    }

    public void Refresh()
    {
        if (_isBusy)
            return;

        _value = get();
        Changed?.Invoke();
    }
}
