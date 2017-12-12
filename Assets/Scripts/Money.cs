using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {
    public float amount = 2f;
    private PhysicsMode mode = PhysicsMode.Floating;
    public float bobDistance = 0.1f;
    public float bobSpeed = 0.5f;

    private float initY;

    public enum PhysicsMode { Throwable, Grabbed, Floating };
    void Start(){
        initY = transform.position.y;
    }

    void Update(){
        if(mode == PhysicsMode.Floating){
            Vector3 newPos = transform.position;
            newPos.y = initY + Mathf.Sin(Time.realtimeSinceStartup * bobSpeed) * bobDistance;
            transform.position = newPos;
        }
    }

    public void SetMode(PhysicsMode mode)
    {
        this.mode = mode;
        if(mode == PhysicsMode.Throwable)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().useGravity = true;

        }
        else if(mode == PhysicsMode.Grabbed)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
        }
    }
}
