using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a settings page, from top to bottom.
///     Pass a lambda that uses it to <see cref="IPluginContext.SetPage(Glyph, Action{PageBuilder})"/>.
/// </summary>
/// <example>
///     <code>
///     context.SetPage(Glyph.Delete, page => page
///         .Intro("Opens and empties the Recycle Bin.")
///         .Toggle("Hide from desktop", hidden)
///         .Group("Actions", group => group
///             .Button("Open Recycle Bin", "Open", Open)));
///     </code>
/// </example>
public sealed class PageBuilder : CardsBuilder<PageBuilder>
{
    private readonly List<PageElement> _elements = [];
    private readonly List<string> _keywords = [];

    private PageBuilder()
    {
    }

    /// <summary>
    ///     Adds a paragraph that explains what the plugin does. It's usually the first element on the page.
    /// </summary>
    /// <param name="text">The paragraph.</param>
    public PageBuilder Intro(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        _elements.Add(new IntroElement(text));
        return this;
    }

    /// <summary>
    ///     Adds search terms that find this page in Sill, such as other names for what it does or the names of what it works with.
    ///     The page's title and description, and the titles and descriptions of its cards, are already searched.
    ///     For a term that belongs to a single card, use <see cref="Abstractions.CardBuilder{TSelf}.Keywords"/> on that card instead, so the page opens with the card in view.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Keywords("base64", "url encode", "percent-encoding")
    ///     </code>
    /// </example>
    /// <param name="keywords">The search terms, each a word or a few. Case is ignored.</param>
    public PageBuilder Keywords(params string[] keywords)
    {
        Keyword.AddTo(_keywords, keywords);
        return this;
    }

    /// <summary>
    ///     Adds cards under a header, such as the options for one part of the plugin.
    /// </summary>
    /// <param name="header">The header above the cards.</param>
    /// <param name="build">Adds the cards.</param>
    public PageBuilder Group(string header, Action<GroupBuilder> build)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(header);
        ArgumentNullException.ThrowIfNull(build);

        var builder = new GroupBuilder();
        build(builder);

        _elements.Add(builder.Build(header));
        return this;
    }

    /// <summary>
    ///     Adds a timeline of <paramref name="items"/> that updates as they change.
    ///     Items are shown newest first under a heading per day, with the time of each, and ongoing items above the rest.
    ///     The timeline has its own headings, so it's usually the only element on a subpage.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Timeline(events, item => item.At, item => item.Title, timeline => timeline
    ///         .Text(item => item.Text)
    ///         .Tag(item => item.Area)
    ///         .Icon(item => item.IsWarning ? Glyph.Warning : null)
    ///         .Tone(item => item.IsWarning ? Tone.Caution : Tone.Neutral)
    ///         .EmptyText("Nothing happened yet."))
    ///     </code>
    /// </example>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <param name="items">The items, in any order.</param>
    /// <param name="at">Gets when an item happened or started, in local time.</param>
    /// <param name="title">Gets what happened.</param>
    /// <param name="build">Sets the text, tags, icons, tones and details of the items, and which of them last.</param>
    public PageBuilder Timeline<T>(IReadOnlyValue<IReadOnlyList<T>> items, Func<T, DateTime> at, Func<T, string> title, Action<TimelineBuilder<T>>? build = null)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(at);
        ArgumentNullException.ThrowIfNull(title);

        var builder = new TimelineBuilder<T>();
        build?.Invoke(builder);

        _elements.Add(builder.Build(items, at, title));
        return this;
    }

    private protected override void Add(PageElement element)
        => _elements.Add(element);

    internal static PageModel Build(Action<PageBuilder> build)
    {
        ArgumentNullException.ThrowIfNull(build);

        var builder = new PageBuilder();
        build(builder);

        return new PageModel(builder._elements.ToArray(), builder._keywords.ToArray());
    }
}
