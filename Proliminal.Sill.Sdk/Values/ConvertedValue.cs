using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk.Values;

internal sealed class ConvertedValue<T, TResult>(IValue<T> source, Func<T, TResult> to, Func<TResult, T> from)
    : MappedValue<T, TResult>(source, to), IValue<TResult>
{
    public bool IsEnabled
        => source.IsEnabled;

    public Task SetAsync(TResult value)
        => source.SetAsync(from(value));
}
