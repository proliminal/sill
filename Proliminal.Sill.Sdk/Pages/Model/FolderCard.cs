using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     A card that shows a folder under its description, with a button that picks another one.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class FolderCard : CardElement
{
    internal FolderCard(CardInfo info, IValue<string> value, string dialogTitle)
        : base(info)
    {
        Value = value;
        DialogTitle = dialogTitle;
    }

    /// <summary>The folder's path. Environment variables in it, such as <c>%USERPROFILE%</c>, are kept as they are.</summary>
    public IValue<string> Value { get; }

    /// <summary>The title of the window that picks a folder.</summary>
    public string DialogTitle { get; }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
