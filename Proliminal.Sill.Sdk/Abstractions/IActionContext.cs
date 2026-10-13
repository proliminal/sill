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
}
