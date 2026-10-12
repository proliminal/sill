namespace Proliminal.Sill.Sdk;

/// <summary>
///     Says what a plugin uses and why, for Sill to show before it's loaded. Sill reads it from the DLL without running
///     the plugin, and shows next to it what it found in the plugin's code. A use Sill finds that isn't declared is
///     pointed out, so declare every <see cref="PluginCapability"/> the plugin's code has.
/// </summary>
/// <remarks>
///     It's what the plugin says about itself, not a limit: once loaded, a plugin can do anything Sill can, and Sill says
///     so where it shows these.
/// </remarks>
/// <example>
///     <code>
///     [assembly: PluginUses(PluginCapability.KeyboardAndMouse, "Watches Caps Lock, to type Morse code while it's held.")]
///     </code>
/// </example>
/// <param name="capability">What the plugin uses.</param>
/// <param name="reason">Why, in a sentence people read before loading it, such as <c>Reads the text you copy to decode it.</c></param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class PluginUsesAttribute(PluginCapability capability, string reason) : Attribute
{
    /// <summary>
    ///     What the plugin uses.
    /// </summary>
    public PluginCapability Capability { get; } = capability;

    /// <summary>
    ///     Why the plugin uses it.
    /// </summary>
    public string Reason { get; } = reason;
}
