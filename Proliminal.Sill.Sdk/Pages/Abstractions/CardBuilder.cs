using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;

namespace Proliminal.Sill.Sdk.Pages.Abstractions;

/// <summary>
///     Sets what every card has besides its title: an icon, a description and when it's shown and enabled.
/// </summary>
/// <typeparam name="TSelf">The builder itself, so calls can be chained.</typeparam>
public abstract class CardBuilder<TSelf>
    where TSelf : CardBuilder<TSelf>
{
    private readonly string _title;
    private IReadOnlyValue<string>? _description;
    private Glyph? _icon;
    private IReadOnlyValue<bool>? _visibleWhen;
    private IReadOnlyValue<bool>? _enabledWhen;
    private IReadOnlyValue<string?>? _status;
    private IReadOnlyValue<Tone>? _statusTone;
    private readonly List<CardAction> _actions = [];
    private IReadOnlyValue<IReadOnlyList<CardDetail>>? _details;
    private readonly List<string> _keywords = [];

    private protected CardBuilder(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        _title = title;
    }

    private protected TSelf Self
        => (TSelf)this;

    internal CardInfo Info
        => new(_title, _description, _icon, _visibleWhen, _enabledWhen, _status, _statusTone, _actions.ToArray(), _details, _keywords.ToArray());

    /// <summary>
    ///     Shows <paramref name="icon"/> left of the title. Cards without one leave the space out.
    /// </summary>
    /// <param name="icon">The icon.</param>
    public TSelf Icon(Glyph icon)
    {
        if (icon.Text is null)
            throw new ArgumentException("Use one of Glyph's icons, or Glyph.FromCodepoint.", nameof(icon));

        _icon = icon;
        return Self;
    }

    /// <summary>
    ///     Shows <paramref name="description"/> under the title, saying what the card does.
    /// </summary>
    /// <param name="description">One or two sentences.</param>
    public TSelf Description(string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        _description = new ConstantValue<string>(description);
        return Self;
    }

    /// <summary>
    ///     Shows <paramref name="description"/> under the title, which changes with it, such as whether what the card
    ///     opens is installed.
    /// </summary>
    /// <param name="description">One or two sentences.</param>
    public TSelf Description(IReadOnlyValue<string> description)
    {
        _description = description ?? throw new ArgumentNullException(nameof(description));
        return Self;
    }

    /// <summary>
    ///     Shows <paramref name="status"/> left of the control, such as <c>Installed</c> or <c>Installing…</c>. It's hidden
    ///     while it's <see langword="null"/> or empty.
    /// </summary>
    /// <param name="status">A few words, such as what's wrong with what was typed.</param>
    public TSelf Status(IReadOnlyValue<string?> status)
    {
        _status = status ?? throw new ArgumentNullException(nameof(status));
        return Self;
    }

    /// <summary>
    ///     Shows <paramref name="status"/> left of the control in the color of <paramref name="tone"/>, such as a green
    ///     <c>Matches</c> or a red <c>Doesn't match</c>. The words should say it too, as not everyone tells colors apart.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Status(match.Map(result => result.Text), match.Map(result => result.IsRight ? Tone.Good : Tone.Critical))
    ///     </code>
    /// </example>
    /// <param name="status">A few words, such as what's wrong with what was typed.</param>
    /// <param name="tone">Its color; <see cref="Tone.Neutral"/> is the usual status color.</param>
    public TSelf Status(IReadOnlyValue<string?> status, IReadOnlyValue<Tone> tone)
    {
        ArgumentNullException.ThrowIfNull(status);
        ArgumentNullException.ThrowIfNull(tone);

        _status = status;
        _statusTone = tone;
        return Self;
    }

    /// <summary>
    ///     Shows <paramref name="details"/> as lines inside the card, under its description, such as what a plugin uses.
    ///     For a few lines that belong to this card; a list of things of their own is a
    ///     <see cref="CardsBuilder{TSelf}.List"/>. Nothing shows while it's empty.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Details(Values.External&lt;IReadOnlyList&lt;CardDetail&gt;&gt;(() =>
    ///         [new("Reads the clipboard", "To decode what you copy.", Glyph.Paste)]))
    ///     </code>
    /// </example>
    /// <param name="details">The lines, from top to bottom.</param>
    public TSelf Details(IReadOnlyValue<IReadOnlyList<CardDetail>> details)
    {
        _details = details ?? throw new ArgumentNullException(nameof(details));
        return Self;
    }

    /// <summary>
    ///     Adds a button left of the control, after the ones added before, such as Install next to a switch for
    ///     something that's missing.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Action("Install", InstallAsync, action => action
    ///         .VisibleWhen(isInstalled.Not()))
    ///     </code>
    /// </example>
    /// <param name="label">The button's text.</param>
    /// <param name="onClick">What the button does. The button is disabled until its task is done.</param>
    /// <param name="build">Sets when the button is shown and enabled.</param>
    public TSelf Action(string label, PluginAction onClick, Action<ActionBuilder>? build = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);
        ArgumentNullException.ThrowIfNull(onClick);

        var builder = new ActionBuilder();
        build?.Invoke(builder);

        _actions.Add(builder.Build(label, onClick));
        return Self;
    }

    /// <summary>
    ///     Adds a button whose action is done right away. See <see cref="Action(string, PluginAction, Action{ActionBuilder}?)"/>.
    /// </summary>
    /// <param name="label">The button's text.</param>
    /// <param name="onClick">What the button does.</param>
    /// <param name="build">Sets when the button is shown and enabled.</param>
    public TSelf Action(string label, Action<IActionContext> onClick, Action<ActionBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(onClick);

        return Action(label, context =>
        {
            onClick(context);
            return Task.CompletedTask;
        }, build);
    }

    /// <summary>
    ///     Adds words people may search Sill for to find this card, besides its title and description, such as other
    ///     names for what it does. The card's page opens with the card in view.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Button("Empty Recycle Bin", "Empty", EmptyAsync, button => button
    ///         .Keywords("trash", "delete files"))
    ///     </code>
    /// </example>
    /// <param name="keywords">A word or a few, each; case doesn't matter.</param>
    public TSelf Keywords(params string[] keywords)
    {
        Keyword.AddTo(_keywords, keywords);
        return Self;
    }

    /// <summary>
    ///     Only shows the card while <paramref name="condition"/> is <see langword="true"/>, such as the folder of a
    ///     "custom folder" option.
    /// </summary>
    /// <param name="condition">When the card is shown.</param>
    public TSelf VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return Self;
    }

    /// <summary>
    ///     Only enables the card's control while <paramref name="condition"/> is <see langword="true"/>. A bound value
    ///     disables it too while it can't be changed.
    /// </summary>
    /// <param name="condition">When the control is enabled.</param>
    public TSelf EnabledWhen(IReadOnlyValue<bool> condition)
    {
        _enabledWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return Self;
    }
}
