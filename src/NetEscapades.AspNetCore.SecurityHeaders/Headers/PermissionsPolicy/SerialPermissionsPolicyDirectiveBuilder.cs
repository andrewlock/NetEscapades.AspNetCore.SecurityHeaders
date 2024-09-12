namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>serial</c> directive controls whether the current document is
/// allowed to use the <c>Web Serial API</c> to communicate with serial devices, either directly connected
/// via a serial port, or via USB or Bluetooth devices emulating a serial port. Specifically, where a
/// defined policy blocks use of this feature, <c>Serial.requestPort()</c> and <c>Serial.getPorts()</c> calls
/// will return a <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>SecurityError</c>.
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