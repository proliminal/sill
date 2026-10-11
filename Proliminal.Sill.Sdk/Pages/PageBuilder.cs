using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Describes a settings page, from top to bottom. Pass a lambda that uses it to
///     <see cref="IPluginContext.SetPage(Glyph, Action{PageBuilder})"/>.
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

    private PageBuilder()
    {
    }

    /// <summary>
    ///     Adds a paragraph saying what the plugin does. Usually the first thing on the page.
    /// </summary>
    /// <param name="text">The paragraph.</param>
    public PageBuilder Intro(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        _elements.Add(new IntroElement(text));
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

    private protected override void Add(PageElement element)
        => _elements.Add(element);

    /// <summary>
    ///     Describes a page with <paramref name="build"/>.
    /// </summary>
    internal static PageModel Build(Action<PageBuilder> build)
    {
        ArgumentNullException.ThrowIfNull(build);

        var builder = new PageBuilder();
        build(builder);

        return new PageModel(builder._elements.ToArray());
    }
}
