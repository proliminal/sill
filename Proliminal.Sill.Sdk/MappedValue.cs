using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     A value computed from another. Created by <see cref="ValueExtensions.Map"/>.
/// </summary>
internal class MappedValue<T, TResult>(IReadOnlyValue<T> source, Func<T, TResult> map) : IReadOnlyValue<TResult>
{
    public TResult Value
        => map(source.Value);

    public event Action? Changed
    {
        add => source.Changed += value;
        remove => source.Changed -= value;
    }

    public void Refresh()
        => source.Refresh();
}
