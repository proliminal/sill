using System.ComponentModel;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a paragraph at the top of the page that explains what the plugin does.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class IntroElement : PageElement
{
    /// <summary>
    ///     Gets the paragraph's text.
    /// </summary>
    public string Text { get; }

    internal IntroElement(string text)
        : base(null)
        => Text = text;
}
