namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Takes the keywords a page or card is searched by, as their builders do.
/// </summary>
internal static class Keyword
{
    /// <summary>
    ///     Adds <paramref name="keywords"/> to <paramref name="list"/>, each checked to have something in it first.
    /// </summary>
    public static void AddTo(List<string> list, string[] keywords)
    {
        ArgumentNullException.ThrowIfNull(keywords);

        foreach (var keyword in keywords)
            ArgumentException.ThrowIfNullOrWhiteSpace(keyword);

        list.AddRange(keywords);
    }
}
