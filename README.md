# Proliminal Sill

A small Windows tray app with a handful of keyboard shortcuts for everyday tasks: typing morse code, copying image
paths to the clipboard, jumping to Windows Terminal, and switching Windows to an all-black look.

## Getting started

1. Download `sill.exe` from the latest release on the [Releases](https://github.com/proliminal/sill/releases) page.
2. Run it and choose *Install*.

Sill installs itself for your account in `%LOCALAPPDATA%\Programs\Proliminal\Sill`, without administrator rights,
and adds a shortcut to the Start menu. *Start Sill when I sign in* turns on *Start with Windows* right away. The
downloaded file isn't needed afterwards. If you choose *Run without installing*, Sill runs from where it is and asks
again the next time.

Sill runs on 64-bit Windows 10 and 11. Nothing needs to be installed first; everything it needs is inside `sill.exe`.

Sill lives in the notification area (the system tray). Click its icon to open the quick menu, with buttons for copying an
image path, opening Terminal and opening the Recycle Bin, and a gear that opens the settings window, which has a page for
each feature. Right-click the icon for *Exit*. To have Sill start when you sign in, turn on *Start with Windows* on the
*General* page.

> **Windows SmartScreen:** `sill.exe` isn't code-signed (YET), so Windows may warn you the first time you run it.
> Choose *More info* → *Run anyway*.

## Features

### Image path

Press **Ctrl+Shift+Alt+P** (or *Copy now* on the *Image path* page) to put a path on the clipboard, for example to paste a
screenshot into a tool that only accepts file paths. What gets copied depends on what's on the clipboard:

1. **A file copied in Explorer:** its own path.
2. **An image Snipping Tool just saved:** the path of that screenshot in your Screenshots folder.
3. **Any other image:** it's saved as a PNG and that path is copied.

A Windows notification shows what was copied.

### Windows Terminal

Press **Ctrl+Shift+Alt+T** (or *Open* on the *Windows Terminal* page) to bring Windows Terminal to the front. The
window you used last is restored and focused; if Terminal isn't open, it's started.

The page also sets what Terminal's own settings don't make easy. Terminal applies them right away:

- **Opacity**: how much of the desktop shows through, for every profile.
- **Window color**: the background behind the text, for every profile. *Default* uses the color scheme's.
- **Title bar color**: the title bar, tab row and frame, as a `Proliminal Sill` theme in Terminal's settings.
  *Default* goes back to Terminal's own theme.

A profile with its own opacity or background keeps it.

### PowerShell

The *PowerShell* page sets up [PowerShell 7](https://github.com/PowerShell/PowerShell). If it isn't installed, *Install*
gets it with winget. Each part of the profile can be turned on and off on its own:

- **Autocomplete and history**: suggestions from your history as you type, history search on the arrow keys, and a menu
  on Tab.
- **File icons**: icons in file listings, from [Terminal-Icons](https://github.com/devblackops/Terminal-Icons).
- **Prompt**: the [oh-my-posh](https://ohmyposh.dev) `atomic` prompt. It looks best with a
  [Nerd Font](https://www.nerdfonts.com).

File icons and the prompt also have an *Install* button when they're missing. Sill adds its lines in a marked block in
your profile. Turning a part off removes its lines wherever they are, also ones you wrote yourself, and leaves the rest
of the profile alone. Open a new PowerShell tab to see changes.

*Font* sets the font of the PowerShell profile in Terminal; other profiles keep theirs.

When Visual Studio 2026 is installed, *Developer PowerShell 7* adds a terminal profile to it that opens PowerShell 7
with the developer tools (through `Launch-VsDevShell.ps1`) and makes it the default. Turning it off removes the profile,
so Visual Studio's own Developer PowerShell is the default again. It runs PowerShell 7 with the same profile as
everywhere else, so the profile settings above (autocomplete, file icons, the prompt) apply in Visual Studio too. The change applies the next time Visual Studio starts;
terminal tabs it had open come back with the shell they had, so a new tab shows the new default.

### Recycle Bin

On the *Recycle Bin* page, *Open* shows it even when it isn't on the desktop, and *Empty* empties it after Windows asks
you to confirm. *Hide from desktop* removes its desktop icon, which Windows doesn't let you delete like other icons;
deleted files still go to the Recycle Bin.

### Windows Appearance

The *Windows Appearance* page has a switch for each part of how Windows looks:

- **Dark mode**: dark apps, menus and taskbar.
- **All black**: a black taskbar, Start, title bars and window borders. Your accent color for toggles and highlights
  isn't changed. If title bars don't change, sign out and back in.
- **Transparency effects**: the desktop showing through Start, the taskbar and some windows.
- **Compact taskbar and Start**: a compact, left-aligned taskbar without search, Task View, widgets or chat, and a
  compact Start that shows only all apps, without pinned, recommended or most used apps.
- **Hide keyboard layout button**: no language button on the taskbar; **Win+Space** still switches layouts.

*All black* and *Compact taskbar and Start* save the settings they change and put them back when you turn them off.
Without a saved copy, as when you set them up by hand, turning them off goes back to a grey accent and to Windows'
default taskbar and Start. Changing the accent color in Windows Settings turns *All black* off; turn it on again to
reapply.

The compact sizes and Start's sections are Windows 26H2 features that are still being rolled out. They're only changed
when Windows has them (feature IDs `59213768` and `61090762` for the taskbar, `61754985` for Start). If Windows doesn't
allow changing a setting, such as widgets, Sill says which one so you can change it in Settings.

Switches that change the taskbar restart it, which also closes open File Explorer windows.

## Updates

Sill checks for a new release a minute after it starts and every 6 hours after that. When there is one, a notification
says so; click it to update. Sill downloads the new `sill.exe`, checks it against the release, swaps it in and restarts.
The *Updates* group on the *General* page shows the version and has a button to check or update right away, and a switch
to turn the automatic checks off (`Updates.CheckAutomatically` in the settings file).

Local builds (version `0.0.0`) aren't updated and don't offer to install themselves.

## Settings

Most options are on the feature pages. To edit the file itself, choose *Open* next to *Settings file* on the *General*
page; it's at `%APPDATA%\Proliminal\Sill\settings.json`. Every option is listed with its default the first time Sill
runs, and changes apply as soon as you save the file.

| `ImagePath` option | Values                                                                                              |
| ------------------ | --------------------------------------------------------------------------------------------------- |
| `SaveLocation`     | `Temp` (`%TEMP%\Proliminal\Sill`, default) or `Custom`                                              |
| `CustomFolder`     | Folder used by `Custom`; defaults to `Screenshots\Pathcopies`. Environment variables work.          |
| `ClipboardMode`    | `Override`: the path replaces the clipboard (default). `Join`: the path is added next to the image. |

All three can also be changed on the *Image path* page.

### From the command line

Settings can also be changed from a terminal. Keys are written as `Section.Option` and aren't case-sensitive:

```powershell
sill setting list                                 # every setting, its value and what it does
sill setting get ImagePath.SaveLocation
sill setting set ImagePath.SaveLocation Custom
sill setting set ImagePath.CustomFolder "D:\Screenshots"
sill setting reset ImagePath.SaveLocation         # back to the default
```

A running Sill picks up the change right away. Run `sill help` for an overview.

To use `sill` from any folder, turn on *Add to PATH* on the *General* page. Terminals opened after that find it; ones
already open don't.

## Troubleshooting

- **A shortcut does nothing, or Sill says a feature was disabled.** Another app already uses that key combination. The
  other features keep working; close the other app, or change its shortcut, and restart Sill.
- **Settings didn't apply.** If `settings.json` has a mistake, Sill shows a notification and keeps the previous
  settings until the file is fixed.

## Uninstalling

1. Uninstall *Proliminal Sill* from *Settings* → *Apps* → *Installed apps*, or run `sill uninstall`. This closes Sill,
   turns off *Start with Windows* and *Add to PATH*, and removes the Start menu shortcut and
   `%LOCALAPPDATA%\Programs\Proliminal\Sill`.
2. Optionally delete `%APPDATA%\Proliminal\Sill` (settings) and `%TEMP%\Proliminal\Sill` (saved clipboard images).

Changes Sill made to Windows, such as *All black* or a hidden Recycle Bin, stay; turn them off before uninstalling to
undo them. A copy you ran without installing is removed by turning off *Start with Windows* and deleting `sill.exe`.

## Writing plugins

Plugins are .NET class libraries built on the Sill SDK, which is the source in this repository. You don't need to build
it: on the *Documentation* page under *Plugins*, *API reference* shows every type and member, and *Export SDK* writes
`Proliminal.Sill.Sdk.dll` with a starter plugin project that builds against it. Both come from `sill.exe` itself, so
they always match the Sill you run.

Issues and pull requests for the SDK are welcome here.

## License

The SDK in this repository is licensed under the [MIT License](LICENSE). Sill itself (`sill.exe`) isn't open source:
the MIT License covers only the source in this repository.
