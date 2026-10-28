using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Values;

internal sealed class CombinedValue<T1, T2, TResult>(IReadOnlyValue<T1> first, IReadOnlyValue<T2> second, Func<T1, T2, TResult> combine)
    : IReadOnlyValue<TResult>
{
    public TResult Value
        => combine(first.Value, second.Value);

    public event Action? Changed
    {
        add
        {
            first.Changed += value;
            second.Changed += value;
        }
        remove
        {
            first.Changed -= value;
            second.Changed -= value;
        }
    }

    public void Refresh()
    {
        first.Refresh();
        second.Refresh();
    }
}
