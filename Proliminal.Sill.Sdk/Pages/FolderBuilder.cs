using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Sets up a card that picks a folder. See <see cref="CardsBuilder{TSelf}.Folder"/>.
/// </summary>
public sealed class FolderBuilder : CardBuilder<FolderBuilder>
{
    private string _dialogTitle;

    internal FolderBuilder(string title)
        : base(title)
        => _dialogTitle = title;

    /// <summary>
    ///     Titles the window that picks a folder with <paramref name="title"/>, such as <c>Save images to</c>. By default
    ///     it's the card's title.
    /// </summary>
    /// <param name="title">The window's title.</param>
    public FolderBuilder DialogTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        _dialogTitle = title;
        return this;
    }

    internal FolderCard Build(IValue<string> value)
        => new(Info, value, _dialogTitle);
}
