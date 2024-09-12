namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>bluetooth</c> directive controls whether the current document is allowed to use the <c>Web Bluetooth API</c>.
/// Specifically, where a defined policy disallows use of this feature, the methods of the <c>Bluetooth</c> object returned by <c>Navigator.bluetooth</c>, will block access:
/// <list type="bullet">
/// <item><description><c>Bluetooth.getAvailability()</c> will always fulfill its returned <c>Promise</c> with a value of false.</description></item>
/// <item><description><c>Bluetooth.getDevices()</c> will reject its returned <c>Promise</c> with a <c>SecurityError</c> <c>DOMException</c>.</description></item>
/// <item><description><c>Bluetooth.requestDevice()</c> will reject its returned <c>Promise</c> with a <c>SecurityError</c> <c>DOMException</c>.</description></item>
/// </list>
/// </summary>
public class BluetoothPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BluetoothPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public BluetoothPermissionsPolicyDirectiveBuilder() : base("bluetooth")
    {
    }
}