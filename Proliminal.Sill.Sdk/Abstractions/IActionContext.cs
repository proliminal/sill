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
}
