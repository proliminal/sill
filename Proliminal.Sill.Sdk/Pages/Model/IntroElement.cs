using System.ComponentModel;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A paragraph saying what the plugin does, at the top of the page.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class IntroElement : PageElement
{
    internal IntroElement(string text)
        : base(null)
        => Text = text;

    /// <summary>The paragraph.</summary>
    public string Text { get; }
}
