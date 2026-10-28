namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     Represents a value of type <typeparamref name="T"/> a page shows, such as the condition a card is shown on.
/// </summary>
/// <typeparam name="T">The value's type.</typeparam>
public interface IReadOnlyValue<out T> : IReadOnlyValue
{
    /// <summary>
    ///     Gets the current value.
    /// </summary>
    public T Value { get; }
}
