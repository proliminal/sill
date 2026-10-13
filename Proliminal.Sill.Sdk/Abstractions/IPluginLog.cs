namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     Writes to Sill's log, which users can send along with a problem report from General › Troubleshooting. Entries
///     say they come from the plugin, as <c>Plugin &lt;id&gt;</c>. Can be used from any thread, at any time, also from a
///     thread the plugin started itself.
/// </summary>
/// <remarks>
///     The log stays on the user's PC; Sill doesn't send it anywhere. Their profile folder is written as
///     <c>%USERPROFILE%</c>, but don't log what's private, such as a password or what the user typed. The same entry
///     written again and again is kept once, with how often it repeated.
/// </remarks>
public interface IPluginLog
{
    /// <summary>
    ///     Something worth knowing when reading back what happened.
    /// </summary>
    /// <param name="message">What happened, as a sentence.</param>
    public void Info(string message);

    /// <summary>
    ///     Something failed, and the plugin went on without it.
    /// </summary>
    /// <param name="message">What failed, and what the plugin did instead.</param>
    /// <param name="exception">Why it failed, written below the message with its stack trace.</param>
    public void Warning(string message, Exception? exception = null);

    /// <summary>
    ///     Something failed that the user notices.
    /// </summary>
    /// <param name="message">What failed.</param>
    /// <param name="exception">Why it failed, written below the message with its stack trace.</param>
    public void Error(string message, Exception? exception = null);
}
