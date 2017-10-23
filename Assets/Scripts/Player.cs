using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Transform playerHead;
    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;
    public float maxHealth = 10;
    public float minHealth = 1;
    // Movement cost in health points per meter.
    public float movementCost = 0.5f;

    private CharacterController charCtrl;
    private float currentHealth;
    private Vector3 previousPosition;
    private List<GameObject> collidingObjects;

	// Use this for initialization
	void Start () {
        collidingObjects = new List<GameObject>();
        charCtrl = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        previousPosition = playerHead.position;
	}
	
	// Update is called once per frame
	void Update () {
        ApplyMovementCost();
        CenterColliderOnPlayerHead();
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
        Vector3 finalMovement = (forward * forwardMovement + right * sideMovement) * (currentHealth / maxHealth);
        charCtrl.SimpleMove(finalMovement);

        // Rotation.
        float rotation = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x * rotateSpeed * deltaTime;
        transform.RotateAround(Camera.main.transform.position, Vector3.up, rotation);
    }

    public void ChangeHealth(float deltaHealth){
        currentHealth += deltaHealth;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    public void CenterColliderOnPlayerHead(){
        if(collidingObjects.Count == 0){
            charCtrl.center = new Vector3(playerHead.localPosition.x, charCtrl.center.y, playerHead.localPosition.z);
        }
    }

    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag != "Ground"){
            collidingObjects.Add(c.gameObject);
        }
    }

    void OnCollisionExit(Collision c){
        collidingObjects.Remove(c.gameObject);
    }
}
