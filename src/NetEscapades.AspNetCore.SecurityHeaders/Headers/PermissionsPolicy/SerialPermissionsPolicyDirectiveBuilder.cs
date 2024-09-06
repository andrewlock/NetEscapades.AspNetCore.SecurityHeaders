namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header serial directive controls whether the current document is allowed to use
/// the <code>Web Serial API</code> to communicate with serial devices, either directly connected via a serial port, or via USB
/// or Bluetooth devices emulating a serial port. Specifically, where a defined policy blocks use of this feature,
/// <code>Serial.requestPort()</code> and <code>Serial.getPorts()</code> calls will return a Promise that rejects with a <code>DOMException</code> of type <code>SecurityError</code>
/// </summary>
public class SerialPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SerialPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public SerialPermissionsPolicyDirectiveBuilder() : base("serial")
    {
    }
}