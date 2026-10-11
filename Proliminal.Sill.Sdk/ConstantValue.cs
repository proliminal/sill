using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     A value that never changes, such as a description given as text.
/// </summary>
internal sealed class ConstantValue<T>(T value) : IReadOnlyValue<T>
{
    public T Value
        => value;

    // Never raised.
    public event Action? Changed
    {
        add { }
        remove { }
    }

    public void Refresh()
    {
    }
}
