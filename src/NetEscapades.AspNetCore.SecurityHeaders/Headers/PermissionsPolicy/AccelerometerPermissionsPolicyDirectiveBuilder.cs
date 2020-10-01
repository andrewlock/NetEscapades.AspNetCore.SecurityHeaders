namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use the accelerometer sensor.
    /// If disabled then constructing of a Sensor-based interface object will throw a
    /// <code>SecurityError</code>. The events are not fired. If an interface (or an
    /// event) requires access to multiple sensors of different types than each of the
    /// corresponding sensor features must be allowed in order to use the interface.
    /// </summary>
    public class AccelerometerPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccelerometerPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public AccelerometerPermissionsPolicyDirectiveBuilder() : base("accelerometer")
        {
        }
    }
}
