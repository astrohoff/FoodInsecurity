using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Player movement for both VR and non-VR mode.
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    // The player camera.
    public Transform cameraTransform;
    // Walking speed (in meters/second) when unaffected by fatigue.
    public float maxMoveSpeed = 1f;
    // Angle (in degrees) that the player will turn in VR mode when the turn joystick is pressed.
    // Spinning around with a joystick as you would in a non-VR FPS is uncomfortable in VR;
    // an easy fix is to turn instantaneously by some angle.
    public float snapTurnAngle = 30f;
    // Multiplier for adjusting mouselook speed in non-VR mode.
    public float mouseLookSpeed = 500;
    // Maximum angle (in degrees) that the player can look up or down in non-VR mode.
    // This prevents the player from endlessly rolling their head.
    public float mouseLookUpDownLimit = 70f;

    // The CharacterController component attached to the player.
    private CharacterController charCtrl;
    // The fraction of max speed that should be allowed given the players engery level.
    private float energySpeedScale = 1;
	
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {    
        // Movement is done in FixedUpdate instead of Update because that is where physics are updated, and movement
        // involves collisions. (AKA it's smoother.)

        // Translation movement (walking).

        // Get the axis of forward movement from the direction the player is looking.
        Vector3 moveForwardDir = cameraTransform.forward;
        // Remove the y component so that the player can't walk up or down.
        moveForwardDir.y = 0;
        // Normalize direction to length of 1 so that the removed y component doesn't affect later calculations.
        moveForwardDir.Normalize();
        // Get sideways movement axis with same steps as above.
        Vector3 moveSidewaysDir = cameraTransform.right;
        moveSidewaysDir.y = 0;
        moveSidewaysDir.Normalize();

        // Get forward and sideways controller inputs.
        Vector2 moveInput;
        // VR mode.
        if (XRDevice.isPresent)
        {
            moveInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        }
        // Non-VR mode.
        else
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        // Calculate actual movement vector.
        Vector3 movement = moveForwardDir * moveInput.y + moveSidewaysDir * moveInput.x;
        movement *= maxMoveSpeed * energySpeedScale;
        // Apply movement.
        charCtrl.SimpleMove(movement);

        
    }

    void Update()
    {
        // Rotation can be done in normal update since it doesn't involve physics.

        // Rotation movement.
        // VR mode.
        if (XRDevice.isPresent)
        {
            // Rotate player left if stick pressed left.
            if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
            {
                // VR head movement allows the camera to be offcenter reletive to the player GameObject,
                // which would make the camera move in a circle when the player GameObject rotates.
                // Rotating the player GameObject around the camera keeps the center of rotation at the camera.
                transform.RotateAround(cameraTransform.position, Vector3.up, -snapTurnAngle);
            }
            else if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
            {
                transform.RotateAround(cameraTransform.position, Vector3.up, snapTurnAngle);
            }
        }
        // Non-VR mode.
        else
        {
            // Disable mouselook with left control key, else apply mouselook.
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                // Get mouselook input.
                Vector2 lookInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
                // Scale input by speed * frame time.
                Vector2 look = lookInput * mouseLookSpeed * Time.deltaTime;
                // Get the axis of rotation for our input.
                // This is the reciprical of the look input.
                Vector3 rotationAxis = -cameraTransform.right * look.y + Vector3.up * look.x;
                float rotationAngle = look.magnitude;
                // Rotate camera.
                cameraTransform.Rotate(rotationAxis, rotationAngle, Space.World);

                // Make sure the camera isn't rolling or outside up/down limit.
                Vector3 constrainedAngles = cameraTransform.localEulerAngles;
                constrainedAngles.z = 0;
                // Wierd pitch angles. 0/360 = forward, 90 = down, 270 = up.
                if (constrainedAngles.x < 180 && constrainedAngles.x > mouseLookUpDownLimit)
                {
                    constrainedAngles.x = mouseLookUpDownLimit;
                }
                else if (constrainedAngles.x > 180 && constrainedAngles.x < 360 - mouseLookUpDownLimit)
                {
                    constrainedAngles.x = 360 - mouseLookUpDownLimit;
                }
                cameraTransform.localEulerAngles = constrainedAngles;
            }
        }
    }
}
