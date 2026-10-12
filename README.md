<h1 align="center">Proliminal Sill</h1>

<p align="center">
  A small Windows tray app that keeps your PC private, secure and the way you like it,<br>
  with keyboard shortcuts and one-click switches for everyday tasks.
</p>

<p align="center">
  <a href="https://github.com/proliminal/sill/releases/latest"><img src="https://img.shields.io/github/v/release/proliminal/sill?style=for-the-badge&label=Download&color=4cc2ff" alt="Download the latest release"></a>
  <img src="https://img.shields.io/badge/Windows-10%20%7C%2011-0078d4?style=for-the-badge" alt="Windows 10 and 11, 64-bit">
  <a href="LICENSE"><img src="https://img.shields.io/badge/SDK-MIT-3a3a3a?style=for-the-badge" alt="SDK: MIT license"></a>
</p>

<p align="center">
  <img src="docs/screenshots/hero.png" alt="Sill's Home page with the privacy and security scores, next to the quick menu that opens from the tray icon" width="100%">
</p>

<p align="center">
  <b>Home</b> shows what needs you, your scores and your shortcuts. The <b>quick menu</b> opens from the tray icon or <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Q</kbd>.
</p>

<br>

## 🛡️ Privacy and security, scored

Sill scores how well your account is set up and fixes it in one click. It only changes settings of your own account,
needs no administrator, and nothing it reads leaves your PC.

<table>
  <tr>
    <td width="50%"><img src="docs/screenshots/security.png" alt="Security page with the security score and Apply all"></td>
    <td width="50%"><img src="docs/screenshots/startup-apps.png" alt="Startup apps, showing the command each one runs and who signed it"></td>
  </tr>
  <tr>
    <td><b>Security score</b><br>What Windows Security doesn't tell you, from Defender's defaults to drive encryption, with <i>Apply all</i>.</td>
    <td><b>Startup apps</b><br>Everything that starts when you sign in, the command it runs, and whether it's signed.</td>
  </tr>
  <tr>
    <td width="50%"><img src="docs/screenshots/privacy-check.png" alt="Privacy check, turning off all sharing at once"></td>
    <td width="50%"><img src="docs/screenshots/app-access.png" alt="App access for the camera, microphone and location, with the privacy shutter"></td>
  </tr>
  <tr>
    <td><b>Privacy check</b><br>Turns off every setting that shares what you do, and turns it off again when a Windows update turns it back on.</td>
    <td><b>App access</b><br>Which apps may use your camera, microphone and location, which did, and a shutter that blocks all three.</td>
  </tr>
</table>

**Sill noticed** keeps a timeline of what changed on its own: sharing that came back on, protection that turned off,
apps that added themselves to startup.

<br>

## 🎨 Switch how Windows looks in one click

<img src="docs/screenshots/appearance.png" alt="Appearance page with saved looks and the built-in Presenting look" width="100%">

Save how Windows looks now as a **look**: dark mode, all black, transparency, wallpaper and a compact taskbar. Switch
between looks from the quick menu, or put on the built-in **Presenting** look before you share your screen.

<br>

## ⌨️ Made for the command line

<table>
  <tr>
    <td width="50%"><img src="docs/screenshots/powershell.png" alt="PowerShell page with autocomplete, file icons and the oh-my-posh prompt"></td>
    <td width="50%"><img src="docs/screenshots/path.png" alt="PATH editor with a checkup for missing and duplicate folders"></td>
  </tr>
  <tr>
    <td><b>PowerShell</b><br>One-click PowerShell 7 with autocomplete, file icons, an oh-my-posh prompt and a Visual Studio dev shell.</td>
    <td><b>PATH</b><br>Edit your PATH and the system's, and find folders that are gone, listed twice or hide another program.</td>
  </tr>
  <tr>
    <td width="50%"><img src="docs/screenshots/terminal.png" alt="Windows Terminal page with opacity, window and title bar colors"></td>
    <td width="50%"><img src="docs/screenshots/ports.png" alt="Ports page listing which program is on which port"></td>
  </tr>
  <tr>
    <td><b>Windows Terminal</b><br>Bring Terminal to the front from anywhere, open it in the folder you're in, and set its opacity and colors.</td>
    <td><b>Ports</b><br>See which program is on a port, and stop it to free the port.</td>
  </tr>
</table>

<br>

## ⚡ Everyday shortcuts

<table>
  <tr>
    <td width="50%"><img src="docs/screenshots/image-path.png" alt="Image path page"></td>
    <td width="50%"><img src="docs/screenshots/recycle-bin.png" alt="Recycle Bin page"></td>
  </tr>
  <tr>
    <td><b>Image path</b><br>Turns the image or file on your clipboard into its path, ready to paste into a terminal or a chat.</td>
    <td><b>Recycle Bin</b><br>Open or empty it without the desktop icon, so you can hide that.</td>
  </tr>
</table>

| Shortcut | Does |
| --- | --- |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>S</kbd> | Open Sill |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Q</kbd> | Open the quick menu |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>P</kbd> | Copy the clipboard's image as a path |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>T</kbd> | Bring Windows Terminal to the front |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>E</kbd> | Open Terminal in the File Explorer folder in front |

Every shortcut can be changed. Moving to another PC? Export your setup to a file and import it there; you see every
change before anything is changed. From a terminal, `sill setup export|import <file>` does the same, and
`sill setting list|get|set|reset` reaches every option.

<br>

## 🧩 Write your own plugins

<table>
  <tr>
    <td width="50%"><img src="docs/screenshots/plugins.png" alt="External plugins page with the Morse keyboard sample plugin and what it uses"></td>
    <td width="50%"><img src="docs/screenshots/plugin-docs.png" alt="Plugin documentation with the API reference and Export SDK"></td>
  </tr>
  <tr>
    <td><b>Plugins you trust</b><br>Sill asks before it loads a new or changed plugin, and shows what Sill found it uses in its code, like this Morse keyboard sample.</td>
    <td><b>Everything you need is in Sill</b><br>The API reference lists every type and member; <i>Export SDK</i> writes the SDK with a starter project.</td>
  </tr>
</table>

Plugins are .NET class libraries built on the SDK in this repository. They get their own page in Sill, quick actions
and shortcuts. Issues and pull requests for the SDK are welcome.

---

<sub>Sill installs itself per user without administrator rights, and updates itself from this repository's releases.
`sill.exe` isn't code-signed yet, so SmartScreen may ask you to choose *More info* → *Run anyway*.
The SDK is [MIT-licensed](LICENSE); `sill.exe` itself isn't open source.</sub>
