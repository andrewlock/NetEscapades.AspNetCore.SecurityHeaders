namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// Controls whether the current document is allowed to use the Web Bluetooth API.
/// If disabled then methods of the Bluetooth object returned by Navigator.bluetooth
/// will block access.
/// Bluetooth.getAvailability() will always fulfill its returned
/// Promise with a value of false.
/// Bluetooth.getDevices() will reject its returned Promise with a <code>SecurityError</code>.
/// Bluetooth.requestDevice() will reject its returned Promise with a <code>SecurityError</code>
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