namespace Proliminal.Sill.Sdk;

/// <summary>
///     Who a plugin is. A plugin from outside Sill names its author and version; Sill's own plugins are part of Sill, so
///     they have neither.
/// </summary>
public sealed record PluginInfo
{
    /// <summary>
    ///     Describes a plugin from outside Sill.
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

    /// <summary>
    ///     Describes one of Sill's own plugins, which Sill marks as System.
    /// </summary>
    internal PluginInfo(string id, string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = id;
        Name = name;
    }

    /// <summary>
    ///     A name that never changes, which the plugin's settings are stored under.
    /// </summary>
    public string Id { get; }

    /// <summary>
    ///     The display name, used as the page's title and in messages.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Who made the plugin, or <see langword="null"/> for one of Sill's own.
    /// </summary>
    public string? Author { get; }

    /// <summary>
    ///     The plugin's own version, or <see langword="null"/> for one of Sill's own.
    /// </summary>
    public Version? Version { get; }
}
