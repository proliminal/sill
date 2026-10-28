using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a group of cards and lists under a header.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class GroupElement : PageElement
{
    /// <summary>
    ///     Gets the header above the cards.
    /// </summary>
    public string Header { get; }

    /// <summary>
    ///     Gets the cards and lists, from top to bottom.
    /// </summary>
    public IReadOnlyList<PageElement> Elements { get; }

    /// <summary>
    ///     Gets the condition the cards' controls are enabled on, or <see langword="null"/> when they're always enabled.
    /// </summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }

    internal GroupElement(string header, IReadOnlyList<PageElement> elements, IReadOnlyValue<bool>? visibleWhen, IReadOnlyValue<bool>? enabledWhen)
        : base(visibleWhen)
    {
        Header = header;
        Elements = elements;
        EnabledWhen = enabledWhen;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
    {
        foreach (var value in base.GetValues())
            yield return value;

        if (EnabledWhen is not null)
            yield return EnabledWhen;

        foreach (var value in Elements.SelectMany(element => element.GetValues()))
            yield return value;
    }
}
