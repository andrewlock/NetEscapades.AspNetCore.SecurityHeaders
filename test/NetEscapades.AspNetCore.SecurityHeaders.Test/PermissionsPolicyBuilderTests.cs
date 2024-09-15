using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

public class PermissionsPolicyBuilderTests
{
    [Fact]
    public void PermissionsPolicy_Build_WhenNoValues_Returns()
    {
        var builder = new PermissionsPolicyBuilder();

        var result = builder.Build();

        result.Should().BeNullOrEmpty();
    }

    [Fact]
    public void PermissionsPolicy_Build_AddSelfOnly_ReturnsOnlySelf()
    {
        var builder = new PermissionsPolicyBuilder();

        builder.AddAccelerometer()
            .Self();

        var result = builder.Build();

        result.Should().Be("accelerometer=self");
    }

    [Fact]
    public void PermissionsPolicy_Build_AddAccelerometer_WhenAddsMultipleValue_ReturnsAllValues()
    {
        var builder = new PermissionsPolicyBuilder();

        builder.AddAccelerometer()
            .Self()
            .For("http://testUrl.com").For("https://testUrl2.se");

        var result = builder.Build();

        result.Should().Be("accelerometer=(self \"http://testUrl.com\" \"https://testUrl2.se\")");
    }

    [Fact]
    public void PermissionsPolicy_Build_AddAccelerometer_WhenIncludesNone_OnlyWritesNone()
    {
        var builder = new PermissionsPolicyBuilder();
        builder.AddAccelerometer()
            .Self()
            .For("http://testUrl.com")
            .None();

        var result = builder.Build();

        result.Should().Be("accelerometer=()");
    }

    [Fact]
    public void PermissionsPolicy_Build_AddFLoC_Calculation_WhenIncludesNone_OnlyWritesNone()
    {
        var builder = new PermissionsPolicyBuilder();
        builder.AddFederatedLearningOfCohortsCalculation()
            .None();

        var result = builder.Build();

        result.Should().Be("interest-cohort=()");
    }

    [Fact]
    public void PermissionsBuild_AddAccelerometer_WhenIncludesAll_OnlyWritesAll()
    {
        var builder = new PermissionsPolicyBuilder();
        builder.AddAccelerometer()
            .Self()
            .For("http://testUrl.com")
            .All();

        var result = builder.Build();

        result.Should().Be("accelerometer=*");
    }

