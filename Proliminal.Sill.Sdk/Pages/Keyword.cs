namespace Proliminal.Sill.Sdk.Pages;

internal static class Keyword
{
    public static void AddTo(List<string> list, string[] keywords)
    {
        ArgumentNullException.ThrowIfNull(keywords);

        foreach (var keyword in keywords)
            ArgumentException.ThrowIfNullOrWhiteSpace(keyword);

        list.AddRange(keywords);
    }
}
