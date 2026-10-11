namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     A plugin's settings in <c>settings.json</c>, kept up to date when the file is edited. Get it from
///     <see cref="IPluginContext.GetSettings{T}"/>.
/// </summary>
/// <typeparam name="T">A class with a property per option, serialized as JSON.</typeparam>
public interface IPluginSettings<T>
    where T : class, new()
{
    /// <summary>
    ///     The current settings. Read this when you need a value instead of keeping a copy, so edits apply right away.
    ///     Don't change it; use <see cref="Update"/>.
    /// </summary>
    public T Current { get; }

    /// <summary>
    ///     Raised on the UI thread after the settings changed, in the file or through <see cref="Update"/>.
    /// </summary>
    public event Action? Changed;

    /// <summary>
    ///     Applies <paramref name="change"/> to a copy of the current settings and saves it. Call on the UI thread.
    /// </summary>
    /// <param name="change">Changes the options to save.</param>
    public void Update(Action<T> change);
}
