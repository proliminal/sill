namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     A value a page shows, which tells when it changed. Create one with <see cref="Values"/> or
///     <see cref="IPluginSettings{T}"/>, keep it in a field of the plugin and bind cards to it.
/// </summary>
public interface IReadOnlyValue
{
    /// <summary>
    ///     Raised after the value changed. It can be raised from any thread; Sill updates the page on the UI thread.
    /// </summary>
    public event Action? Changed;

    /// <summary>
    ///     Reads the value again from wherever it's kept, raising <see cref="Changed"/>. Sill calls this when the
    ///     settings window comes to the front, so changes made outside Sill show up.
    /// </summary>
    public void Refresh();
}
