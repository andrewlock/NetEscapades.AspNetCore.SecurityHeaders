namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// Controls whether the current document is allowed to use the Web Bluetooth API.
/// If disabled then methods of the Bluetooth object returned by <code>Navigator.bluetooth</code>
/// will block access. <code>Bluetooth.getAvailability()</code> will always fulfill its returned
/// promise with a value of false. Bluetooth.getDevices() will reject its returned
/// promise with a <code>SecurityError</code><code>Bluetooth.requestDevice()</code> will reject
/// its returned promise with a <code>SecurityError</code>
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