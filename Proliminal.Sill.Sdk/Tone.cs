namespace Proliminal.Sill.Sdk;

/// <summary>
///     Represents how much something asks for attention, shown in the color of its icon in a timeline, or of a card's status. Say
///     it in words too, such as with a warning icon, as not everyone tells colors apart.
/// </summary>
public enum Tone
{
    /// <summary>
    ///     The primary tone, the text color for an icon, the dimmer one of a status.
    /// </summary>
    Neutral,

    /// <summary>
    ///     Green: something that's fine, or was updated to be correct.
    /// </summary>
    Good,

    /// <summary>
    ///     Yellow: worth investigating.
    /// </summary>
    Caution,

    /// <summary>
    ///     Red: something that needs to be fixed, or is wrong.
    /// </summary>
    Critical,
}
