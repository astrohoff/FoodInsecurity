using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Miscellaneous player things.
public class PlayerMiscellaneous : MonoBehaviour {

	void Awake () {
        if (XRDevice.isPresent)
        {
            Debug.Log("Starting in VR mode.");
            // Set physics update rate for VR mode.
            Time.fixedDeltaTime = 1 / 90f;
        }
        else
        {
            Debug.Log("Starting in non-VR mode.");
            // Set physics update rate for non-VR mode.
            Time.fixedDeltaTime = 1 / 60f;
        }
	}
	
	void Update () {
        if (XRDevice.isPresent)
        {
            // Recenter player's view if recentering buttons are pressed.
            if (ShouldRecenter())
            {
                InputTracking.Recenter();
            }
        }
	}

    // Check if buttons for recentering are pressed.
    private bool ShouldRecenter()
    {
        // Recenter the player's view when both joysticks are pressed down.
        // Only want to recenter on the frame that the 2nd joystick is pressed.

        // If left stick held down and right stick just pressed...
        if (OVRInput.Get(OVRInput.RawButton.LThumbstick) && OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            return true;
        }
        // If right stick held down and left stick just pressed...
        else if (OVRInput.Get(OVRInput.RawButton.RThumbstick) && OVRInput.GetDown(OVRInput.RawButton.LThumbstick))
        {
            return true;
        }
        // Conditions not satisfied, don't recenter.
        return false;
    }
}
