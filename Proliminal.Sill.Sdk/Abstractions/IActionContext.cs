using Proliminal.Sill.Sdk.Pages;

namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     Represents what Sill offers an action while it runs.
/// </summary>
public interface IActionContext
{
    /// <summary>
    ///     Gets the plugin the action belongs to.
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
    ///     Gets the text on the clipboard.
    ///     Declare <see cref="PluginCapability.Clipboard"/> when you use it.
    /// </summary>
    /// <returns>The text, or <see langword="null"/> when there's no text on the clipboard, such as when it only holds an image.</returns>
    public string? GetClipboardText();

    /// <summary>
    ///     Sets <paramref name="text"/> on the clipboard, replacing what was on it.
    ///     Declare <see cref="PluginCapability.Clipboard"/> when you use it.
    /// </summary>
    /// <param name="text">The text, which can't be empty.</param>
    public void SetClipboardText(string text);

    /// <summary>
    ///     Sets a secret on the clipboard, such as a password, a key or a token.
    ///     Windows leaves it out of clipboard history and doesn't sync it to the user's other devices, and Sill clears it after the time the user set for copied passwords, unless something else was copied since.
    ///     Other apps can still read it while it's on the clipboard.
    /// </summary>
    /// <param name="text">The secret, which can't be empty.</param>
    public void SetSecretClipboardText(string text);

    /// <summary>
    ///     Shows a popup over the app in front, as the Find a password shortcut does, with cards as on a page and commands in its footer.
    ///     It closes with Esc or when it loses focus, and stays open while one of its commands runs.
    ///     A popup that's already open, of this plugin or another, closes first; the plugin's popup also closes when the plugin is turned off.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <paramref name="build"/> is called once, right away; mistakes in it throw here. The popup follows the values its cards are bound to while it's open, so keep those in fields of the plugin, as for a page.
    ///     </para>
    ///     <para>
    ///         Users can pin a popup with the pin next to its title. It then stays on top where they drag it, and <see cref="CommandBuilder.ClosesPopup"/> and <see cref="ClosePopup"/> leave it open.
    ///         Before closing, set what it shows next, such as what's running after Start, as a pinned popup keeps showing it. Showing the plugin's popup again while it's pinned keeps it pinned in place.
    ///     </para>
    /// </remarks>
    /// <example>
    ///     <code>
    ///     context.AddQuickAction("Add a note", Glyph.Edit, action => action.ShowPopup("Add a note", popup => popup
    ///         .TextBox("Note", note)
    ///         .Command("Add", AddAsync, command => command.IsDefault().ClosesPopup())));
    ///     </code>
    /// </example>
    /// <param name="title">The popup's title, such as <c>Track time</c>.</param>
    /// <param name="build">Describes the popup's cards and commands.</param>
    public void ShowPopup(string title, Action<PopupBuilder> build);

    /// <summary>
    ///     Closes the plugin's popup if it's open, for a command that only closes it in some cases, such as Start when nothing else runs.
    ///     For a command that always closes it, use <see cref="CommandBuilder.ClosesPopup"/>. A popup the user pinned stays open.
    /// </summary>
    public void ClosePopup();
}
