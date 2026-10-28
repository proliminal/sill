namespace Proliminal.Sill.Sdk;

/// <summary>
///     Represents what a plugin is. A plugin from outside Sill names its author and version; Sill's own plugins are part of Sill, so
///     they have neither.
/// </summary>
public sealed record PluginInfo
{
    /// <summary>
    ///     Creates a new instance of <see cref="PluginInfo"/> which describes a plugin from outside Sill, naming its author and version.
    /// </summary>
    /// <param name="id">
    ///     A name that never changes, of letters and digits starting with a letter, such as <c>HelloWorld</c>. The
    ///     plugin's settings are stored under it in <c>settings.json</c>, so changing it loses them.
    /// </param>
    /// <param name="name">The display name, used as the page's title and in messages, such as <c>Hello world</c>.</param>
    /// <param name="author">Who made the plugin, as shown where plugins are listed.</param>
    /// <param name="version">The plugin's own version, as shown where plugins are listed.</param>
    public PluginInfo(string id, string name, string author, Version version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(author);
        ArgumentNullException.ThrowIfNull(version);

        Id = id;
        Name = name;
        Author = author;
        Version = version;
    }

    internal PluginInfo(string id, string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = id;
        Name = name;
    }

    /// <summary>
    ///     Gets the plugin ID, an immutable identification of letters and digits starting with a letter, such as <c>HelloWorld</c>.
    /// </summary>
    public string Id { get; }

    /// <summary>
    ///     Gets the plugin display name, used as the page's title and in messages.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets who made the plugin, or <see langword="null"/> for one of Sill's own.
    /// </summary>
    public string? Author { get; }

    /// <summary>
    ///     Gets the plugin's own version, or <see langword="null"/> for one of Sill's own.
    /// </summary>
    public Version? Version { get; }
}
