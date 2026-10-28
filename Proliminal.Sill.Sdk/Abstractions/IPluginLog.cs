namespace Proliminal.Sill.Sdk.Abstractions;

/// <summary>
///     Represents Sill's log, which users can send along with a problem report from General › Troubleshooting.
///     Entries are marked as coming from the plugin, as <c>Plugin &lt;id&gt;</c>. It can be used from any thread, at any time.
/// </summary>
/// <remarks>
///     The log stays on the user's PC; Sill doesn't send it anywhere. The profile folder is written as <c>%USERPROFILE%</c>, but don't log what's private, such as a password or what the user typed.
///     An entry written repeatedly is kept once, with how often it repeated.
/// </remarks>
public interface IPluginLog
{
    /// <summary>
    ///     Writes something worth knowing when reading back what happened.
    /// </summary>
    /// <param name="message">What happened, as a sentence.</param>
    public void Info(string message);

    /// <summary>
    ///     Writes that something failed, and the plugin continued without it.
    /// </summary>
    /// <param name="message">What failed, and what the plugin did instead.</param>
    /// <param name="exception">Why it failed, written below the message with its stack trace.</param>
    public void Warning(string message, Exception? exception = null);

    /// <summary>
    ///     Writes that something failed that the user notices.
    /// </summary>
    /// <param name="message">What failed.</param>
    /// <param name="exception">Why it failed, written below the message with its stack trace.</param>
    public void Error(string message, Exception? exception = null);
}
