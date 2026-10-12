using System.ComponentModel;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Something on a timeline: what happened, or what started, at one moment.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class TimelineItem
{
    internal TimelineItem(DateTime at, string title, string? text, string? tag, Glyph? icon, Tone tone, IReadOnlyList<CardDetail> details, DateTime? endedAt, bool isOngoing)
    {
        At = at;
        Title = title;
        Text = text;
        Tag = tag;
        Icon = icon;
        Tone = tone;
        Details = details;
        EndedAt = endedAt;
        IsOngoing = isOngoing;
    }

    /// <summary>When it happened or started, in local time.</summary>
    public DateTime At { get; }

    /// <summary>What happened.</summary>
    public string Title { get; }

    /// <summary>More about it under the title, if anything.</summary>
    public string? Text { get; }

    /// <summary>A word or two right of the title, such as the area it's about, if any.</summary>
    public string? Tag { get; }

    /// <summary>The icon on the timeline, if any; a dot otherwise.</summary>
    public Glyph? Icon { get; }

    /// <summary>How much it asks for attention, in the color of its icon.</summary>
    public Tone Tone { get; }

    /// <summary>Lines under the text, from top to bottom.</summary>
    public IReadOnlyList<CardDetail> Details { get; }

    /// <summary>When something that lasted was over, if it was.</summary>
    public DateTime? EndedAt { get; }

    /// <summary>Whether it lasts and is still going on, which puts it above the rest.</summary>
    public bool IsOngoing { get; }
}
