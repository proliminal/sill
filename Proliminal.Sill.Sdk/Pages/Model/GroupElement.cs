using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Cards and lists under a header.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class GroupElement : PageElement
{
    internal GroupElement(string header, IReadOnlyList<PageElement> elements, IReadOnlyValue<bool>? visibleWhen, IReadOnlyValue<bool>? enabledWhen)
        : base(visibleWhen)
    {
        Header = header;
        Elements = elements;
        EnabledWhen = enabledWhen;
    }

    /// <summary>The header above the cards.</summary>
    public string Header { get; }

    /// <summary>The cards and lists, from top to bottom.</summary>
    public IReadOnlyList<PageElement> Elements { get; }

    /// <summary>The cards' controls are only enabled while this is <see langword="true"/>, or always when it's <see langword="null"/>.</summary>
    public IReadOnlyValue<bool>? EnabledWhen { get; }

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
