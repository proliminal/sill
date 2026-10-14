using System.Globalization;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     An icon from Segoe Fluent Icons, the icon font Windows 11 and its Settings app use. Windows 10 falls back to Segoe
///     MDL2 Assets, which has the same code points. The well-known icons are properties; any other is
///     <see cref="FromCodepoint"/>.
/// </summary>
public readonly record struct Glyph
{
    private Glyph(string text)
        => Text = text;

    /// <summary>
    ///     The character that draws the icon in the icon font.
    /// </summary>
    public string Text { get; }

    /// <summary>
    ///     An icon by its code point, as listed in Microsoft's Segoe Fluent Icons reference, such as <c>0xE713</c>.
    /// </summary>
    /// <param name="codepoint">The code point, from the Private Use Area the font uses.</param>
    public static Glyph FromCodepoint(int codepoint)
    {
        if (codepoint is < 0xE000 or > 0xF8FF)
            throw new ArgumentOutOfRangeException(nameof(codepoint), $"0x{codepoint:X4} isn't an icon; Segoe Fluent Icons uses 0xE000 to 0xF8FF.");

        return new(char.ConvertFromUtf32(codepoint));
    }

    /// <inheritdoc />
    public override string ToString()
        => Text is null ? "" : $"U+{char.ConvertToUtf32(Text, 0).ToString("X4", CultureInfo.InvariantCulture)}";

    /// <summary>A gear, for settings.</summary>
    public static Glyph Settings { get; } = FromCodepoint(0xE713);

    /// <summary>A brush on a frame, for how something looks.</summary>
    public static Glyph Personalize { get; } = FromCodepoint(0xE771);

    /// <summary>A palette, for colors.</summary>
    public static Glyph Color { get; } = FromCodepoint(0xE790);

    /// <summary>A crescent moon, for dark mode.</summary>
    public static Glyph Moon { get; } = FromCodepoint(0xE708);

    /// <summary>Stacked sheets, for transparency and layers.</summary>
    public static Glyph Layers { get; } = FromCodepoint(0xE81E);

    /// <summary>A grid of tiles, for Start and app layouts.</summary>
    public static Glyph Tiles { get; } = FromCodepoint(0xECA5);

    /// <summary>A keyboard, for typing and shortcuts.</summary>
    public static Glyph Keyboard { get; } = FromCodepoint(0xE765);

    /// <summary>A monitor, for the screen and desktop.</summary>
    public static Glyph Monitor { get; } = FromCodepoint(0xE7F4);

    /// <summary>A landscape picture, for images.</summary>
    public static Glyph Picture { get; } = FromCodepoint(0xE8B9);

    /// <summary>Two sheets, for copying.</summary>
    public static Glyph Copy { get; } = FromCodepoint(0xE8C8);

    /// <summary>A clipboard, for pasting.</summary>
    public static Glyph Paste { get; } = FromCodepoint(0xE77F);

    /// <summary>A floppy disk, for saving.</summary>
    public static Glyph Save { get; } = FromCodepoint(0xE74E);

    /// <summary>A closed folder.</summary>
    public static Glyph Folder { get; } = FromCodepoint(0xE8B7);

    /// <summary>An open folder, for browsing to or opening a folder.</summary>
    public static Glyph FolderOpen { get; } = FromCodepoint(0xE838);

    /// <summary>A document with an arrow, for opening a file.</summary>
    public static Glyph OpenFile { get; } = FromCodepoint(0xE8E5);

    /// <summary>A window with an arrow, for opening something in its own window.</summary>
    public static Glyph OpenInNewWindow { get; } = FromCodepoint(0xE8A7);

    /// <summary>A sheet of paper, for documents and files.</summary>
    public static Glyph Document { get; } = FromCodepoint(0xE8A5);

    /// <summary>A trash can, for deleting.</summary>
    public static Glyph Delete { get; } = FromCodepoint(0xE74D);

    /// <summary>A plus sign, for adding.</summary>
    public static Glyph Add { get; } = FromCodepoint(0xE710);

    /// <summary>A triangle with an exclamation mark, for warnings.</summary>
    public static Glyph Warning { get; } = FromCodepoint(0xE7BA);

    /// <summary>A command prompt, for terminals and shells.</summary>
    public static Glyph CommandPrompt { get; } = FromCodepoint(0xE756);

    /// <summary>Braces, for code and developer tools.</summary>
    public static Glyph Code { get; } = FromCodepoint(0xE943);

    /// <summary>A clock with an arrow back, for history.</summary>
    public static Glyph History { get; } = FromCodepoint(0xE81C);

    /// <summary>A chevron pointing right, for prompts and going further.</summary>
    public static Glyph ChevronRight { get; } = FromCodepoint(0xE76C);

    /// <summary>Two letters A, for fonts and text.</summary>
    public static Glyph Font { get; } = FromCodepoint(0xE8D2);

    /// <summary>A power button, for starting and turning on.</summary>
    public static Glyph Power { get; } = FromCodepoint(0xE7E8);

    /// <summary>A circled i, for information.</summary>
    public static Glyph Info { get; } = FromCodepoint(0xE946);

    /// <summary>A heartbeat line, for status and activity.</summary>
    public static Glyph Pulse { get; } = FromCodepoint(0xE9D9);

    /// <summary>Two circling arrows, for syncing and updates.</summary>
    public static Glyph Sync { get; } = FromCodepoint(0xE895);

    /// <summary>A speaker, for sound and volume.</summary>
    public static Glyph Volume { get; } = FromCodepoint(0xE767);

    /// <summary>A sun, for brightness.</summary>
    public static Glyph Brightness { get; } = FromCodepoint(0xE706);

    /// <summary>A speech bubble, for messages and notifications.</summary>
    public static Glyph Message { get; } = FromCodepoint(0xE8BD);

    /// <summary>A triangle pointing right, for playing and running.</summary>
    public static Glyph Play { get; } = FromCodepoint(0xE768);

    /// <summary>A clock, for time and schedules.</summary>
    public static Glyph Clock { get; } = FromCodepoint(0xE823);

    /// <summary>A globe, for the internet and languages.</summary>
    public static Glyph Globe { get; } = FromCodepoint(0xE774);

    /// <summary>Two chain links, for links.</summary>
    public static Glyph Link { get; } = FromCodepoint(0xE71B);

    /// <summary>A magnifying glass, for searching.</summary>
    public static Glyph Search { get; } = FromCodepoint(0xE721);

    /// <summary>A pencil, for editing.</summary>
    public static Glyph Edit { get; } = FromCodepoint(0xE70F);

    /// <summary>A circular arrow, for refreshing and reloading.</summary>
    public static Glyph Refresh { get; } = FromCodepoint(0xE72C);

    /// <summary>An arrow pointing down to a line, for downloading and exporting.</summary>
    public static Glyph Download { get; } = FromCodepoint(0xE896);

    /// <summary>A puzzle piece, for plugins and extensions.</summary>
    public static Glyph Puzzle { get; } = FromCodepoint(0xEA86);

    /// <summary>A shield, for privacy and protection.</summary>
    public static Glyph Shield { get; } = FromCodepoint(0xEA18);

    /// <summary>A padlock, for what's locked or managed by someone else.</summary>
    public static Glyph Lock { get; } = FromCodepoint(0xE72E);

    /// <summary>A camera, for the webcam.</summary>
    public static Glyph Camera { get; } = FromCodepoint(0xE722);

    /// <summary>A microphone.</summary>
    public static Glyph Microphone { get; } = FromCodepoint(0xE720);

    /// <summary>A map pin, for location.</summary>
    public static Glyph MapPin { get; } = FromCodepoint(0xE707);

    /// <summary>A price tag, for advertising and labels.</summary>
    public static Glyph Tag { get; } = FromCodepoint(0xE8EC);

    /// <summary>A megaphone, for promotions and announcements.</summary>
    public static Glyph Megaphone { get; } = FromCodepoint(0xE789);

    /// <summary>A light bulb, for tips and suggestions.</summary>
    public static Glyph Lightbulb { get; } = FromCodepoint(0xEA80);

    /// <summary>An eye, for what's seen or watched.</summary>
    public static Glyph View { get; } = FromCodepoint(0xE890);

    /// <summary>A bell, for alerts.</summary>
    public static Glyph Bell { get; } = FromCodepoint(0xEA8F);

    /// <summary>A briefcase, for work and organizations.</summary>
    public static Glyph Work { get; } = FromCodepoint(0xE821);

    /// <summary>A certificate, for trust and certificates.</summary>
    public static Glyph Certificate { get; } = FromCodepoint(0xEB95);

    /// <summary>A person, for accounts and people.</summary>
    public static Glyph Person { get; } = FromCodepoint(0xE77B);

    /// <summary>A person with a shield, for administrators and what they manage.</summary>
    public static Glyph Admin { get; } = FromCodepoint(0xE7EF);

    /// <summary>An arrow curving back, for undoing.</summary>
    public static Glyph Undo { get; } = FromCodepoint(0xE7A7);

    /// <summary>A shield with a stripe, Windows Security's own, for security and antivirus.</summary>
    public static Glyph Defender { get; } = FromCodepoint(0xE83D);

    /// <summary>A laptop with a shield, for the protection built into the PC itself.</summary>
    public static Glyph DeviceSecurity { get; } = FromCodepoint(0xF552);

    /// <summary>A bug, for threats and malware.</summary>
    public static Glyph Bug { get; } = FromCodepoint(0xEBE8);

    /// <summary>A hard drive, for disks and drives.</summary>
    public static Glyph HardDrive { get; } = FromCodepoint(0xEDA2);

    /// <summary>A network, for firewalls and connections.</summary>
    public static Glyph Network { get; } = FromCodepoint(0xE968);

    /// <summary>A fingerprint, for signing in.</summary>
    public static Glyph Fingerprint { get; } = FromCodepoint(0xE928);

    /// <summary>A key, for signatures, passwords and credentials.</summary>
    public static Glyph Key { get; } = FromCodepoint(0xE8D7);

    /// <summary>An open padlock, for what's unprotected.</summary>
    public static Glyph Unlock { get; } = FromCodepoint(0xE785);

    /// <summary>A lightning bolt, for what starts by itself.</summary>
    public static Glyph Lightning { get; } = FromCodepoint(0xE945);

    /// <summary>A check mark in a circle, for what's fine.</summary>
    public static Glyph Completed { get; } = FromCodepoint(0xE930);

    /// <summary>An exclamation mark in a circle, for what needs attention.</summary>
    public static Glyph Error { get; } = FromCodepoint(0xE783);

    /// <summary>A path, for file paths and locations.</summary>
    public static Glyph Path { get; } = FromCodepoint(0xE7C3);

    /// <summary>Two tools vertically aligned side by side.</summary>
    public static Glyph DeveloperTools { get; } = FromCodepoint(0xEC7A);
}
