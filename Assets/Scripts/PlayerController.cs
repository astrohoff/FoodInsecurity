using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;

    private CharacterController charCtrl;
	// Use this for initialization
	void Start () {
        charCtrl = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        ProcessGamepadInput(true);
    }

    void ProcessGamepadInput(bool inFixedUpdate = false)
    {
        // Should we use normal update time or fixed update time.
        // Fixed update seems smoothing, haven't noticed this before...
        float deltaTime;
        if (inFixedUpdate)
        {
            deltaTime = Time.fixedDeltaTime;
        }
        else
        {
            deltaTime = Time.deltaTime;
        }

        // Translation.
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();
        float forwardMovement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y * moveSpeed * deltaTime;
        float sideMovement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x * moveSpeed * deltaTime;
        charCtrl.SimpleMove(forward * forwardMovement + right * sideMovement);

        // Rotation.
        float rotation = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x * rotateSpeed * deltaTime;
        transform.RotateAround(Camera.main.transform.position, Vector3.up, rotation);
    }
}
