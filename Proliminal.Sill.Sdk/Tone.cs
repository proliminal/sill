namespace Proliminal.Sill.Sdk;

/// <summary>
///     How much something asks for attention, shown in the color of its icon in a timeline, or of a card's status. Say
///     it in words too, such as with a warning icon, as not everyone tells colors apart.
/// </summary>
public enum Tone
{
    /// <summary>The usual color: the text color for an icon, the dimmer one of a status.</summary>
    Neutral,

    /// <summary>Green: something that's fine, or was put right.</summary>
    Good,

    /// <summary>Yellow: something to look at.</summary>
    Caution,

    /// <summary>Red: something to do something about. A timeline's row is tinted too.</summary>
    Critical,
}
