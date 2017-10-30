using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {
    private const float VrFramerate = 90;

    public Transform playerHead;
    // Movement variables.
    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;
    public float mouseLookSpeed = 1f;
    public float mouseLookMinPitchAngle = -70;
    public float mouseLookMaxPitchAngle = 70;

    public float normalizedEnergy = 1;
    public float exertionMaxSpeed = 2;
    public float energyDepletionSpeed = 0.1f;
    public float energyRegenSpeed = 0.06f;
    public float maxRegenEnergy = 0.5f;
    public float maxHealth = 10f;
    public HealthBar healthBar;
    // Movement cost in health points per meter.
    //public float movementCost = 0.5f;
    // Distance that the player collider must be from the player head before it recenters itself (for VR).
    public float colliderAlignmentThresh = 0.1f;
    public BlurOptimized blur;
    public float blurEnergyStart = 0.5f;
    public float maxBlur = 4;

    private float nonVrGraphicsFramerate = 60;
    private float nonVRPhysicsFramerate = 60;
    private CharacterController charCtrl;
    //private float currentHealth;
    private Vector3 previousPosition;
    private float mouseLookYaw, mouseLookPitch;

	// Use this for initialization
	void Start () {
        SetTargetFramerates();
        charCtrl = GetComponent<CharacterController>();
        //currentHealth = maxHealth;
        previousPosition = playerHead.position;
	}
	
	// Update is called once per frame
	void Update () {
        //ApplyMovementCost();
        UpdateEnegery();
        if (XRDevice.isPresent)
        {
            CenterColliderOnPlayerHead();
        }
        previousPosition = playerHead.position;
        healthBar.SetNormalizedHealth(normalizedEnergy);
        UpdateBlur();
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

    /*void ApplyMovementCost(){
        // Reduce health based on amount of movement.
        float distanceMoved = (playerHead.position - previousPosition).magnitude;
        // Ignore any jumps.
        if(distanceMoved < 0.5f){
            ChangeHealth(-movementCost * distanceMoved);
        }
    }*/

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
        Vector3 finalMovement = (forward * forwardMovement + right * sideMovement) * normalizedEnergy;
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
        //currentHealth += (deltaHealth;
        //currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        normalizedEnergy += deltaHealth / maxHealth;
        normalizedEnergy = Mathf.Clamp01(normalizedEnergy);
    }

    public void CenterColliderOnPlayerHead(){
        Vector3 playerXZ = new Vector3(playerHead.localPosition.x, 0, playerHead.localPosition.z);
        Vector3 colliderXZ = new Vector3(charCtrl.center.x, 0, charCtrl.center.z);
        if((colliderXZ - playerXZ).magnitude > 0.1f){
            charCtrl.center = new Vector3(playerHead.localPosition.x, charCtrl.center.y, playerHead.localPosition.z);
        }
    }

    public float GetNormalizedExertion(){
        float distanceMoved = (playerHead.position - previousPosition).magnitude;
        // Ignore any jumps.
        if(distanceMoved > 0.5f){
            distanceMoved = 0;
        }
        float playerSpeed = distanceMoved / Time.deltaTime;
        float movementExertion = Mathf.Clamp01(playerSpeed / exertionMaxSpeed);

        if (Time.frameCount % 60 == 0)
        {
            Debug.Log("Exertion: " + movementExertion);
        }

        return movementExertion;        
    }

    public void UpdateEnegery(){
        float exertion = GetNormalizedExertion();
        float depletion = exertion * energyDepletionSpeed * Time.deltaTime;
        float regen = (1 - exertion) * energyRegenSpeed * Time.deltaTime;
        if (normalizedEnergy < maxRegenEnergy)
        {
            if ((normalizedEnergy + regen) > maxRegenEnergy)
            {
                normalizedEnergy = maxRegenEnergy;
            }
            else
            {
                normalizedEnergy += regen;
            }
        }
        normalizedEnergy -= depletion;
        normalizedEnergy = Mathf.Clamp01(normalizedEnergy);
    }

    public void UpdateBlur(){
        if (normalizedEnergy < blurEnergyStart)
        {
            float blurSize = (1 - normalizedEnergy / blurEnergyStart) * maxBlur;
            blur.blurSize = blurSize;
            blur.blurIterations = 2;
        }
        else
        {
            blur.blurSize = 0;
            blur.blurIterations = 1;
        }
    }
}
