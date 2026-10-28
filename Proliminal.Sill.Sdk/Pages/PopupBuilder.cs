using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for a popup: cards from top to bottom, as on a page, and commands in its footer that Enter or a Ctrl shortcut press.
///     Pass a lambda that uses it to <see cref="IActionContext.ShowPopup"/>.
///     To ask for different things one after another, toggle cards and commands with <see cref="CardBuilder{TSelf}.VisibleWhen"/> and <see cref="CommandBuilder.VisibleWhen"/> on a <see cref="State{T}"/> that the commands change. When a command is done, the first visible box gets the focus.
/// </summary>
/// <example>
///     <code>
///     action.ShowPopup("Add a note", popup => popup
///         .TextBox("Note", note, text => text.Placeholder("What happened"))
///         .Message(error)
///         .Command("Add", AddAsync, command => command.IsDefault().ClosesPopup()));
///     </code>
/// </example>
public sealed class PopupBuilder : CardsBuilder<PopupBuilder>
{
    private readonly List<PageElement> _elements = [];
    private readonly List<PopupCommand> _commands = [];
    private IReadOnlyValue<string?>? _message;
    private IReadOnlyValue<Tone>? _messageTone;

    private PopupBuilder()
    {
    }

    /// <summary>
    ///     Shows <paramref name="message"/> above the footer, such as what's wrong with the input. It's hidden while it's <see langword="null"/> or empty.
    ///     Errors thrown by a command are shown there too, in red, until a command runs again.
    /// </summary>
    /// <param name="message">A sentence or two.</param>
    public PopupBuilder Message(IReadOnlyValue<string?> message)
    {
        _message = message ?? throw new ArgumentNullException(nameof(message));
        return this;
    }

    /// <summary>
    ///     Shows <paramref name="message"/> above the footer in the color of <paramref name="tone"/>.
    ///     See <see cref="Message(IReadOnlyValue{string?})"/>.
    /// </summary>
    /// <param name="message">A sentence or two.</param>
    /// <param name="tone">The message color. <see cref="Tone.Neutral"/> is the default text color.</param>
    public PopupBuilder Message(IReadOnlyValue<string?> message, IReadOnlyValue<Tone> tone)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(tone);

        _message = message;
        _messageTone = tone;
        return this;
    }

    /// <summary>
    ///     Adds cards side by side, each equally wide, such as a short box and a switch that would otherwise each take a full row.
    ///     A hidden card, as with <see cref="CardBuilder{TSelf}.VisibleWhen"/>, leaves its width to the others.
    ///     Use it only for small cards, as a popup is narrow.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Row(row => row
    ///         .TextBox("Time to log", duration, text => text.Placeholder("1:30"))
    ///         .Toggle("Billable", billable))
    ///     </code>
    /// </example>
    /// <param name="build">Adds two or more cards, from left to right.</param>
    public PopupBuilder Row(Action<RowBuilder> build)
    {
        ArgumentNullException.ThrowIfNull(build);

        var builder = new RowBuilder();
        build(builder);

        _elements.Add(builder.Build());
        return this;
    }

    /// <summary>
    ///     Adds a button to the footer, right of the buttons added before it.
    ///     While it runs, the popup stays open when it loses focus, and its commands are disabled.
    ///     If it throws, the error is shown above the footer instead of as a notification, and the popup stays open.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Command("Start", StartAsync, command => command
    ///         .IsDefault()
    ///         .ClosesPopup()
    ///         .VisibleWhen(isRunning.Invert()))
    ///     </code>
    /// </example>
    /// <param name="label">The button text, such as <c>Start</c>.</param>
    /// <param name="run">What the button does. The boxes in the popup have their values set before it runs.</param>
    /// <param name="build">Sets the shortcut key, whether the command closes the popup, and when it's shown and enabled.</param>
    public PopupBuilder Command(string label, PluginAction run, Action<CommandBuilder>? build = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);
        ArgumentNullException.ThrowIfNull(run);

        var builder = new CommandBuilder();
        build?.Invoke(builder);

        _commands.Add(builder.Build(label, run));
        return this;
    }

    /// <inheritdoc cref="Command(string, PluginAction, Action{CommandBuilder}?)"/>
    public PopupBuilder Command(string label, Action<IActionContext> run, Action<CommandBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(run);

        return Command(label, context =>
        {
            run(context);
            return Task.CompletedTask;
        }, build);
    }

    private protected override void Add(PageElement element)
        => _elements.Add(element);

    internal static PopupModel Build(string title, Action<PopupBuilder> build)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentNullException.ThrowIfNull(build);

        var builder = new PopupBuilder();
        build(builder);

        return new PopupModel(title, builder._elements.ToArray(), builder._commands.ToArray(), builder._message, builder._messageTone);
    }
}
