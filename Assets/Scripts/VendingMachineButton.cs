using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineButton : MonoBehaviour {

    public float pressDistance = 0.02f;
    public GameObject foodPrefab;
    public Transform spawnAnchor;

    private Rigidbody rb;
    private Vector3 initPos;
    private Vector3 maxPos;
    private bool pressRegistered = false;
    private List<Collider> pressingColliders;
    

	void Start () {
        pressingColliders = new List<Collider>();
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        maxPos = Vector3.zero;
	}
	
	void FixedUpdate () {
        float distanceFromIdle = (rb.position - initPos).magnitude;
        if (distanceFromIdle >= pressDistance)
        {
            if (!pressRegistered)
            {
                pressRegistered = true;
                OnPressed();
            }
            if (maxPos == Vector3.zero)
            {
                maxPos = rb.position;
            }
            else
            {
                rb.position = maxPos;
            }
        }
        else
        {
            pressRegistered = false;
        }
	}

    void OnCollisionEnter(Collision c)
    {
        pressingColliders.Add(c.collider);
    }

    void OnCollisionExit(Collision c)
    {
        pressingColliders.Remove(c.collider);
        if(pressingColliders.Count == 0)
        {
            pressRegistered = false;
        }
    }

    private void OnPressed()
    {
        Debug.Log(name + " pressed");
        GameObject food = Instantiate(foodPrefab);
        food.transform.position = spawnAnchor.position;
        Rigidbody foodRb = food.GetComponent<Rigidbody>();
        if(foodRb != null)
        {
            foodRb.useGravity = false;
        }
    }
}
