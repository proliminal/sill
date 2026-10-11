using Proliminal.Sill.Sdk.Abstractions;

namespace Proliminal.Sill.Sdk;

/// <summary>
///     What a button or quick action does. It runs on the UI thread; while the returned task runs, its button is
///     disabled. If it throws, Sill shows the error as a notification.
/// </summary>
/// <param name="context">What the action can use, such as notifications.</param>
public delegate Task PluginAction(IActionContext context);
