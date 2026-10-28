namespace Proliminal.Sill.Sdk;

/// <summary>
///     Represents what a plugin's code can do that people want to know about before they load it.
///     Declare the ones your plugin uses with <see cref="PluginUsesAttribute"/>.
/// </summary>
public enum PluginCapability
{
    /// <summary>
    ///     Connects to the internet or the local network.
    /// </summary>
    Network,

    /// <summary>
    ///     Reads or changes what's on the clipboard.
    /// </summary>
    Clipboard,

    /// <summary>
    ///     Watches what's typed or clicked in other apps, as a keyboard or mouse hook does.
    /// </summary>
    KeyboardAndMouse,

    /// <summary>
    ///     Types or clicks as if the user did, as <c>SendInput</c> does.
    /// </summary>
    SendsInput,

    /// <summary>
    ///     Starts other programs.
    /// </summary>
    StartsPrograms,

    /// <summary>
    ///     Reads or changes the registry.
    /// </summary>
    Registry,

    /// <summary>
    ///     Loads more code while it runs, or calls code through reflection or COM, which Sill can't look at beforehand.
    /// </summary>
    LoadsCode,

    /// <summary>
    ///     Reads or changes files and folders.
    /// </summary>
    Files,
}
