namespace Proliminal.Sill.Sdk;

/// <summary>
///     How much something asks for attention, shown in the color of its icon. Say it in words too, such as with a
///     warning icon, as not everyone tells colors apart.
/// </summary>
public enum Tone
{
    /// <summary>The icon in the text color.</summary>
    Neutral,

    /// <summary>Green: something that's fine, or was put right.</summary>
    Good,

    /// <summary>Yellow: something to look at.</summary>
    Caution,

    /// <summary>Red: something to do something about. The row is tinted too.</summary>
    Critical,
}
