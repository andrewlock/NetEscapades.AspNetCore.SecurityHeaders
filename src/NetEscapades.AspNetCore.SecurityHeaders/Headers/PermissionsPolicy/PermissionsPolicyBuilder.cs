using System.Collections.Generic;
using System.Linq;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Used for building a Permissions-Policy header
    /// </summary>
    public class PermissionsPolicyBuilder
    {
        private readonly Dictionary<string, PermissionsPolicyDirectiveBuilderBase> _directives = new Dictionary<string, PermissionsPolicyDirectiveBuilderBase>();

        /// <summary>
        /// Controls whether the current document is allowed to use the accelerometer sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an
        /// event) requires access to multiple sensors of different types than each of the
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        /// <returns>A configured <see cref="AccelerometerPermissionsPolicyDirectiveBuilder"/></returns>
        public AccelerometerPermissionsPolicyDirectiveBuilder AddAccelerometer() => AddDirective(new AccelerometerPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the ambient light sensor sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an
        /// event) requires access to multiple sensors of different types than each of the
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        /// <returns>A configured <see cref="AmbientLightSensorPermissionsPolicyDirectiveBuilder"/></returns>
        public AmbientLightSensorPermissionsPolicyDirectiveBuilder AddAmbientLightSensor() => AddDirective(new AmbientLightSensorPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls access to autoplay of media requested through the
        /// <code>HTMLMediaElement</code> interface. If disabled in a document,
        /// then calls to <code>play()</code> without a user gesture will
        /// reject the promise with a <code>NotAllowedError</code> DOMException
        /// object as its parameter. The autoplay attribute will be ignored.
        /// </summary>
        /// <returns>A configured <see cref="AutoplayPermissionsPolicyDirectiveBuilder"/></returns>
        public AutoplayPermissionsPolicyDirectiveBuilder AddAutoplay() => AddDirective(new AutoplayPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls access to video input devices requested through the
        /// NavigatorUserMedia interface. If disabled in a document, then calls
        /// to <code>getUserMedia()</code> will not grant access to video input
        /// devices in that document.
        /// </summary>
        /// <returns>A configured <see cref="CameraPermissionsPolicyDirectiveBuilder"/></returns>
        public CameraPermissionsPolicyDirectiveBuilder AddCamera() => AddDirective(new CameraPermissionsPolicyDirectiveBuilder());

        ///// <summary>
        ///// Controls whether encrypted media extensions are available. If disabled
        ///// The promise returned by <code>requestMediaKeySystemAccess()</code> must
        ///// return a promise which rejects with a <code>SecurityError</code> DOMException
        ///// object as its parameter.
        ///// </summary>
        ///// <returns>A configured <see cref="EncryptedMediaFeaturePolicyDirectiveBuilder"/></returns>
        // public EncryptedMediaFeaturePolicyDirectiveBuilder AddEncryptedMedia() => AddDirective(new EncryptedMediaFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use
        /// <code>Element.requestFullScreen()</code>. When this policy is enabled,
        /// the returned <code>Promise</code> rejects with a <code>TypeError</code>.
        /// </summary>
        /// <returns>A configured <see cref="FullscreenPermissionsPolicyDirectiveBuilder"/></returns>
        public FullscreenPermissionsPolicyDirectiveBuilder AddFullscreen() => AddDirective(new FullscreenPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the
        /// <code>Geolocation</code> Interface. When this policy is enabled,
        /// calls to <code>getCurrentPosition()</code> and <code>watchPosition()</code>
        /// will cause those functions' callbacks to be invoked with a
        /// <code>PositionError</code> code of <code>PERMISSION_DENIED</code>.
        /// </summary>
        /// <returns>A configured <see cref="GeolocationPermissionsPolicyDirectiveBuilder"/></returns>
        public GeolocationPermissionsPolicyDirectiveBuilder AddGeolocation() => AddDirective(new GeolocationPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the gyroscope sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an
        /// event) requires access to multiple sensors of different types than each of the
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        /// <returns>A configured <see cref="GyroscopePermissionsPolicyDirectiveBuilder"/></returns>
        public GyroscopePermissionsPolicyDirectiveBuilder AddGyroscope() => AddDirective(new GyroscopePermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to make a FLoC calculation.
        /// If disabled in a document, the promise returned by <code>interestCohort()</code>
        /// must reject with a DOMException parameter.
        /// </summary>
        /// <returns>A configured <see cref="FederatedLearningOfCohortsCalculationPermissionsPolicyDirectiveBuilder"/></returns>
        public FederatedLearningOfCohortsCalculationPermissionsPolicyDirectiveBuilder AddFederatedLearningOfCohortsCalculation() => AddDirective(new FederatedLearningOfCohortsCalculationPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the magnetometer sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an
        /// event) requires access to multiple sensors of different types than each of the
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        /// <returns>A configured <see cref="MagnetometerPermissionsPolicyDirectiveBuilder"/></returns>
        public MagnetometerPermissionsPolicyDirectiveBuilder AddMagnetometer() => AddDirective(new MagnetometerPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use audio
        /// input devices. When this policy is enabled, the <code>Promise</code>
        /// returned by <code>MediaDevices.getUserMedia()</code> will
        /// reject with a <code>NotAllowedError</code>.
        /// </summary>
        /// <returns>A configured <see cref="MicrophonePermissionsPolicyDirectiveBuilder"/></returns>
        public MicrophonePermissionsPolicyDirectiveBuilder AddMicrophone() => AddDirective(new MicrophonePermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the Web MIDI API.
        /// If disabled in a document, the promise returned by <code>requestMIDIAccess()</code>
        /// must reject with a DOMException parameter.
        /// </summary>
        /// <returns>A configured <see cref="MidiPermissionsPolicyDirectiveBuilder"/></returns>
        public MidiPermissionsPolicyDirectiveBuilder AddMidi() => AddDirective(new MidiPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls  whether the current document is allowed to use the
        /// PaymentRequest interface. If disabled then calls to the
        /// <code>PaymentRequest</code> constuctor will throw a <code>SecurityError</code>.
        /// </summary>
        /// <returns>A configured <see cref="PaymentPermissionsPolicyDirectiveBuilder"/></returns>
        public PaymentPermissionsPolicyDirectiveBuilder AddPayment() => AddDirective(new PaymentPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use Picture In Picture.
        /// If disabled in a document, then calls to <code>requestPictureInPicture()</code>
        /// will throw a <code>SecurityError</code> and <code>pictureInPictureEnabled</code>
        /// will return <code>false</code>.
        /// </summary>
        /// <returns>A configured <see cref="PictureInPicturePermissionsPolicyDirectiveBuilder"/></returns>
        public PictureInPicturePermissionsPolicyDirectiveBuilder AddPictureInPicture() => AddDirective(new PictureInPicturePermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls access to audio output devices requested through
        /// the NavigatorUserMedia interface. If disabled then calls to
        /// <code>getUserMedia()</code> will not grant access to audio
        /// output devices in that document.
        /// </summary>
        /// <returns>A configured <see cref="SpeakerPermissionsPolicyDirectiveBuilder"/></returns>
        public SpeakerPermissionsPolicyDirectiveBuilder AddSpeaker() => AddDirective(new SpeakerPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use synchronous XMLHttpRequest transfers.
        /// If disabled in a document, then calls to <code>send()</code> on XMLHttpRequest objects
        /// will throw a <code>NetworkError</code>.
        /// </summary>
        /// <returns>A configured <see cref="SynchronousXhrPermissionsPolicyDirectiveBuilder"/></returns>
        public SynchronousXhrPermissionsPolicyDirectiveBuilder AddSyncXHR() => AddDirective(new SynchronousXhrPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the WebUSB API.
        /// If disabled then calls to <code>getDevices()</code> should return a
        /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
        /// </summary>
        /// <returns>A configured <see cref="UsbPermissionsPolicyDirectiveBuilder"/></returns>
        public UsbPermissionsPolicyDirectiveBuilder AddUsb() => AddDirective(new UsbPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the WebVR API.
        /// If disabled then calls to <code>getVRDisplays()</code> should return a
        /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
        /// </summary>
        /// <returns>A configured <see cref="VRPermissionsPolicyDirectiveBuilder"/></returns>
        public VRPermissionsPolicyDirectiveBuilder AddVR() => AddDirective(new VRPermissionsPolicyDirectiveBuilder());

        /// <summary>
        /// Create a custom Feature-Policy directive for an un-implemented directive.
        ///
        /// The directive can be built using standard methods such as <see cref="PermissionsPolicyDirectiveBuilder.Self"/>
        /// and <see cref="PermissionsPolicyDirectiveBuilder.None"/>
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        /// <returns>A configured <see cref="CustomPermissionsPolicyDirectiveBuilder"/></returns>
        public CustomPermissionsPolicyDirectiveBuilder AddCustomFeature(string directive) => AddDirective(new CustomPermissionsPolicyDirectiveBuilder(directive));

        /// <summary>
        /// Create a custom Feature-Policy directive for an un-implemented directive.
        ///
        /// The directive can be built using standard methods such as <see cref="PermissionsPolicyDirectiveBuilder.Self"/>
        /// and <see cref="PermissionsPolicyDirectiveBuilder.None"/>
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        /// <param name="value">The value to use for the directive, e.g. * or 'none'</param>
        /// <returns>A configured <see cref="CustomPermissionsPolicyDirectiveBuilder"/></returns>
        public CustomPermissionsPolicyDirective AddCustomFeature(string directive, string value) => AddDirective(new CustomPermissionsPolicyDirective(directive, value));

        private T AddDirective<T>(T directive) where T : PermissionsPolicyDirectiveBuilderBase
        {
            _directives[directive.Directive] = directive;
            return directive;
        }

        /// <summary>
        /// Builds the Feature-Policy value.
        /// </summary>
        /// <returns>The string representing the complete Feature-Policy value.</returns>
        internal string Build()
        {
            return string.Join(", ", _directives.Values.Select(x => x.Build()).Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
