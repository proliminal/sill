using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up what a timeline shows of each item: its text, tag, icon, tone and details, and whether it lasts. See
///     <see cref="PageBuilder.Timeline"/>.
/// </summary>
/// <typeparam name="T">The type of the timeline's items.</typeparam>
public sealed class TimelineBuilder<T>
{
    private Func<T, string?>? _text;
    private Func<T, string?>? _tag;
    private Func<T, Glyph?>? _icon;
    private Func<T, Tone>? _tone;
    private Func<T, IReadOnlyList<CardDetail>>? _details;
    private Func<T, DateTime?>? _ended;
    private Func<T, bool>? _ongoing;
    private string? _empty;
    private IReadOnlyValue<bool>? _visibleWhen;

    internal TimelineBuilder()
    {
    }

    /// <summary>
    ///     Shows what <paramref name="text"/> returns under an item's title, or nothing when it returns
    ///     <see langword="null"/>.
    /// </summary>
    /// <param name="text">The text for an item.</param>
    public TimelineBuilder<T> Text(Func<T, string?> text)
    {
        _text = text ?? throw new ArgumentNullException(nameof(text));
        return this;
    }

    /// <summary>
    ///     Shows a word or two right of an item's title, such as the area it's about, or nothing when it returns
    ///     <see langword="null"/>.
    /// </summary>
    /// <param name="tag">The tag for an item.</param>
    public TimelineBuilder<T> Tag(Func<T, string?> tag)
    {
        _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        return this;
    }

    /// <summary>
    ///     Shows the icon <paramref name="icon"/> returns on the timeline, or a dot when it returns
    ///     <see langword="null"/>.
    /// </summary>
    /// <param name="icon">The icon for an item.</param>
    public TimelineBuilder<T> Icon(Func<T, Glyph?> icon)
    {
        _icon = icon ?? throw new ArgumentNullException(nameof(icon));
        return this;
    }

    /// <summary>
    ///     Colors an item's icon by how much it asks for attention. See <see cref="Sdk.Tone"/>.
    /// </summary>
    /// <param name="tone">The tone for an item.</param>
    public TimelineBuilder<T> Tone(Func<T, Tone> tone)
    {
        _tone = tone ?? throw new ArgumentNullException(nameof(tone));
        return this;
    }

    /// <summary>
    ///     Shows lines under an item's text, such as the command something runs, so facts don't run together in one
    ///     paragraph.
    /// </summary>
    /// <param name="details">The lines for an item, from top to bottom.</param>
    public TimelineBuilder<T> Details(Func<T, IReadOnlyList<CardDetail>> details)
    {
        _details = details ?? throw new ArgumentNullException(nameof(details));
        return this;
    }

    /// <summary>
    ///     Shows how long an item lasted, for one that's over, such as protection that was off for a while.
    /// </summary>
    /// <param name="ended">When it was over, or <see langword="null"/> for an item that didn't last.</param>
    public TimelineBuilder<T> Ended(Func<T, DateTime?> ended)
    {
        _ended = ended ?? throw new ArgumentNullException(nameof(ended));
        return this;
    }

    /// <summary>
    ///     Puts the items <paramref name="ongoing"/> is <see langword="true"/> for above the rest, as what's still going
    ///     on, with how long it's been.
    /// </summary>
    /// <param name="ongoing">Whether an item is still going on.</param>
    public TimelineBuilder<T> Ongoing(Func<T, bool> ongoing)
    {
        _ongoing = ongoing ?? throw new ArgumentNullException(nameof(ongoing));
        return this;
    }

    /// <summary>
    ///     Shows <paramref name="text"/> while there's nothing on the timeline.
    /// </summary>
    /// <param name="text">What to say, such as what will show here.</param>
    public TimelineBuilder<T> Empty(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        _empty = text;
        return this;
    }

    /// <summary>
    ///     Only shows the timeline while <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the timeline is shown.</param>
    public TimelineBuilder<T> VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    internal TimelineElement Build(IReadOnlyValue<IReadOnlyList<T>> items, Func<T, DateTime> at, Func<T, string> title)
    {
        var (text, tag, icon, tone, details, ended, ongoing) = (_text, _tag, _icon, _tone, _details, _ended, _ongoing);

        var rows = items.Map(list => (IReadOnlyList<TimelineItem>)[.. list.Select(item => new TimelineItem(
            at(item),
            title(item),
            text?.Invoke(item),
            tag?.Invoke(item),
            icon?.Invoke(item),
            tone?.Invoke(item) ?? Sdk.Tone.Neutral,
            details?.Invoke(item) ?? [],
            ended?.Invoke(item),
            ongoing?.Invoke(item) ?? false))]);

        return new TimelineElement(rows, _empty, _visibleWhen);
    }
}
