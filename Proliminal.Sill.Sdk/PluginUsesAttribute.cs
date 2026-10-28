namespace Proliminal.Sill.Sdk;

/// <summary>
///     Says what a plugin uses and why, for Sill to show before it's loaded. Sill reads it from the DLL without running
///     the plugin, and shows next to it what it found in the plugin's code. A use Sill finds that isn't declared is
///     pointed out, so declare every <see cref="PluginCapability"/> the plugin's code has.
/// </summary>
/// <example>
///     <code>
///     [assembly: PluginUses(PluginCapability.KeyboardAndMouse, "Watches Caps Lock, to type special characters while it's held.")]
///     </code>
/// </example>
/// <param name="capability">What capability the plugin uses.</param>
/// <param name="reason">Why, in a sentence people read before loading it, such as <c>Reads the text you copy to decode it.</c></param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class PluginUsesAttribute(PluginCapability capability, string reason) : Attribute
{
    /// <summary>
    ///     Gets the capability the plugin uses.
    /// </summary>
    public PluginCapability Capability { get; } = capability;

    /// <summary>
    ///     Gets the reason the plugin uses the capability.
    /// </summary>
    public string Reason { get; } = reason;
}
