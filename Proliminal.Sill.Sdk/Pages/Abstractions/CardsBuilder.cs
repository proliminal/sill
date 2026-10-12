using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Abstractions;

/// <summary>
///     Adds cards, from top to bottom: to a page with <see cref="PageBuilder"/>, or under a header with
///     <see cref="GroupBuilder"/>. Each card takes what it can't do without as arguments, and everything else, such as
///     its icon and description, through its builder.
/// </summary>
/// <typeparam name="TSelf">The builder itself, so calls can be chained.</typeparam>
public abstract class CardsBuilder<TSelf>
    where TSelf : CardsBuilder<TSelf>
{
    private protected CardsBuilder()
    {
    }

    private TSelf Self
        => (TSelf)this;

    /// <summary>
    ///     Adds a card with a button.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Button("Empty Recycle Bin", "Empty", EmptyAsync, button => button
    ///         .Icon(Glyph.Delete)
    ///         .Description("Permanently deletes everything in it."))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title.</param>
    /// <param name="label">The button's text, such as <c>Open</c>.</param>
    /// <param name="onClick">What the button does. The button is disabled until its task is done.</param>
    /// <param name="build">Sets the card's icon, description and conditions.</param>
    public TSelf Button(string title, string label, PluginAction onClick, Action<ButtonBuilder>? build = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(label);

        return Button(title, new ConstantValue<string>(label), onClick, build);
    }

    /// <summary>
    ///     Adds a card with a button whose text changes with <paramref name="label"/>, such as from "Check for updates" to
    ///     "Update". See <see cref="Button(string, string, PluginAction, Action{ButtonBuilder}?)"/>.
    /// </summary>
    /// <param name="title">The card's title.</param>
    /// <param name="label">The button's text.</param>
    /// <param name="onClick">What the button does. The button is disabled until its task is done.</param>
    /// <param name="build">Sets the card's icon, description and conditions.</param>
    public TSelf Button(string title, IReadOnlyValue<string> label, PluginAction onClick, Action<ButtonBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(label);
        ArgumentNullException.ThrowIfNull(onClick);

        var builder = new ButtonBuilder(title);
        build?.Invoke(builder);

        Add(new ButtonCard(builder.Info, label, onClick));
        return Self;
    }

    /// <summary>
    ///     Adds a card with a button whose action is done right away.
    ///     See <see cref="Button(string, string, PluginAction, Action{ButtonBuilder}?)"/>.
    /// </summary>
    /// <param name="title">The card's title.</param>
    /// <param name="label">The button's text, such as <c>Open</c>.</param>
    /// <param name="onClick">What the button does.</param>
    /// <param name="build">Sets the card's icon, description and conditions.</param>
    public TSelf Button(string title, string label, Action<IActionContext> onClick, Action<ButtonBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(onClick);

        return Button(title, label, context =>
        {
            onClick(context);
            return Task.CompletedTask;
        }, build);
    }

    /// <summary>
    ///     Adds a card with an on/off switch.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Toggle("Hide from desktop", hiddenFromDesktop, toggle => toggle
    ///         .Icon(Glyph.Monitor)
    ///         .Description("Deleted files still go to it."))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title.</param>
    /// <param name="value">Whether it's on.</param>
    /// <param name="build">Sets the card's icon, description and conditions.</param>
    public TSelf Toggle(string title, IValue<bool> value, Action<ToggleBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        var builder = new ToggleBuilder(title);
        build?.Invoke(builder);

        Add(new ToggleCard(builder.Info, value));
        return Self;
    }

    /// <summary>
    ///     Adds a card with a slider from <paramref name="minimum"/> to <paramref name="maximum"/>. The value changes
    ///     when the slider is let go, not while it's dragged.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Slider("Opacity", opacity, 0, 1, slider => slider
    ///         .Step(0.05)
    ///         .Format(value => $"{value:P0}"))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title.</param>
    /// <param name="value">The number the slider is at.</param>
    /// <param name="minimum">The lowest number.</param>
    /// <param name="maximum">The highest number.</param>
    /// <param name="build">Sets the card's icon, description, conditions, steps and how the number is shown.</param>
    public TSelf Slider(string title, IValue<double> value, double minimum, double maximum, Action<SliderBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        var builder = new SliderBuilder(title, minimum, maximum, step: 0, format: number => number.ToString("0.##"));
        build?.Invoke(builder);

        Add(builder.Build(value));
        return Self;
    }

    /// <summary>
    ///     Adds a card with a slider over whole numbers, which snaps to every number unless
    ///     <see cref="SliderBuilder.Step"/> says otherwise.
    ///     See <see cref="Slider(string, IValue{double}, double, double, Action{SliderBuilder}?)"/>.
    /// </summary>
    /// <param name="title">The card's title.</param>
    /// <param name="value">The number the slider is at.</param>
    /// <param name="minimum">The lowest number.</param>
    /// <param name="maximum">The highest number.</param>
    /// <param name="build">Sets the card's icon, description, conditions, steps and how the number is shown.</param>
    public TSelf Slider(string title, IValue<int> value, int minimum, int maximum, Action<SliderBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        var builder = new SliderBuilder(title, minimum, maximum, step: 1, format: number => number.ToString("0"));
        build?.Invoke(builder);

        Add(builder.Build(value.Convert(number => (double)number, number => (int)Math.Round(number))));
        return Self;
    }

    /// <summary>
    ///     Adds a card with a dropdown, whose options are added with <see cref="PickerBuilder{T}.Option"/>.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Picker("Save images to", saveLocation, picker => picker
    ///         .Icon(Glyph.Save)
    ///         .Option(SaveLocation.Temp, "Temp folder")
    ///         .Option(SaveLocation.Custom, "Custom folder"))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title.</param>
    /// <param name="value">The chosen option's value.</param>
    /// <param name="build">Adds the options and sets the card's icon, description and conditions.</param>
    public TSelf Picker<T>(string title, IValue<T> value, Action<PickerBuilder<T>> build)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(build);

        var builder = new PickerBuilder<T>(title);
        build(builder);

        Add(builder.Build(value));
        return Self;
    }

    /// <summary>
    ///     Adds a card with a color picker, which offers dark presets, a typed-in <c>#RRGGBB</c> color and "Default".
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Color("Window color", windowColor, color => color
    ///         .Icon(Glyph.Color)
    ///         .Description("The background behind the text. Default uses the color scheme's."))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title.</param>
    /// <param name="value">The color as <c>#RRGGBB</c>, or <see langword="null"/> for the default.</param>
    /// <param name="build">Sets the card's icon, description and conditions.</param>
    public TSelf Color(string title, IValue<string?> value, Action<ColorBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        var builder = new ColorBuilder(title);
        build?.Invoke(builder);

        Add(new ColorCard(builder.Info, value));
        return Self;
    }

    /// <summary>
    ///     Adds a card that shows a folder under its description, with a Browse button that picks another one.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Folder("Custom folder", customFolder, folder => folder
    ///         .Icon(Glyph.Folder)
    ///         .Description("Used when saving images to a custom folder.")
    ///         .DialogTitle("Save images to"))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title.</param>
    /// <param name="value">The folder's path.</param>
    /// <param name="build">Sets the card's icon, description, conditions and the picking window's title.</param>
    public TSelf Folder(string title, IValue<string> value, Action<FolderBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        var builder = new FolderBuilder(title);
        build?.Invoke(builder);

        Add(builder.Build(value));
        return Self;
    }

    /// <summary>
    ///     Adds a card without a control of its own, which shows its status and extra buttons, if any.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Info("PowerShell 7", info => info
    ///         .Icon(Glyph.Code)
    ///         .Status(isInstalled.Map(installed => installed ? "Installed" : null))
    ///         .Action("Install", InstallAsync, action => action.VisibleWhen(isInstalled.Not())))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title.</param>
    /// <param name="build">Sets the card's icon, description, status, buttons and conditions.</param>
    public TSelf Info(string title, Action<InfoBuilder>? build = null)
    {
        var builder = new InfoBuilder(title);
        build?.Invoke(builder);

        Add(new InfoCard(builder.Info));
        return Self;
    }

    /// <summary>
    ///     Adds a large card with a score as a ring that fills and colors from red to green, as Windows Settings shows
    ///     its own, with the recommendations that raise it. Usually the first thing on a page, or a category's header.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .Score("Setup score", score, card => card
    ///         .Status(score.Map(value => value >= 0.6 ? "Good" : "Fair"))
    ///         .Description("How much of the recommended setup is done.")
    ///         .Recommendations(open)
    ///         .ApplyAll("Apply all", ApplyAllAsync))
    ///     </code>
    /// </example>
    /// <param name="title">The card's title, such as <c>Privacy score</c>.</param>
    /// <param name="value">The score, from 0 to 1.</param>
    /// <param name="build">Sets the card's grade, description, colors, recommendations and buttons.</param>
    public TSelf Score(string title, IReadOnlyValue<double> value, Action<ScoreBuilder>? build = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        var builder = new ScoreBuilder(title);
        build?.Invoke(builder);

        Add(builder.Build(value));
        return Self;
    }

    /// <summary>
    ///     Adds a compact card per item of <paramref name="items"/>, which follow it as it changes, such as the folders
    ///     on <c>PATH</c>. Each row can have small icon buttons that act on its item.
    /// </summary>
    /// <example>
    ///     <code>
    ///     .List(entries, entry => entry.Value, list => list
    ///         .Icon(entry => entry.Exists ? Glyph.Folder : Glyph.Warning)
    ///         .Description(entry => entry.Exists ? null : "Folder not found")
    ///         .Action(Glyph.Delete, "Remove from PATH", (entry, _) => RemoveAsync(entry)))
    ///     </code>
    /// </example>
    /// <param name="items">The items, in order.</param>
    /// <param name="title">The title of an item's row.</param>
    /// <param name="build">Sets the rows' descriptions, icons and buttons.</param>
    public TSelf List<T>(IReadOnlyValue<IReadOnlyList<T>> items, Func<T, string> title, Action<ListBuilder<T>>? build = null)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(title);

        var builder = new ListBuilder<T>();
        build?.Invoke(builder);

        Add(builder.Build(items, title));
        return Self;
    }

    private protected abstract void Add(PageElement element);
}
