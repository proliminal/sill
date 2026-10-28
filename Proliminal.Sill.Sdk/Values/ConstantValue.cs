using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Values;

internal sealed class ConstantValue<T>(T value) : IReadOnlyValue<T>
{
    public T Value
        => value;

    public event Action? Changed
    {
        add { }
        remove { }
    }

    public void Refresh()
    {
    }
}
