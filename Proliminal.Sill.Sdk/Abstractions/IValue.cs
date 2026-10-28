namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     Represents a value of type <typeparamref name="T"/> a card shows and changes, such as whether a toggle is on.
/// </summary>
/// <typeparam name="T">The value's type.</typeparam>
public interface IValue<T> : IReadOnlyValue<T>
{
    /// <summary>
    ///     Gets whether the value can be changed now. Cards bound to it are disabled while it's <see langword="false"/>, such as while a change is being applied.
    /// </summary>
    public bool IsEnabled { get; }

    /// <summary>
    ///     Sets the value. When it's done, <see cref="IReadOnlyValue{T}.Value"/> is what was actually applied.
    ///     If it throws, Sill shows the error as a notification.
    /// </summary>
    /// <param name="value">The new value.</param>
    public Task SetAsync(T value);
}
