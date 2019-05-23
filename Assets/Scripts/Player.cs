using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Camera camera;

    public GameObject Prefab;
    Controller Head;
    public Controller LeftHand, RightHand;

    private void Awake() {
        // Singleton Setup
        if (Instance != null && Instance != this) {
            if (Application.isPlaying)
                Destroy(this);
            else
                DestroyImmediate(this);
        }
        else {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
    
    void Start()
    {
        camera = camera != null ? camera : GetComponent<Camera>();
        XRSettings.gameViewRenderMode = GameViewRenderMode.BothEyes;
        Debug.Log("XR loaded device: " + XRSettings.loadedDeviceName);

        InitializeVRDevices();
    }

    IEnumerator VibrateTimer() {
        var wait = new WaitForSeconds(1f);
        while (true) {
            yield return wait;
            LeftHand?.Vibrate();
            yield return wait;
            RightHand?.Vibrate();
        }
    }

    public float gpuTime;
    void Update() {
        // hands
        if (LeftHand) {
            if (LeftHand.GetIsTracked() && LeftHand.GetTrigger())
                LeftHand.Vibrate();
            LeftHand.transform.localPosition = LeftHand.GetPosition();
            LeftHand.transform.localRotation = LeftHand.GetRotation();
        }
        if (RightHand) {
            if (RightHand.GetIsTracked() && RightHand.GetTrigger())
                RightHand.Vibrate();
            RightHand.transform.localPosition = RightHand.GetPosition();
            RightHand.transform.localRotation = RightHand.GetRotation();
        }
    }

    private void InitializeVRDevices() {
        // TODO: do a callback for when devices change

        // log inputs
        List<InputDevice> inputDevices = new List<InputDevice>();
        // handle devices
        InputDevices.GetDevices(inputDevices);
        foreach (InputDevice device in inputDevices) {
            List<InputFeatureUsage> usages = new List<InputFeatureUsage>();
            if (device.TryGetFeatureUsages(usages)) {
                //foreach(var usage in usages) {
                //    device.TryGetFeatureValue()
                //}
            }

            switch (device.role) {
                case InputDeviceRole.Generic: {
                        Debug.Log("XR Input: " + device.name);
                        var go = Instantiate(Prefab);
                        go.name = go.name + "_" + device.role;

                        var controller = go.GetComponent<Controller>();
                        controller.Device = device;
                        Head = controller;
                    }
                    break;
                case InputDeviceRole.LeftHanded:
                case InputDeviceRole.RightHanded: {
                        var go = Instantiate(Prefab, transform);
                        go.name = go.name + "_" + device.role;

                        var controller = go.GetComponent<Controller>();
                        controller.Device = device;
                        if (device.role == InputDeviceRole.LeftHanded
                            || device.name.Contains("Left")) {
                            LeftHand = controller;
                        }
                        else if (device.role == InputDeviceRole.RightHanded
                            || device.name.Contains("Right")) {
                            RightHand = controller;
                        }
                    }
                    break;
                case InputDeviceRole.TrackingReference:
                    Debug.Log("tracker detected: " + device.name);
                    break;
                case InputDeviceRole.HardwareTracker:
                    if (device.name.Contains("Knuckles")) {
                        goto case InputDeviceRole.LeftHanded;
                    }
                    goto case InputDeviceRole.Unknown;
                case InputDeviceRole.GameController:
                case InputDeviceRole.Unknown:
                case InputDeviceRole.LegacyController:
                    Debug.LogError(string.Format("unhandeled input device found with name '{0}' and role '{1}'",
                      device.name, device.role.ToString()));
                    break;
            }
        }
    }
}