    [Fact]
    public void PermissionsPolicy_Build_AddAccelerometer_WhenIncludesAllAndNone_ThrowsInvalidOperationException()
    {
        var builder = new PermissionsPolicyBuilder();
        builder.AddAccelerometer()
            .None()
            .All();

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void PermissionsPolicy_Build_CustomPermissionsPolicyDirective_AddsValues()
    {
        var builder = new PermissionsPolicyBuilder();
        builder.AddCustomFeature("push", string.Empty);
        builder.AddCustomFeature("vibrate", "*");
        builder.AddCustomFeature("something-else", string.Empty);

        var result = builder.Build();

        result.Should().Be("push=(), vibrate=*, something-else=()");
    }

    [Fact]
    public void PermissionsPolicy_Build_WhenHasAllPermissions_HasValues()
    {
        var builder = new PermissionsPolicyBuilder();

        builder.AddAccelerometer().None();
        builder.AddAmbientLightSensor().None();
        builder.AddAttributionReporting().None();
        builder.AddAutoplay().None();
        builder.AddBluetooth().None();
        builder.AddCamera().None();
        builder.AddDisplayCapture().None();
        builder.AddEncryptedMedia().None();
        builder.AddFederatedLearningOfCohortsCalculation().None();
        builder.AddFullscreen().None();
        builder.AddGeolocation().None();
        builder.AddGyroscope().None();
        builder.AddHid().None();
        builder.AddIdentityCredentialsGet().None();
        builder.AddIdleDetection().None();
        builder.AddLocalFonts().None();
        builder.AddMagnetometer().None();
        builder.AddMicrophone().None();
        builder.AddMidi().None();
        builder.AddOtpCredentials().None();
        builder.AddPayment().None();
        builder.AddPictureInPicture().None();
        builder.AddPublickeyCredentialsCreate().None();
        builder.AddPublickeyCredentialsGet().None();
        builder.AddScreenWakeLock().None();
        builder.AddSerialPermissions().None();
        builder.AddSpeaker().None();
        builder.AddSpeakerSelection().None();
        builder.AddStorageAccess().None();
        builder.AddSyncXHR().None();
        builder.AddUsb().None();
        builder.AddVR().None();
        builder.AddWebShare().None();
        builder.AddWindowManagement().None();
        builder.AddXrSpatialTracking().None();

        var result = builder.Build();

        result.Should().Be("accelerometer=(), ambient-light-sensor=(), attribution-reporting=(), autoplay=(), bluetooth=()," +
            " camera=(), display-capture=(), encrypted-media=(), interest-cohort=(), fullscreen=(), geolocation=(), gyroscope=(), hid=()," +
            " identity-credentials-get=(), idle-detection=(), local-fonts=(), magnetometer=(), microphone=(), midi=(), otp-credentials=()," +
            " payment=(), picture-in-picture=(), publickey-credentials-create=(), publickey-credentials-get=(), screen-wake-lock=()," +
            " serial=(), speaker=(), speaker-selection=(), storage-access=(), sync-xhr=(), usb=(), vr=(), web-share=(), window-management=(), xr-spatial-tracking=()");
    }

    [Fact]
    public void PermissionsPolicy_Build_CustomPermissionsPolicyBuilder_AddsValues()
    {
        var builder = new PermissionsPolicyBuilder();
        builder.AddCustomFeature("push").None();
        builder.AddCustomFeature("vibrate").All();

        var result = builder.Build();

        result.Should().Be("push=(), vibrate=*");
    }

    [Fact]
    public void PermissionsPolicy_Build_AddingTheSameDirectiveTwice_OverwritesThePreviousCopy()
    {
        var builder = new PermissionsPolicyBuilder();
        builder.AddAccelerometer().Self();
        builder.AddAccelerometer().None();

        var result = builder.Build();

        result.Should().Be("accelerometer=()");
    }

    [Fact]
    public void PermissionsPolicy_DefaultSecure_IsEquivalent()
    {
        // https://github.com/w3c/webappsec-permissions-policy/blob/f15a4548691ea69a87227c0f67571da2cc0e08c1/features.md?plain=1#L19
        var builder = new PermissionsPolicyBuilder();
        builder.AddAccelerometer().None();
        builder.AddAmbientLightSensor().None();
        builder.AddAutoplay().None();
        // builder.AddBattery().None(); // Request: https://issues.chromium.org/issues/40100229
        builder.AddCamera().None();
        // builder.AddCrossOriginIsolated().None(); // experimental in chrome 85
        builder.AddDisplayCapture().None();
        // builder.AddDocumentDomain().None(); // retired
        builder.AddEncryptedMedia().None();
        // builder.AddExecutionWhileNotRendered().None(); // Behind a flag in chrome
        // builder.AddExecutionWhileOutOfViewport().None();; // Behind a flag in chrome
        builder.AddFullscreen().None();
        builder.AddGeolocation().None();
        builder.AddGyroscope().None();
        // builder.AddKeyboardMap().None(); // Chrome only https://www.chromestatus.com/feature/5657965899022336
        builder.AddMagnetometer().None();
        builder.AddMicrophone().None();
        builder.AddMidi().None();
        // builder.AddNavigationOverride().None(); // No implementations
        builder.AddPayment().None();
        builder.AddPictureInPicture().None();
        builder.AddPublickeyCredentialsGet().None();
        builder.AddScreenWakeLock().None();
        builder.AddSyncXHR().None();
        builder.AddUsb().None();
        builder.AddWebShare().None();
        builder.AddXrSpatialTracking().None();

        var expected = builder.Build();
        
        PermissionsPolicyHeaderExtensions.DefaultSecurePolicy.Should().Be(expected);
    }
}