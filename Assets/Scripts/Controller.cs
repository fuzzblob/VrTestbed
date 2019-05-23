using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Controller : MonoBehaviour
{
    public InputDevice Device;
    List<InputFeatureUsage> usages;
    
    internal void Init() {
        usages = new List<InputFeatureUsage>();
        Device.TryGetFeatureUsages(usages);
    }

    public bool GetIsTracked() {
        bool value;
        Device.TryGetFeatureValue(CommonUsages.isTracked, out value);
        return value;
    }
    public bool GetTrigger() {
        bool value;
        Device.TryGetFeatureValue(CommonUsages.triggerButton, out value);
        return value;
    }
    public Vector3 GetPosition() {
        Vector3 value;
        Device.TryGetFeatureValue(CommonUsages.devicePosition, out value);
        return value;
    }
    public Quaternion GetRotation() {
        Quaternion value;
        Device.TryGetFeatureValue(CommonUsages.deviceRotation, out value);
        return value;
    }
    public struct X {
        //
        // Summary:
        //     The velocity of the device.
        public static InputFeatureUsage<Vector3> deviceVelocity;
        //
        // Summary:
        //     The primary face button being pressed on a device, or sole button if only one
        //     is available.
        public static InputFeatureUsage<bool> primaryButton;
        //
        // Summary:
        //     The primary face button being touched on a device.
        public static InputFeatureUsage<bool> primaryTouch;
        //
        // Summary:
        //     The secondary face button being pressed on a device.
        public static InputFeatureUsage<bool> secondaryButton;
        //
        // Summary:
        //     The secondary face button being touched on a device.
        public static InputFeatureUsage<bool> secondaryTouch;
        //
        // Summary:
        //     A binary measure of whether the device is being gripped.
        public static InputFeatureUsage<bool> gripButton;
        //
        // Summary:
        //     Represents a menu button, used to pause, go back, or otherwise exit gameplay.
        public static InputFeatureUsage<bool> menuButton;
        //
        // Summary:
        //     Represents the Primary 2D axis being clicked or otherwise depressed.
        public static InputFeatureUsage<bool> primary2DAxisClick;
        //
        // Summary:
        //     Represents the Primary 2D axis being touched.
        public static InputFeatureUsage<bool> primary2DAxisTouch;
        //
        // Summary:
        //     Represents a thumbrest or light thumb touch.
        public static InputFeatureUsage<bool> thumbrest;
        //
        // Summary:
        //     Represents the values being tracked for this device.
        public static InputFeatureUsage<InputTrackingState> trackingState;
        //
        // Summary:
        //     Represents a touch of the trigger or index finger.
        public static InputFeatureUsage<float> indexTouch;
        //
        // Summary:
        //     Represents the thumb pressing any input or feature.
        public static InputFeatureUsage<float> thumbTouch;
        //
        // Summary:
        //     Value representing the current battery life of this device.
        public static InputFeatureUsage<float> batteryLevel;
        //
        // Summary:
        //     A trigger-like control, pressed with the index finger.
        public static InputFeatureUsage<float> trigger;
        //
        // Summary:
        //     Represents the users grip on the controller.
        public static InputFeatureUsage<float> grip;
        //
        // Summary:
        //     Represents the grip pressure or angle of the index finger.
        public static InputFeatureUsage<float> indexFinger;
        //
        // Summary:
        //     Represents the grip pressure or angle of the middle finger.
        public static InputFeatureUsage<float> middleFinger;
        //
        // Summary:
        //     Represents the grip pressure or angle of the ring finger.
        public static InputFeatureUsage<float> ringFinger;
        //
        // Summary:
        //     Represents the grip pressure or angle of the pinky finger.
        public static InputFeatureUsage<float> pinkyFinger;
        //
        // Summary:
        //     The primary touchpad or joystick on a device.
        public static InputFeatureUsage<Vector2> primary2DAxis;
        //
        // Summary:
        //     A non-handed 2D axis.
        public static InputFeatureUsage<Vector2> dPad;
        //
        // Summary:
        //     A secondary touchpad or joystick on a device.
        public static InputFeatureUsage<Vector2> secondary2DAxis;
        //
        // Summary:
        //     The position of the left eye on this device.
        public static InputFeatureUsage<Vector3> leftEyePosition;
        //
        // Summary:
        //     Value representing the hand data for this device.
        public static InputFeatureUsage<Hand> handData;
        //
        // Summary:
        //     An Eyes struct containing eye tracking data collected from the device.
        public static InputFeatureUsage<Eyes> eyesData;
    }

    public void Vibrate() {
        // test haptics
        HapticCapabilities cap;
        Device.TryGetHapticCapabilities(out cap);
        if (cap.numChannels == 0)
            Debug.LogWarning("no haptic channels");
        else {
            if (cap.supportsBuffer) {
                Device.SendHapticBuffer(0, new byte[10] { 32, 32, 64, 64, 64, 127, 127, 127, 127, 127 });
            }
            else if (cap.supportsImpulse) {
                Device.SendHapticImpulse(0, .2f);
            }
            else {
                Debug.LogWarning("XR device without haptics!");
            }
        }
    }
}
