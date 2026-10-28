using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a described popup: its title, its cards from top to bottom, a message above its footer and the commands in it.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PopupModel
{
    /// <summary>
    ///     Gets the popup's title, above its cards.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets the popup's cards, from top to bottom.
    /// </summary>
    public IReadOnlyList<PageElement> Elements { get; }

    /// <summary>
    ///     Gets the buttons in the footer, from left to right, and the keys that press them.
    /// </summary>
    public IReadOnlyList<PopupCommand> Commands { get; }

    /// <summary>
    ///     Gets the line above the footer, if any. It's hidden while it's empty.
    /// </summary>
    public IReadOnlyValue<string?>? Message { get; }

    /// <summary>
    ///     Gets the color of <see cref="Message"/>, or <see langword="null"/> for the default text color.
    /// </summary>
    public IReadOnlyValue<Tone>? MessageTone { get; }

    internal PopupModel(string title, IReadOnlyList<PageElement> elements, IReadOnlyList<PopupCommand> commands, IReadOnlyValue<string?>? message,
        IReadOnlyValue<Tone>? messageTone)
    {
        Title = title;
        Elements = elements;
        Commands = commands;
        Message = message;
        MessageTone = messageTone;
    }

    /// <summary>
    ///     Gets every value in the popup, including the conditions cards and commands are shown and enabled on.
    /// </summary>
    public IEnumerable<IReadOnlyValue> GetValues()
    {
        foreach (var element in Elements)
        {
            foreach (var value in element.GetValues())
                yield return value;
        }

        foreach (var command in Commands)
        {
            if (command.VisibleWhen is not null)
                yield return command.VisibleWhen;

            if (command.EnabledWhen is not null)
                yield return command.EnabledWhen;
        }

        if (Message is not null)
            yield return Message;

        if (MessageTone is not null)
            yield return MessageTone;
    }
}
