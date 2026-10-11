namespace Proliminal.Sill.Sdk;

/// <summary>
///     A group of pages that share one entry in the settings window's navigation bar, as Personalization does in Windows
///     Settings. Its page links to the pages in it.
/// </summary>
public sealed class PageCategory
{
    private PageCategory(string id)
        => Id = id;

    /// <summary>
    ///     Shells, the terminal and the folders they find programs in.
    /// </summary>
    public static PageCategory Command { get; } = new("Command");

    /// <summary>
    ///     Sill's own plugin pages: its plugins, the ones from the plugins folder, and the plugin documentation. Only Sill
    ///     puts pages here.
    /// </summary>
    internal static PageCategory Plugins { get; } = new("Plugins");

    /// <summary>
    ///     The name Sill knows the category by.
    /// </summary>
    public string Id { get; }

    /// <inheritdoc />
    public override string ToString()
        => Id;
}
