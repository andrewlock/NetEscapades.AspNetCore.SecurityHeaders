namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use the Web MIDI API.
    /// If disabled in a document, the promise returned by <code>requestMIDIAccess()</code>
    /// must reject with a DOMException parameter.
    /// </summary>
    public class MidiPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public MidiPermissionsPolicyDirectiveBuilder() : base("midi")
        {
        }
    }
}
