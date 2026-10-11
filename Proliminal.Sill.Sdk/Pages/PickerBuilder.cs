using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card with a dropdown. See <see cref="CardsBuilder{TSelf}.Picker"/>.
/// </summary>
/// <typeparam name="T">The type of the options' values.</typeparam>
public sealed class PickerBuilder<T> : CardBuilder<PickerBuilder<T>>
{
    private readonly string _title;
    private readonly List<(T Value, string Label)> _options = [];
    private IReadOnlyValue<IReadOnlyList<(T Value, string Label)>>? _changingOptions;

    internal PickerBuilder(string title)
        : base(title)
        => _title = title;

    /// <summary>
    ///     Adds an option to the dropdown, below the ones added before.
    /// </summary>
    /// <param name="value">The value chosen with it.</param>
    /// <param name="label">The text shown for it.</param>
    public PickerBuilder<T> Option(T value, string label)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);

        if (_changingOptions is not null)
            throw new InvalidOperationException($"The picker \"{_title}\" already takes its options from a value.");

        if (_options.Any(option => EqualityComparer<T>.Default.Equals(option.Value, value)))
            throw new ArgumentException($"The picker \"{_title}\" already has an option for {value}.", nameof(value));

        _options.Add((value, label));
        return this;
    }

    /// <summary>
    ///     Takes the dropdown's options from <paramref name="options"/>, which can change, such as the fonts that are
    ///     installed. Use it instead of <see cref="Option"/>.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Options(Values.External(GetInstalledFonts), font => font ?? "Same as other profiles")
    ///     </code>
    /// </example>
    /// <param name="options">The options' values, in order.</param>
    /// <param name="label">The text shown for a value.</param>
    public PickerBuilder<T> Options(IReadOnlyValue<IReadOnlyList<T>> options, Func<T, string> label)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(label);

        if (_options.Count > 0 || _changingOptions is not null)
            throw new InvalidOperationException($"The picker \"{_title}\" already has its options.");

        _changingOptions = options.Map(values => (IReadOnlyList<(T, string)>)[.. values.Select(value => (value, label(value)))]);
        return this;
    }

    internal PickerCard Build(IValue<T> value)
    {
        var options = _changingOptions ?? (_options.Count > 0
            ? new ConstantValue<IReadOnlyList<(T Value, string Label)>>(_options.ToArray())
            : throw new InvalidOperationException($"The picker \"{_title}\" has no options; add them with Option or Options."));

        var selectedIndex = value.Convert(
            current => IndexOf(options.Value, current),
            index => options.Value[index].Value);

        return new PickerCard(Info, options.Map(list => (IReadOnlyList<string>)[.. list.Select(option => option.Label)]), selectedIndex);
    }

    private static int IndexOf(IReadOnlyList<(T Value, string Label)> options, T value)
    {
        for (var i = 0; i < options.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(options[i].Value, value))
                return i;
        }

        return -1;
    }
}
