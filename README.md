<p align="center">
  <img src="docs/brand/sill-banner@2x.png" alt="Sill. Your PC, on your terms. The cornerstone of a private, secure and personal device." width="100%">
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
  <b>Home</b> shows what needs you, what's new, your scores and what Sill noticed. The <b>quick menu</b> opens from the tray icon or <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Q</kbd>.
</p>

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
    <td width="50%"><img src="docs/screenshots/app-access.png" alt="App access, with the switches for the camera, microphone, location and your information"></td>
  </tr>
  <tr>
    <td><b>Privacy check</b><br>Turns off every setting that shares what you do, and turns it off again when a Windows update turns it back on.</td>
    <td><b>App access</b><br>Which apps may use your camera, microphone and location, which did, and a shutter that blocks all three.</td>
  </tr>
</table>

**Sill noticed** keeps a timeline of what changed on its own: sharing that came back on, protection that turned off,
apps that added themselves to startup, and settings Sill applied that something else changed back.

## 🔑 A password manager that stays on your PC

Your logins in one encrypted file on this PC, never synced or sent anywhere. Unlock it with a master password or
Windows Hello, optionally on a secure desktop that other apps can't watch. Press
<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>L</kbd> over any app to fill in a login or copy it; copies stay out
of clipboard history and are cleared after a while. It shows weak and reused passwords, makes two-factor codes, and
imports from your browser, Bitwarden, 1Password and KeePass. From a terminal, `sill pass get <name> --clip` copies a
password once you allow it.

## 🎨 Switch how Windows looks in one click

<img src="docs/screenshots/appearance.png" alt="Appearance page with saved looks and the built-in Presenting look" width="100%">

Save your configuration as a **look**: dark mode, all black, transparency, wallpaper and a compact taskbar. 
Switch between looks from the quick menu.

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

## 🧰 Developer tools

Small tools for code and data, all working on your PC only: **Encoding** (Base64, URL, HTML, Punycode and more),
**Hashing** of text and files, **Generators** for GUIDs, passwords and keys, **Date and time**, **Numbers** in every
base, **Color** formats and contrast, **Text** case and cleanup, **Format** for JSON, XML and YAML, **Regex** with its
matches and groups, and **JWT** decoding. Most also work on what's on the clipboard, from the quick menu.

Sill can also opt you out of the usage data that tools like the .NET SDK, PowerShell, the Azure CLI and Next.js send.

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

Every shortcut can be changed, and every quick action, and every look, can get one of its own. Moving to another PC?
Export your setup to a file, looks included, and import it there; you see every change before anything is changed.
From a terminal, `sill setup export|import <file>` does the same, `sill setting list|get|set` reaches every option on
Sill's pages, and `sill look apply <name>` switches looks.

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

Plugins are .NET class libraries built on the SDK in this repository. A plugin describes its page and Sill draws it,
so it looks like the rest of Sill and is found by search. Its options can be changed with `sill setting` and go along
in an exported setup; secrets, such as API tokens, are kept encrypted apart from them and never exported. It can add
quick actions, which users can give a shortcut, popups that open over any app as the password picker does, sections at
the top of the quick menu, and recommendations to the privacy or security score. It can also note what it did in Sill
noticed, and show on Home what keeps it from working. Each plugin
is loaded apart from Sill and from other plugins: it gets the SDK and .NET from Sill, and brings everything else
itself. Issues and pull requests for the SDK are welcome.
