using System;
using System.Collections.Generic;
using System.Linq;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

namespace Microsoft.AspNetCore.Builder;

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
    /// The HTTP <c>Permissions-Policy</c> header <c>attribution-reporting</c> directive controls whether the current document is allowed to use the <c>Attribution Reporting API</c>.
    /// Specifically, where a defined policy blocks the use of this feature:
    /// <list type="bullet">
    /// <item><description>Background <c>attributionsrc</c> requests won't be made.</description></item>
    /// <item><description>The <c>XMLHttpRequest.setAttributionReporting()</c> method will throw an exception when called.</description></item>
    /// <item><description>The <c>attributionReporting</c> option, when included on a <c>fetch()</c> call, will cause it to throw an exception.</description></item>
    /// <item><description>Registration headers (<c>Attribution-Reporting-Register-Source</c> and <c>Attribution-Reporting-Register-Trigger</c>) in HTTP responses on associated documents will be ignored.</description></item>
    /// </list>
    /// </summary>
    /// <returns>A configured <see cref="AttributionReportingPermissionsPolicyDirectiveBuilder"/></returns>
    public AttributionReportingPermissionsPolicyDirectiveBuilder AddAttributionReporting() => AddDirective(new AttributionReportingPermissionsPolicyDirectiveBuilder());

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
    /// The HTTP <c>Permissions-Policy</c> header <c>bluetooth</c> directive controls whether the current document is allowed to use the <c>Web Bluetooth API</c>.
    /// Specifically, where a defined policy disallows use of this feature, the methods of the <c>Bluetooth</c> object returned by <c>Navigator.bluetooth</c>, will block access:
    /// <list type="bullet">
    /// <item><description><c>Bluetooth.getAvailability()</c> will always fulfill its returned <c>Promise</c> with a value of false.</description></item>
    /// <item><description><c>Bluetooth.getDevices()</c> will reject its returned <c>Promise</c> with a <c>SecurityError</c> <c>DOMException</c>.</description></item>
    /// <item><description><c>Bluetooth.requestDevice()</c> will reject its returned <c>Promise</c> with a <c>SecurityError</c> <c>DOMException</c>.</description></item>
    /// </list>
    /// </summary>
    /// <returns>A configured <see cref="BluetoothPermissionsPolicyDirectiveBuilder"/></returns>
    public BluetoothPermissionsPolicyDirectiveBuilder AddBluetooth() => AddDirective(new BluetoothPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// Controls access to video input devices requested through the
    /// NavigatorUserMedia interface. If disabled in a document, then calls
    /// to <code>getUserMedia()</code> will not grant access to video input
    /// devices in that document.
    /// </summary>
    /// <returns>A configured <see cref="CameraPermissionsPolicyDirectiveBuilder"/></returns>
    public CameraPermissionsPolicyDirectiveBuilder AddCamera() => AddDirective(new CameraPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>display-capture</c> directive controls whether or not the document is permitted to use <c>Screen Capture API</c>, that is, <c>getDisplayMedia()</c> to capture the screen's contents.
    /// If <c>display-capture</c> is disabled in a document, the document will not be able to initiate screen capture via <c>getDisplayMedia()</c> and will throw a <c>NotAllowedError</c> exception.
    /// </summary>
    /// <returns>A configured <see cref="DisplayCapturePermissionsPolicyDirectiveBuilder"/></returns>
    public DisplayCapturePermissionsPolicyDirectiveBuilder AddDisplayCapture() => AddDirective(new DisplayCapturePermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// Controls whether encrypted media extensions are available. If disabled
    /// The promise returned by <code>requestMediaKeySystemAccess()</code> must
    /// return a promise which rejects with a <code>SecurityError</code> DOMException
    /// object as its parameter.
    /// </summary>
    /// <returns>A configured <see cref="EncryptedMediaPermissionsPolicyDirectiveBuilder"/></returns>
    public EncryptedMediaPermissionsPolicyDirectiveBuilder AddEncryptedMedia() => AddDirective(new EncryptedMediaPermissionsPolicyDirectiveBuilder());

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
    /// The HTTP <c>Permissions-Policy</c> header <c>hid</c> directive controls whether the current document is allowed to use the <c>WebHID API</c> to connect to uncommon or exotic human interface devices such as alternative keyboards or gamepads.
    /// Specifically, where a defined policy blocks WebHID usage, the <c>Navigator.hid</c> property will not be available.
    /// </summary>
    /// <returns>A configured <see cref="HidPermissionsPolicyDirectiveBuilder"/></returns>
    public HidPermissionsPolicyDirectiveBuilder AddHid() => AddDirective(new HidPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>identity-credentials-get</c> directive controls whether the current document
    /// is allowed to use the <c>Federated Credential Management API (FedCM)</c>, and more specifically the <c>navigator.credentials.get()</c>
    /// method with an <c>identity</c> option. Where this policy forbids use of the API, the <c>Promise</c> returned by the <c>get()</c>
    /// call will reject with a <c>NotAllowedError DOMException</c>.
    /// </summary>
    /// <returns>A configured <see cref="IdentityCredentialsGetPermissionsPolicyDirectiveBuilder"/></returns>
    public IdentityCredentialsGetPermissionsPolicyDirectiveBuilder AddIdentityCredentialsGet() => AddDirective(new IdentityCredentialsGetPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>idle-detection</c> directive controls whether the current document is
    /// allowed to use the <c>Idle Detection API</c> to detect when users are interacting with their devices, for example
    /// to report "available"/"away" status in chat applications. Specifically, where a defined policy blocks use
    /// of this feature, <c>IdleDetector.start()</c> calls will return a <c>Promise</c> that rejects with a <c>DOMException</c> of
    /// type <c>NotAllowedError</c>.
    /// </summary>
    /// <returns>A configured <see cref="IdleDetectionPermissionsPolicyDirectiveBuilder"/></returns>
    public IdleDetectionPermissionsPolicyDirectiveBuilder AddIdleDetection() => AddDirective(new IdleDetectionPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>local-fonts</c> directive controls whether the current
    /// document is allowed to gather data on the user's locally-installed fonts via the
    /// <c>Window.queryLocalFonts()</c> method. Specifically, where a defined policy blocks use
    /// of this feature, <c>Promises</c> returned by <c>Window.queryLocalFonts()</c> will reject with a
    /// <c>DOMException</c> of type <c>SecurityError</c>.
    /// </summary>
    /// <returns>A configured <see cref="LocalFontsPermissionsPolicyDirectiveBuilder"/></returns>
    public LocalFontsPermissionsPolicyDirectiveBuilder AddLocalFonts() => AddDirective(new LocalFontsPermissionsPolicyDirectiveBuilder());

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
    /// The HTTP <c>Permissions-Policy</c> header <c>otp-credentials</c> directive controls whether
    /// the current document is allowed to use the <c>WebOTP API</c> to request a one-time password (OTP)
    /// from a specially-formatted SMS message sent by the app's server, i.e., via
    /// <c>navigator.credentials.get({otp: ..., ...})</c>. Specifically, where a defined policy blocks
    /// the use of this feature, the <c>Promise</c> returned by <c>navigator.credentials.get({otp})</c> will
    /// reject with a <c>SecurityError DOMException</c>.
    /// </summary>
    /// <returns>A configured <see cref="OtpCredentialsPermissionsPolicyDirectiveBuilder"/></returns>
    public OtpCredentialsPermissionsPolicyDirectiveBuilder AddOtpCredentials() => AddDirective(new OtpCredentialsPermissionsPolicyDirectiveBuilder());

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
    /// The HTTP <c>Permissions-Policy</c> header <c>publickey-credentials-create</c> directive
    /// controls whether the current document is allowed to use the <c>Web Authentication API</c>
    /// to create new WebAuthn credentials, i.e., via <c>navigator.credentials.create({publicKey})</c>.
    /// Specifically, where a defined policy blocks use of this feature, the <c>Promise</c> returned
    /// by <c>navigator.credentials.create({publicKey})</c> will reject with a <c>NotAllowedError DOMException</c>.
    /// If the method is called cross-origin. the <c>Promise</c> will also reject with a
    /// <c>NotAllowedError</c> if the feature is granted by <c>allow= on an iframe</c> and the frame
    /// does not also have <c>Transient activation</c>.
    /// </summary>
    /// <returns>A configured <see cref="PublickeyCredentialsCreatePermissionsPolicyDirectiveBuilder"/></returns>
    public PublickeyCredentialsCreatePermissionsPolicyDirectiveBuilder AddPublickeyCredentialsCreate() => AddDirective(new PublickeyCredentialsCreatePermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>publickey-credentials-get</c> directive controls whether the
    /// current document is allowed to access the <c>Web Authentication API</c> to retrieve public-key
    /// credentials, i.e., via <c>navigator.credentials.get({publicKey})</c>. Specifically, where a defined
    /// policy blocks the use of this feature, the <c>Promise</c> returned by <c>navigator.credentials.get({publicKey})</c>
    /// will reject with a <c>NotAllowedError DOMException</c>.
    /// </summary>
    /// <returns>A configured <see cref="PublickeyCredentialsGetPermissionsPolicyDirectiveBuilder"/></returns>
    public PublickeyCredentialsGetPermissionsPolicyDirectiveBuilder AddPublickeyCredentialsGet() => AddDirective(new PublickeyCredentialsGetPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>screen-wake-lock</c> directive controls whether the current document
    /// is allowed to use <c>Screen Wake Lock API</c> to indicate that the device should not dim or turn off the screen.
    /// Specifically, where a defined policy blocks use of this feature, <c>WakeLock.request()</c> calls will return a
    /// <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>NotAllowedError</c>.
    /// </summary>
    /// <returns>A configured <see cref="ScreenWakeLockPermissionsPolicyDirectiveBuilder"/></returns>
    public ScreenWakeLockPermissionsPolicyDirectiveBuilder AddScreenWakeLock() => AddDirective(new ScreenWakeLockPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>serial</c> directive controls whether the current document is
    /// allowed to use the <c>Web Serial API</c> to communicate with serial devices, either directly connected
    /// via a serial port, or via USB or Bluetooth devices emulating a serial port. Specifically, where a
    /// defined policy blocks use of this feature, <c>Serial.requestPort()</c> and <c>Serial.getPorts()</c> calls
    /// will return a <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>SecurityError</c>.
    /// </summary>
    /// <returns>A configured <see cref="SerialPermissionsPolicyDirectiveBuilder"/></returns>
    public SerialPermissionsPolicyDirectiveBuilder AddSerialPermissions() => AddDirective(new SerialPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// Controls access to audio output devices requested through
    /// the NavigatorUserMedia interface. If disabled then calls to
    /// <code>getUserMedia()</code> will not grant access to audio
    /// output devices in that document.
    /// </summary>
    /// <returns>A configured <see cref="SpeakerPermissionsPolicyDirectiveBuilder"/></returns>
    public SpeakerPermissionsPolicyDirectiveBuilder AddSpeaker() => AddDirective(new SpeakerPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>speaker-selection</c> directive controls whether the current
    /// document is allowed to enumerate and select audio output devices (speakers, headphones, and so on).
    /// Specifically, where a defined policy blocks use of this feature:
    /// <list type="bullet">
    /// <item><description><c>MediaDevices.enumerateDevices()</c> won't return devices of type audio output.</description></item>
    /// <item><description><c>MediaDevices.selectAudioOutput()</c> won't display the popup for
    /// selecting an audio output, and the returned <c>Promise</c> will reject with a <c>DOMException</c>
    /// of type <c>NotAllowedError</c>.</description></item>
    /// <item><description><c>HTMLMediaElement.setSinkId()</c> and <c>AudioContext.setSinkId()</c> will throw a <c>NotAllowedError</c> if called for an audio output.</description></item>
    /// </list>
    /// </summary>
    /// <returns>A configured <see cref="SpeakerSelectionPermissionsPolicyDirectiveBuilder"/></returns>
    public SpeakerSelectionPermissionsPolicyDirectiveBuilder AddSpeakerSelection() => AddDirective(new SpeakerSelectionPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>storage-access</c> directive controls whether
    /// a document loaded in a third-party context (i.e. embedded in an <c>iframe</c>) is
    /// allowed to use the <c>Storage Access API</c> to request access to unpartitioned cookies.
    /// This is relevant to user agents that by default block access to unpartitioned
    /// cookies by sites loaded in a third-party context to improve privacy
    /// (for example, to prevent tracking). Specifically, where a defined policy
    /// blocks use of this feature, <c>Document.requestStorageAccess()</c> calls will
    /// return a <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>NotAllowedError</c>.
    /// </summary>
    /// <returns>A configured <see cref="StorageAccessPermissionsPolicyDirectiveBuilder"/></returns>
    public StorageAccessPermissionsPolicyDirectiveBuilder AddStorageAccess() => AddDirective(new StorageAccessPermissionsPolicyDirectiveBuilder());

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
    /// The HTTP <c>Permissions-Policy</c> header <c>web-share</c> directive controls whether the
    /// current document is allowed to use the <c>Navigator.share()</c> method of the Web Share API
    /// to share text, links, images, and other content to arbitrary destinations of the
    /// user's choice. Specifically, where a defined policy blocks usage of this feature,
    /// <c>Navigator.share()</c> calls will return a <c>Promise</c> that rejects with a <c>DOMException</c> of type
    /// <c>NotAllowedError</c>.
    /// </summary>
    /// <returns>A configured <see cref="WebSharePermissionsPolicyDirectiveBuilder"/></returns>
    public WebSharePermissionsPolicyDirectiveBuilder AddWebShare() => AddDirective(new WebSharePermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>window-management</c> directive controls whether or not the
    /// current document is allowed to use the <c>Window Management API</c> to manage windows on multiple displays.
    /// Where this policy forbids use of the API:
    /// <list type="bullet">
    /// <item><description>The <c>Promise</c> returned by the <c>Window.getScreenDetails()</c> method will reject
    /// with a <c>NotAllowedError</c> exception.</description></item>
    /// <item><description>The <c>Window.screen.isExtended</c> property will always return
    /// <c>false</c>.</description></item>
    /// </list>
    /// </summary>
    /// <returns>A configured <see cref="WindowManagementPermissionsPolicyDirectiveBuilder"/></returns>
    public WindowManagementPermissionsPolicyDirectiveBuilder AddWindowManagement() => AddDirective(new WindowManagementPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// The HTTP <c>Permissions-Policy</c> header <c>xr-spatial-tracking</c> directive controls whether
    /// the current document is allowed to use the <c>WebXR Device API</c>.
    /// Specifically, where a defined policy blocks usage of this feature:
    /// <list type="bullet">
    /// <item><description><c>navigator.xr.isSessionSupported()</c> and <c>navigator.xr.requestSession()</c>
    /// calls will return a <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>SecurityError</c>.</description></item>
    /// <item><description><c>devicechange</c> events are not fired on the <c>navigator.xr</c> object.</description></item>
    /// </list>
    /// </summary>
    /// <returns>A configured <see cref="XrSpatialTrackingPermissionsPolicyDirectiveBuilder"/></returns>
    public XrSpatialTrackingPermissionsPolicyDirectiveBuilder AddXrSpatialTracking() => AddDirective(new XrSpatialTrackingPermissionsPolicyDirectiveBuilder());

    /// <summary>
    /// Create a custom Permissions-Policy directive for an un-implemented directive.
    ///
    /// The directive can be built using standard methods such as <see cref="PermissionsPolicyDirectiveBuilder.Self"/>
    /// and <see cref="PermissionsPolicyDirectiveBuilder.None"/>
    /// </summary>
    /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
    /// <returns>A configured <see cref="CustomPermissionsPolicyDirectiveBuilder"/></returns>
    public CustomPermissionsPolicyDirectiveBuilder AddCustomFeature(string directive) => AddDirective(new CustomPermissionsPolicyDirectiveBuilder(directive));

    /// <summary>
    /// Create a custom Permissions-Policy directive for an un-implemented directive.
    ///
    /// The directive can be built using standard methods such as <see cref="PermissionsPolicyDirectiveBuilder.Self"/>
    /// and <see cref="PermissionsPolicyDirectiveBuilder.None"/>
    /// </summary>
    /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
    /// <param name="value">The value to use for the directive, e.g. * or 'none'</param>
    /// <returns>A configured <see cref="CustomPermissionsPolicyDirectiveBuilder"/></returns>
    public CustomPermissionsPolicyDirective AddCustomFeature(string directive, string value) => AddDirective(new CustomPermissionsPolicyDirective(directive, value));

    /// <summary>
    /// Add a Permissions-Policy with recommended "secure" directives based on
    /// <see href="https://cheatsheetseries.owasp.org/cheatsheets/REST_Security_Cheat_Sheet.html#security-headers">OWASP recommendations</see>.
    /// Note that this disables many features. If you need to enable some features, add additional directives, to override
    /// the required policy.
    /// </summary>
    /// <returns>The <see cref="PermissionsPolicyBuilder"/> for method chaining</returns>
    /// <remarks>The OWASP recommended policy includes directives that are either experimental,
    /// not available by default, or not implemented. For consistency,
    /// those directives are not included in the policy.
    ///
    /// The policy added is equivalent to
    /// <code>
    /// AddAccelerometer().None();
    /// AddAmbientLightSensor().None();
    /// AddAutoplay().None();
    /// AddCamera().None();
    /// AddDisplayCapture().None();
    /// AddEncryptedMedia().None();
    /// AddFullscreen().None();
    /// AddGeolocation().None();
    /// AddGyroscope().None();
    /// AddMagnetometer().None();
    /// AddMicrophone().None();
    /// AddMidi().None();
    /// AddPayment().None();
    /// AddPictureInPicture().None();
    /// AddPublickeyCredentialsGet().None();
    /// AddScreenWakeLock().None();
    /// AddSyncXHR().None();
    /// AddUsb().None();
    /// AddWebShare().None();
    /// AddXrSpatialTracking().None();
    /// </code>
    /// </remarks>
    public PermissionsPolicyBuilder AddDefaultSecureDirectives()
    {
        // https://github.com/w3c/webappsec-permissions-policy/blob/f15a4548691ea69a87227c0f67571da2cc0e08c1/features.md?plain=1#L19
        AddAccelerometer().None();
        AddAutoplay().None();

        // AddBattery().None(); // Request: https://issues.chromium.org/issues/40100229
        AddCamera().None();

        // AddCrossOriginIsolated().None(); // experimental in chrome 85
        AddDisplayCapture().None();

        // AddDocumentDomain().None(); // retired
        AddEncryptedMedia().None();

        // AddExecutionWhileNotRendered().None(); // Behind a flag in chrome

        // AddExecutionWhileOutOfViewport().None();; // Behind a flag in chrome
        AddFullscreen().None();
        AddGeolocation().None();
        AddGyroscope().None();

        // AddKeyboardMap().None(); // Chrome only https://www.chromestatus.com/feature/5657965899022336
        AddMagnetometer().None();
        AddMicrophone().None();
        AddMidi().None();

        // AddNavigationOverride().None(); // No implementations
        AddPayment().None();
        AddPictureInPicture().None();
        AddPublickeyCredentialsGet().None();
        AddScreenWakeLock().None();
        AddSyncXHR().None();
        AddUsb().None();
        AddWebShare().None();
        AddXrSpatialTracking().None();
        return this;
    }

    private T AddDirective<T>(T directive) where T : PermissionsPolicyDirectiveBuilderBase
    {
        _directives[directive.Directive] = directive;
        return directive;
    }

    /// <summary>
    /// Builds the Permissions-Policy value.
    /// </summary>
    /// <returns>The string representing the complete Permissions-Policy value.</returns>
    internal string Build()
    {
        return string.Join(", ", _directives.Values.Select(x => x.Build()).Where(x => !string.IsNullOrEmpty(x)));
    }
}