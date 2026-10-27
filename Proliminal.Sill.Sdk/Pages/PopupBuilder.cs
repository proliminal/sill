using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Describes a popup: cards from top to bottom, as on a page, and commands in its footer that Enter or Ctrl and a
///     letter press. Pass a lambda that uses it to <see cref="IActionContext.ShowPopup"/>.
/// </summary>
/// <remarks>
///     A popup that asks for different things one after another, such as first what to start and later whether to stop
///     it, shows them with <see cref="CardBuilder{TSelf}.VisibleWhen"/> and <see cref="CommandBuilder.VisibleWhen"/> on
///     a <see cref="State{T}"/> its commands change: when a command is done, the first box that's shown gets the focus.
/// </remarks>
/// <example>
///     <code>
///     action.ShowPopup("Add a note", popup => popup
///         .TextBox("Note", note, text => text.Placeholder("What happened"))
///         .Message(error)
///         .Command("Add", AddAsync, command => command.Default().Closes()));
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
    ///     Shows <paramref name="message"/> above the footer, such as what's wrong with what was typed. It's hidden while
    ///     it's <see langword="null"/> or empty. What a command throws is shown there too, in red, until a command runs
    ///     again.
    /// </summary>
    /// <param name="message">A sentence or two.</param>
    public PopupBuilder Message(IReadOnlyValue<string?> message)
    {
        _message = message ?? throw new ArgumentNullException(nameof(message));
        return this;
    }

    /// <summary>
    ///     Shows <paramref name="message"/> above the footer in the color of <paramref name="tone"/>. See
    ///     <see cref="Message(IReadOnlyValue{string?})"/>.
    /// </summary>
    /// <param name="message">A sentence or two.</param>
    /// <param name="tone">Its color; <see cref="Tone.Neutral"/> is the usual text color.</param>
    public PopupBuilder Message(IReadOnlyValue<string?> message, IReadOnlyValue<Tone> tone)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(tone);

        _message = message;
        _messageTone = tone;
        return this;
    }

    /// <summary>
    ///     Puts cards side by side, each as wide as the others, such as a short box and a switch that would each take a
    ///     full row otherwise. A card that's hidden, as with <see cref="CardBuilder{TSelf}.VisibleWhen"/>, leaves its width
    ///     to the others. Keep it to cards with little in them: a popup is narrow.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Row(row => row
    ///         .TextBox("Time to log", duration, text => text.Placeholder("1:30"))
    ///         .Toggle("Billable", billable))
    ///     </code>
    /// </example>
    /// <param name="build">Adds the cards, from left to right; two or more.</param>
    public PopupBuilder Row(Action<RowBuilder> build)
    {
        ArgumentNullException.ThrowIfNull(build);

        var builder = new RowBuilder();
        build(builder);

        _elements.Add(builder.Build());
        return this;
    }

    /// <summary>
    ///     Adds a button to the footer, right of the ones added before. While it runs, the popup stays open when it loses
    ///     the focus, and its commands are disabled. If it throws, the error shows above the footer instead of as a
    ///     notification, and the popup stays open.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Command("Start", StartAsync, command => command
    ///         .Default()
    ///         .Closes()
    ///         .VisibleWhen(isRunning.Not()))
    ///     </code>
    /// </example>
    /// <param name="label">The button's text, such as <c>Start</c>.</param>
    /// <param name="run">What the button does. Boxes in the popup have their value set before it runs.</param>
    /// <param name="build">Sets its key, whether it closes the popup, and when it's shown and enabled.</param>
    public PopupBuilder Command(string label, PluginAction run, Action<CommandBuilder>? build = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);
        ArgumentNullException.ThrowIfNull(run);

        var builder = new CommandBuilder();
        build?.Invoke(builder);

        _commands.Add(builder.Build(label, run));
        return this;
    }

    /// <summary>
    ///     Adds a button to the footer whose action is done right away. See
    ///     <see cref="Command(string, PluginAction, Action{CommandBuilder}?)"/>.
    /// </summary>
    /// <param name="label">The button's text, such as <c>Cancel</c>.</param>
    /// <param name="run">What the button does.</param>
    /// <param name="build">Sets its key, whether it closes the popup, and when it's shown and enabled.</param>
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

    /// <summary>
    ///     Describes a popup titled <paramref name="title"/> with <paramref name="build"/>.
    /// </summary>
    internal static PopupModel Build(string title, Action<PopupBuilder> build)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentNullException.ThrowIfNull(build);

        var builder = new PopupBuilder();
        build(builder);

        return new PopupModel(title, builder._elements.ToArray(), builder._commands.ToArray(), builder._message, builder._messageTone);
    }
}
