using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages;

/// <summary>
///     Represents a builder for the cards under a header.
///     See <see cref="PageBuilder.Group"/>.
/// </summary>
public sealed class GroupBuilder : CardsBuilder<GroupBuilder>
{
    private readonly List<PageElement> _elements = [];
    private IReadOnlyValue<bool>? _visibleWhen;
    private IReadOnlyValue<bool>? _enabledWhen;

    internal GroupBuilder()
    {
    }

    /// <summary>
    ///     Shows the header and its cards only while <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="condition">When the group is shown.</param>
    public GroupBuilder VisibleWhen(IReadOnlyValue<bool> condition)
    {
        _visibleWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    /// <summary>
    ///     Enables the controls of the cards only while <paramref name="condition"/> is <see langword="true"/>, such as while the app they change is installed.
    /// </summary>
    /// <param name="condition">When the cards are enabled.</param>
    public GroupBuilder EnabledWhen(IReadOnlyValue<bool> condition)
    {
        _enabledWhen = condition ?? throw new ArgumentNullException(nameof(condition));
        return this;
    }

    private protected override void Add(PageElement element)
        => _elements.Add(element);

    internal GroupElement Build(string header)
    {
        if (_elements.Count == 0)
            throw new InvalidOperationException($"The group \"{header}\" has no cards.");

        return new GroupElement(header, _elements.ToArray(), _visibleWhen, _enabledWhen);
    }
}
