using System.ComponentModel;
using Proliminal.Sill.Sdk.Abstractions;
using Proliminal.Sill.Sdk.Pages.Model.Abstractions;

namespace Proliminal.Sill.Sdk.Pages.Model;

/// <summary>
///     Represents a card that shows a folder under its description, with a button to pick another one.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class FolderCard : CardElement
{
    /// <summary>
    ///     Gets the folder's path. Environment variables in it, such as <c>%USERPROFILE%</c>, are kept unexpanded.
    /// </summary>
    public IValue<string> Value { get; }

    /// <summary>
    ///     Gets the title of the folder picker dialog.
    /// </summary>
    public string DialogTitle { get; }

    internal FolderCard(CardInfo info, IValue<string> value, string dialogTitle)
        : base(info)
    {
        Value = value;
        DialogTitle = dialogTitle;
    }

    internal override IEnumerable<IReadOnlyValue> GetValues()
        => base.GetValues().Append(Value);
}
