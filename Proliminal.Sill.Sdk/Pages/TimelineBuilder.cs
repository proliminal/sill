using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for what a timeline shows of each item: its text, tag, icon, tone and details, and whether it lasts.
///     See <see cref="PageBuilder.Timeline"/>.
/// </summary>
/// <typeparam name="T">The type of the timeline items.</typeparam>
public sealed class TimelineBuilder<T>
{
    private Func<T, string?>? _text;
    private Func<T, string?>? _tag;
    private Func<T, Glyph?>? _icon;
    private Func<T, Tone>? _tone;
    private Func<T, IReadOnlyList<CardLine>>? _lines;
    private Func<T, DateTime?>? _endedAt;
    private Func<T, bool>? _isOngoing;
    private string? _emptyText;
    private IReadOnlyValue<bool>? _visibleWhen;

    internal TimelineBuilder()
    {
    }

    #region Items

    /// <summary>
    ///     Shows the text that <paramref name="text"/> returns under the title of an item, or nothing when it returns <see langword="null"/>.
    /// </summary>
    /// <param name="text">Gets the text for an item.</param>
    public TimelineBuilder<T> Text(Func<T, string?> text)
    {
        _text = text ?? throw new ArgumentNullException(nameof(text));
        return this;
    }

    /// <summary>
    ///     Shows a word or two right of the title of an item, such as the area it's about, or nothing when <paramref name="tag"/> returns <see langword="null"/>.
    /// </summary>
    /// <param name="tag">Gets the tag for an item.</param>
    public TimelineBuilder<T> Tag(Func<T, string?> tag)
    {
        _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        return this;
    }

    /// <summary>
    ///     Shows the icon that <paramref name="icon"/> returns on the timeline, or a dot when it returns <see langword="null"/>.
    /// </summary>
    /// <param name="icon">Gets the icon for an item.</param>
    public TimelineBuilder<T> Icon(Func<T, Glyph?> icon)
    {
        _icon = icon ?? throw new ArgumentNullException(nameof(icon));
        return this;
    }

    /// <summary>
    ///     Colors the icon of an item by how much attention it needs.
    ///     See <see cref="Sdk.Tone"/>.
    /// </summary>
    /// <param name="tone">Gets the tone for an item.</param>
    public TimelineBuilder<T> Tone(Func<T, Tone> tone)
    {
        _tone = tone ?? throw new ArgumentNullException(nameof(tone));
        return this;
    }

    /// <summary>
    ///     Shows lines under the text of an item, such as the command something runs, so separate facts aren't merged into one paragraph.
    /// </summary>
    /// <param name="lines">Gets the lines for an item, from top to bottom.</param>
    public TimelineBuilder<T> Lines(Func<T, IReadOnlyList<CardLine>> lines)
    {
        _lines = lines ?? throw new ArgumentNullException(nameof(lines));
        return this;
    }

    /// <summary>
    ///     Shows how long an item lasted once it has ended, such as protection that was turned off for a while.
    /// </summary>
    /// <param name="endedAt">Gets when an item ended, or <see langword="null"/> for an item that didn't last.</param>
    public TimelineBuilder<T> EndedAt(Func<T, DateTime?> endedAt)
    {
        _endedAt = endedAt ?? throw new ArgumentNullException(nameof(endedAt));
        return this;
    }

    /// <summary>
    ///     Shows the items for which <paramref name="isOngoing"/> returns <see langword="true"/> above the rest, as ongoing, with how long they've been going on.
    /// </summary>
    /// <param name="isOngoing">Gets whether an item is ongoing.</param>
    public TimelineBuilder<T> IsOngoing(Func<T, bool> isOngoing)
    {
        _isOngoing = isOngoing ?? throw new ArgumentNullException(nameof(isOngoing));
        return this;
    }

    #endregion

    #region Timeline

    /// <summary>
    ///     Shows <paramref name="text"/> while the timeline has no items.
    /// </summary>
    /// <param name="text">The text to show, such as what will appear here.</param>
    public TimelineBuilder<T> EmptyText(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        _emptyText = text;
        return this;
    }

    /// <summary>
    ///     Shows the timeline only while <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the timeline is shown.</param>
    public TimelineBuilder<T> VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    #endregion

    internal TimelineElement Build(IReadOnlyValue<IReadOnlyList<T>> items, Func<T, DateTime> at, Func<T, string> title)
    {
        var (text, tag, icon, tone, lines, endedAt, isOngoing) = (_text, _tag, _icon, _tone, _lines, _endedAt, _isOngoing);

        var rows = items.Map(list => (IReadOnlyList<TimelineItem>)[.. list.Select(item => new TimelineItem(
            at(item),
            title(item),
            text?.Invoke(item),
            tag?.Invoke(item),
            icon?.Invoke(item),
            tone?.Invoke(item) ?? Sdk.Tone.Neutral,
            lines?.Invoke(item) ?? [],
            endedAt?.Invoke(item),
            isOngoing?.Invoke(item) ?? false))]);

        return new TimelineElement(rows, _emptyText, _visibleWhen);
    }
}
