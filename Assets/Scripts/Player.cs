﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour {
    private const float VrFramerate = 90;

    public Transform playerHead;
    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;
    public float mouseLookSpeed = 1f;
    public float mouseLookMinPitchAngle = -70;
    public float mouseLookMaxPitchAngle = 70;
    public float maxHealth = 10;
    public float minHealth = 1;
    // Movement cost in health points per meter.
    public float movementCost = 0.5f;
    public float colliderAlignmentThresh = 0.1f;
    public float nonVrGraphicsFramerate = 60;
    public float nonVRPhysicsFramerate = 60;

    private CharacterController charCtrl;
    private float currentHealth;
    private Vector3 previousPosition;
    private float mouseLookYaw, mouseLookPitch;

	// Use this for initialization
	void Start () {
        SetTargetFramerates();
        charCtrl = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        previousPosition = playerHead.position;
	}
	
	// Update is called once per frame
	void Update () {
        ApplyMovementCost();
        if (XRDevice.isPresent)
        {
            CenterColliderOnPlayerHead();
        }
	}

    void SetTargetFramerates(){
        if (XRDevice.isPresent)
        {
            Application.targetFrameRate = (int)VrFramerate;
            Time.fixedDeltaTime = 1 / VrFramerate;
        }
        else
        {
            Application.targetFrameRate = (int)nonVrGraphicsFramerate;
            Time.fixedDeltaTime = 1 / nonVRPhysicsFramerate;
        }
    }

    void ApplyMovementCost(){
        // Reduce health based on amount of movement.
        float distanceMoved = (playerHead.position - previousPosition).magnitude;
        // Ignore any jumps.
        if(distanceMoved < 0.5f){
            ChangeHealth(-movementCost * distanceMoved);
        }
        previousPosition = playerHead.position;
    }

    void FixedUpdate()
    {
        ProcessMovementInput(true);
    }

    void ProcessMovementInput(bool inFixedUpdate = false)
    {
        // Should we use normal update time or fixed update time.
        // Fixed update seems smoothing, haven't noticed this before...
        float deltaTime = GetUpdateTime(inFixedUpdate);

        // Translation.
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();
        float forwardMovement = GetForwardMovementInput() * moveSpeed * deltaTime;
        float sideMovement = GetSideMovementInput() * moveSpeed * deltaTime;
        Vector3 finalMovement = (forward * forwardMovement + right * sideMovement) * (currentHealth / maxHealth);
        charCtrl.SimpleMove(finalMovement);

        // Rotation.
        if (XRDevice.isPresent)
        {
            float rotation = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x * rotateSpeed * deltaTime;
            transform.RotateAround(Camera.main.transform.position, Vector3.up, rotation);
        }
        else
        {
            ProcessMouseLook(inFixedUpdate);
        }

    }

    private void ProcessMouseLook(bool isInFixedUpdate){
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return;
        }
        float deltaLookX = Input.GetAxisRaw("Mouse X") * mouseLookSpeed * GetUpdateTime(isInFixedUpdate);
        float deltaLookY = -Input.GetAxisRaw("Mouse Y") * mouseLookSpeed * GetUpdateTime(isInFixedUpdate);

        Vector3 rotAxis = playerHead.right * deltaLookY + Vector3.up * deltaLookX;
        float angle = new Vector2(deltaLookX, deltaLookY).magnitude;
        playerHead.Rotate(rotAxis, angle, Space.World);

        Vector3 constrainedAngles = playerHead.localEulerAngles;
        constrainedAngles.z = 0;
        //constrainedAngles.x = Mathf.Clamp(constrainedAngles.x, mouseLookMinPitchAngle, mouseLookMaxPitchAngle);
        playerHead.localEulerAngles = constrainedAngles;
    }

    private float GetUpdateTime(bool isFixed){
        if (isFixed)
        {
            return Time.fixedDeltaTime;
        }
        else
        {
            return Time.deltaTime;
        }
    }

    private float GetForwardMovementInput(){
        if (XRDevice.isPresent)
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
        }
        else
        {
            return Input.GetAxisRaw("Vertical");
        }
    }

    private float GetSideMovementInput(){
        if (XRDevice.isPresent)
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        }
        else
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }

    public void ChangeHealth(float deltaHealth){
        currentHealth += deltaHealth;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    public void CenterColliderOnPlayerHead(){
        Vector3 playerXZ = new Vector3(playerHead.localPosition.x, 0, playerHead.localPosition.z);
        Vector3 colliderXZ = new Vector3(charCtrl.center.x, 0, charCtrl.center.z);
        if((colliderXZ - playerXZ).magnitude > 0.1f){
            charCtrl.center = new Vector3(playerHead.localPosition.x, charCtrl.center.y, playerHead.localPosition.z);
        }
    }
}
