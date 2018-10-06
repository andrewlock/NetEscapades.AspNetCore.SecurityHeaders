using System.Collections.Generic;
using System.Linq;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Used for building a Feature-Policy header
    /// </summary>
    public class FeaturePolicyBuilder
    {
        /// <summary>
        /// Controls whether the current document is allowed to use the accelerometer sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a 
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an 
        /// event) requires access to multiple sensors of different types than each of the 
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        public AccelerometerFeaturePolicyDirectiveBuilder AddAccelerometer() => AddDirective(new AccelerometerFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the ambient light sensor sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a 
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an 
        /// event) requires access to multiple sensors of different types than each of the 
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        public AmbientLightSensorFeaturePolicyDirectiveBuilder AddAmbientLightSensor() => AddDirective(new AmbientLightSensorFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls access to autoplay of media requested through the 
        /// <code>HTMLMediaElement</code> interface. If disabled in a document, 
        /// then calls to <code>play()</code> without a user gesture will 
        /// reject the promise with a <code>NotAllowedError</code> DOMException 
        /// object as its parameter. The autoplay attribute will be ignored.
        /// </summary>
        public AutoplayFeaturePolicyDirectiveBuilder AddAutoplay() => AddDirective(new AutoplayFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls access to video input devices requested through the 
        /// NavigatorUserMedia interface. If disabled in a document, then calls 
        /// to <code>getUserMedia()</code> will not grant access to video input 
        /// devices in that document.
        /// </summary>
        public CameraFeaturePolicyDirectiveBuilder AddCamera() => AddDirective(new CameraFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether encrypted media extensions are available. If disabled
        /// The promise returned by <code>requestMediaKeySystemAccess()</code> must 
        /// return a promise which rejects with a <code>SecurityError</code> DOMException 
        /// object as its parameter.
        /// </summary>
        public EncryptedMediaFeaturePolicyDirectiveBuilder AddEncryptedMedia() => AddDirective(new EncryptedMediaFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use 
        /// <code>Element.requestFullScreen()</code>. When this policy is enabled,
        /// the returned <code>Promise</code> rejects with a <code>TypeError</code>.
        /// </summary>
        public FullscreenFeaturePolicyDirectiveBuilder AddFullscreen() => AddDirective(new FullscreenFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the
        /// <code>Geolocation</code> Interface. When this policy is enabled,
        /// calls to <code>getCurrentPosition()</code> and <code>watchPosition()</code>
        /// will cause those functions' callbacks to be invoked with a 
        /// <code>PositionError</code> code of <code>PERMISSION_DENIED</code>.
        /// </summary>
        public GeolocationFeaturePolicyDirectiveBuilder AddGeolocation() => AddDirective(new GeolocationFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the gyroscope sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a 
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an 
        /// event) requires access to multiple sensors of different types than each of the 
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        public GyroscopeFeaturePolicyDirectiveBuilder AddGyroscope() => AddDirective(new GyroscopeFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the magnetometer sensor.
        /// If disabled then constructing of a Sensor-based interface object will throw a 
        /// <code>SecurityError</code>. The events are not fired. If an interface (or an 
        /// event) requires access to multiple sensors of different types than each of the 
        /// corresponding sensor features must be allowed in order to use the interface.
        /// </summary>
        public MagnetometerFeaturePolicyDirectiveBuilder AddMagnetometer() => AddDirective(new MagnetometerFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use audio 
        /// input devices. When this policy is enabled, the <code>Promise</code>
        /// returned by <code>MediaDevices.getUserMedia()</code> will
        /// reject with a <code>NotAllowedError</code>.
        /// </summary>
        public MicrophoneFeaturePolicyDirectiveBuilder AddMicrophone() => AddDirective(new MicrophoneFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the Web MIDI API.
        /// If disabled in a document, the promise returned by <code>requestMIDIAccess()</code>
        /// must reject with a DOMException parameter.
        /// </summary>
        public MidiFeaturePolicyDirectiveBuilder AddMidi() => AddDirective(new MidiFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls  whether the current document is allowed to use the
        /// PaymentRequest interface. If disabled then calls to the 
        /// <code>PaymentRequest</code> constuctor will throw a <code>SecurityError</code>.
        /// </summary>
        public PaymentFeaturePolicyDirectiveBuilder AddPayment() => AddDirective(new PaymentFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use Picture In Picture.
        /// If disabled in a document, then calls to <code>requestPictureInPicture()</code>
        /// will throw a <code>SecurityError</code> and <code>pictureInPictureEnabled</code>
        /// will return <code>false</code>.
        /// </summary>
        public PictureInPictureFeaturePolicyDirectiveBuilder AddPictureInPicture() => AddDirective(new PictureInPictureFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls access to audio output devices requested through 
        /// the NavigatorUserMedia interface. If disabled then calls to 
        /// <code>getUserMedia()</code> will not grant access to audio 
        /// output devices in that document.
        /// </summary>
        public SpeakerFeaturePolicyDirectiveBuilder AddSpeaker() => AddDirective(new SpeakerFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use synchronous XMLHttpRequest transfers.
        /// If disabled in a document, then calls to <code>send()</code> on XMLHttpRequest objects
        /// will throw a <code>NetworkError</code>.
        /// </summary>
        public SynchronousXhrFeaturePolicyDirectiveBuilder AddSyncXHR() => AddDirective(new SynchronousXhrFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the WebUSB API.
        /// If disabled then calls to <code>getDevices()</code> should return a 
        /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
        /// </summary>
        public UsbFeaturePolicyDirectiveBuilder AddUsb() => AddDirective(new UsbFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Controls whether the current document is allowed to use the WebVR API.
        /// If disabled then calls to <code>getVRDisplays()</code> should return a 
        /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
        /// </summary>
        public VRFeaturePolicyDirectiveBuilder AddVR() => AddDirective(new VRFeaturePolicyDirectiveBuilder());

        /// <summary>
        /// Create a custom Feature-Policy directive for an un-implemented directive.
        /// 
        /// The directive can be built using standard methods such as <see cref="FeaturePolicyDirectiveBuilder.Self"/>
        /// and <see cref="FeaturePolicyDirectiveBuilder.None"/>
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        public CustomFeaturePolicyDirectiveBuilder AddCustomFeature(string directive) => AddDirective(new CustomFeaturePolicyDirectiveBuilder(directive));

        /// <summary>
        /// Create a custom Feature-Policy directive for an un-implemented directive.
        /// 
        /// The directive can be built using standard methods such as <see cref="FeaturePolicyDirectiveBuilder.Self"/>
        /// and <see cref="FeaturePolicyDirectiveBuilder.None"/>
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        /// <param name="value">The value to use for the directive, e.g. * or 'none'</param>
        public CustomFeaturePolicyDirective AddCustomFeature(string directive, string value) => AddDirective(new CustomFeaturePolicyDirective(directive, value));

        private readonly Dictionary<string, FeaturePolicyDirectiveBuilderBase> _directives = new Dictionary<string, FeaturePolicyDirectiveBuilderBase>();
        
        private T AddDirective<T>(T directive) where T: FeaturePolicyDirectiveBuilderBase
        {
            _directives[directive.Directive] = directive;
            return directive;
        }

        internal string Build()
        {
            return string.Join("; ", _directives.Values.Select(x => x.Build()).Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}