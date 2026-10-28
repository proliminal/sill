using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     Represents a value kept in memory. Create it with <see cref="Value.State{T}"/>.
/// </summary>
/// <typeparam name="T">The value's type.</typeparam>
public sealed class State<T> : IValue<T>
{
    private T _value;

    internal State(T initial)
        => _value = initial;

    /// <summary>
    ///     Gets or sets the current value. Setting it updates the cards bound to it; it can be set from any thread.
    /// </summary>
    public T Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<T>.Default.Equals(value, _value))
                return;

            _value = value;
            Changed?.Invoke();
        }
    }

    /// <inheritdoc />
    public bool IsEnabled
        => true;

    /// <inheritdoc />
    public event Action? Changed;

    /// <inheritdoc />
    public Task SetAsync(T value)
    {
        Value = value;
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Does nothing: the value is only kept here.
    /// </summary>
    public void Refresh()
    {
    }
}
