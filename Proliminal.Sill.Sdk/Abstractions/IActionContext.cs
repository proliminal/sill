using Proliminal.Sill.Sdk.Pages;

namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     What Sill offers an action while it runs.
/// </summary>
public interface IActionContext
{
    /// <summary>
    ///     The plugin the action belongs to.
    /// </summary>
    public PluginInfo Info { get; }

    /// <summary>
    ///     Shows a Windows notification from Sill's tray icon.
    /// </summary>
    /// <param name="title">The notification's first line.</param>
    /// <param name="text">The notification's text.</param>
    /// <param name="kind">The icon shown with it.</param>
    public void Notify(string title, string text, NotificationKind kind = NotificationKind.None);

    /// <summary>
    ///     Asks the user to pick a folder, in front of the settings window when it's open.
    /// </summary>
    /// <param name="title">The title of the window that picks the folder, such as <c>Add a folder to your PATH</c>.</param>
    /// <returns>The folder's full path, or <see langword="null"/> when the user cancelled.</returns>
    public Task<string?> PickFolderAsync(string title);

    /// <summary>
    ///     Asks the user to pick a file, in front of the settings window when it's open.
    /// </summary>
    /// <param name="title">The title of the window that picks the file, such as <c>Hash a file</c>.</param>
    /// <returns>The file's full path, or <see langword="null"/> when the user cancelled.</returns>
    public Task<string?> PickFileAsync(string title);

    /// <summary>
    ///     Reads the text on the clipboard. Declare <see cref="PluginCapability.Clipboard"/> when you use it.
    /// </summary>
    /// <returns>The text, or <see langword="null"/> when there's no text on the clipboard, such as only an image.</returns>
    public string? GetClipboardText();

    /// <summary>
    ///     Puts <paramref name="text"/> on the clipboard, in place of what was on it. Declare
    ///     <see cref="PluginCapability.Clipboard"/> when you use it.
    /// </summary>
    /// <param name="text">The text, which can't be empty.</param>
    public void SetClipboardText(string text);

    /// <summary>
    ///     Copies a secret, such as a password, a key or a token: Windows leaves it out of clipboard history and doesn't
    ///     sync it to the user's other devices, and Sill clears it after a while, as long as the user set for copied
    ///     passwords, unless something else was copied since. Other apps can still read it while it's on the clipboard.
    /// </summary>
    /// <param name="text">The secret; not empty.</param>
    public void SetSecretClipboardText(string text);

    /// <summary>
    ///     Shows a popup over the app in front, as the Find a password shortcut does: cards as on a page, and commands in
    ///     its footer. It's drawn as Sill's own windows are, closes with Esc or when it loses the focus, and stays open
    ///     while one of its commands runs. A popup already open, of this plugin or another, closes first; the plugin's
    ///     popup also closes when it's turned off.
    /// </summary>
    /// <remarks>
    ///     <paramref name="build"/> is called once, right away; mistakes in it throw here. The popup follows the values
    ///     its cards are bound to while it's open, so keep those in fields of the plugin, as for a page.
    /// </remarks>
    /// <example>
    ///     <code>
    ///     context.AddQuickAction("Add a note", Glyph.Edit, action => action.ShowPopup("Add a note", popup => popup
    ///         .TextBox("Note", note)
    ///         .Command("Add", AddAsync, command => command.Default().Closes())));
    ///     </code>
    /// </example>
    /// <param name="title">The popup's title, such as <c>Track time</c>.</param>
    /// <param name="build">Describes the popup's cards and commands.</param>
    public void ShowPopup(string title, Action<PopupBuilder> build);

    /// <summary>
    ///     Closes the plugin's popup, if it's open, as a command does that only closes it sometimes, such as Start when
    ///     nothing else runs. For a command that always closes it, <see cref="CommandBuilder.Closes"/> is shorter.
    /// </summary>
    public void ClosePopup();
}
