using System.ComponentModel;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents an event on a timeline: something that happened, or started, at one moment.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TimelineItem
{
    /// <summary>
    ///     Gets when the event happened or started, in local time.
    /// </summary>
    public DateTime StartedAt { get; }

    /// <summary>
    ///     Gets what happened.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets more about the event, shown under the title, if any.
    /// </summary>
    public string? Text { get; }

    /// <summary>
    ///     Gets a word or two right of the title, such as the area it's about, if any.
    /// </summary>
    public string? Tag { get; }

    /// <summary>
    ///     Gets the icon on the timeline, or <see langword="null"/> for a dot.
    /// </summary>
    public Glyph? Icon { get; }

    /// <summary>
    ///     Gets how much the event asks for attention, shown in the color of its icon.
    /// </summary>
    public Tone Tone { get; }

    /// <summary>
    ///     Gets the lines under the text, from top to bottom.
    /// </summary>
    public IReadOnlyList<CardLine> Lines { get; }

    /// <summary>
    ///     Gets when a lasting event ended, if it has.
    /// </summary>
    public DateTime? EndedAt { get; }

    /// <summary>
    ///     Gets whether the event is lasting and still ongoing, which places it above the rest.
    /// </summary>
    public bool IsOngoing { get; }

    internal TimelineItem(DateTime startedAt, string title, string? text, string? tag, Glyph? icon, Tone tone, IReadOnlyList<CardLine> lines, DateTime? endedAt, bool isOngoing)
    {
        StartedAt = startedAt;
        Title = title;
        Text = text;
        Tag = tag;
        Icon = icon;
        Tone = tone;
        Lines = lines;
        EndedAt = endedAt;
        IsOngoing = isOngoing;
    }
}
