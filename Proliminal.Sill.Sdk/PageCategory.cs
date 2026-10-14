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
    public static PageCategory Developer { get; } = new("Developer");

    /// <summary>
    ///     Tools for code and data, such as encoding text, hashing it, making GUIDs and reading JWTs. Pages here work on
    ///     what's typed or copied, on the PC only.
    /// </summary>
    public static PageCategory Tools { get; } = new("Tools");

    /// <summary>
    ///     What your account shares, which apps use your camera, microphone and location, and what the PC's owner manages.
    ///     Pages here only change settings of the signed-in account, never those of the PC or its organization.
    /// </summary>
    public static PageCategory Privacy { get; } = new("Privacy");

    /// <summary>
    ///     How well Windows Security and the PC protect the account, what Windows Security doesn't say, and what starts
    ///     with the account. Pages here only change settings of the signed-in account; the PC's they show.
    /// </summary>
    public static PageCategory Security { get; } = new("Security");

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
