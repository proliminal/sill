using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a card with a dropdown that picks any number of tags.
///     See <see cref="CardsBuilder{TSelf}.Tags"/>.
/// </summary>
/// <typeparam name="T">The type of the tags.</typeparam>
public sealed class TagsBuilder<T> : CardBuilder<TagsBuilder<T>>
{
    private readonly string _title;
    private IReadOnlyValue<IReadOnlyList<T>>? _options;
    private Func<T, string>? _label;
    private Func<T, string?>? _color;
    private Func<T, string?>? _group;
    private string _placeholder = "Search";

    internal TagsBuilder(string title)
        : base(title)
        => _title = title;

    /// <summary>
    ///     Sets the tags that can be picked, which can change, such as when they're read from a web service.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Options(available, tag => tag.Name)
    ///     </code>
    /// </example>
    /// <param name="options">The tags, in the order they're listed.</param>
    /// <param name="label">Gets the text shown for a tag.</param>
    public TagsBuilder<T> Options(IReadOnlyValue<IReadOnlyList<T>> options, Func<T, string> label)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(label);

        if (_options is not null)
            throw new InvalidOperationException($"The tags \"{_title}\" already have their options.");

        _options = options;
        _label = label;
        return this;
    }

    /// <summary>
    ///     Gives each tag a color, shown as a dot by its name in the list and under the description. Tags without one have the accent color.
    /// </summary>
    /// <param name="color">Gets a tag's color as <c>#RRGGBB</c>, or <see langword="null"/> for the accent color.</param>
    public TagsBuilder<T> Color(Func<T, string?> color)
    {
        _color = color ?? throw new ArgumentNullException(nameof(color));
        return this;
    }

    /// <summary>
    ///     Lists the tags under headers, such as the project they belong to. Groups are listed in the order their first tag is.
    /// </summary>
    /// <param name="group">Gets the header of a tag's group, or <see langword="null"/> for none.</param>
    public TagsBuilder<T> Group(Func<T, string?> group)
    {
        _group = group ?? throw new ArgumentNullException(nameof(group));
        return this;
    }

    /// <summary>
    ///     Sets the text in the list's search box while it's empty, such as <c>Search tags</c>. The default is <c>Search</c>.
    /// </summary>
    /// <param name="placeholder">A few words.</param>
    public TagsBuilder<T> Placeholder(string placeholder)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(placeholder);

        _placeholder = placeholder;
        return this;
    }

    internal TagsCard Build(IValue<IReadOnlyList<T>> value)
    {
        if (_options is not { } options || _label is not { } label)
            throw new InvalidOperationException($"The tags \"{_title}\" have no options; add them with Options.");

        var shown = options.Map(list => (IReadOnlyList<TagOption>)[.. list.Select(tag => new TagOption(label(tag), _color?.Invoke(tag), _group?.Invoke(tag)))]);

        // Picked tags that aren't options, as when the options changed, aren't shown, and are left out once one's picked or removed.
        var selectedIndices = value.Convert(
            picked => (IReadOnlyList<int>)[.. picked.Select(tag => IndexOf(options.Value, tag)).Where(index => index >= 0)],
            indices => (IReadOnlyList<T>)[.. indices.Select(index => options.Value[index])]);

        return new TagsCard(Info, shown, selectedIndices, _placeholder);
    }

    private static int IndexOf(IReadOnlyList<T> options, T value)
    {
        for (var i = 0; i < options.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(options[i], value))
                return i;
        }

        return -1;
    }
}
