# Proliminal Sill

A small Windows tray app with keyboard shortcuts and switches for everyday tasks.

**[Download the latest release](https://github.com/proliminal/sill/releases/latest)** · 64-bit Windows 10 and 11

## Features

| | |
| --- | --- |
| 🖼️ **Image path** | <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>P</kbd> turns the image or file on your clipboard into a path. |
| 💻 **Windows Terminal** | <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>T</kbd> brings Terminal to the front, plus opacity, window and title bar colors. |
| ⚡ **PowerShell** | One-click PowerShell 7 with autocomplete, file icons, an oh-my-posh prompt and a Visual Studio dev shell. |
| 🗑️ **Recycle Bin** | Open, empty or hide it from the desktop. |
| 🌑 **Windows Appearance** | Dark mode, all black, transparency, a compact taskbar and Start. |
| ⌨️ **Command line** | `sill setting get/set/reset` for every option. |

## Writing plugins

Plugins are .NET class libraries built on the SDK in this repository. Sill ships everything you need: on the
*Documentation* page, *API reference* lists every type and member, and *Export SDK* writes `Proliminal.Sill.Sdk.dll`
with a starter project. Issues and pull requests for the SDK are welcome.

---

<sub>Sill installs itself per user without administrator rights, and updates itself from this repository's releases.
`sill.exe` isn't code-signed yet, so SmartScreen may ask you to choose *More info* → *Run anyway*.
The SDK is [MIT-licensed](LICENSE); `sill.exe` itself isn't open source.</sub>
