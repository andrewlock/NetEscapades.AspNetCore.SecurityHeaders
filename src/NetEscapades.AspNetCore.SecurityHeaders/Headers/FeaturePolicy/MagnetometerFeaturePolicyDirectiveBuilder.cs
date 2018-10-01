namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls whether the current document is allowed to use the magnetometer sensor.
    /// If disabled then constructing of a Sensor-based interface object wukk throw a 
    /// <code>SecurityError</code>. The events are not fired. If an interface (or an 
    /// event) requires access to multiple sensors of different types than each of the 
    /// corresponding sensor features must be allowed in order to use the interface.
    /// </summary>
    public class MagnetometerFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        public MagnetometerFeaturePolicyDirectiveBuilder() : base("magnetometer")
        {
        }
    }
}