using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour {
    public enum PhysicsMode { Throwable, FixedTrigger };

    void Start()
    {
        SetMode(PhysicsMode.FixedTrigger);
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.collider.GetComponent<Food>() != null)
        {
            c.collider.transform.SetParent(transform);
            c.collider.GetComponent<Food>().SetMode(Food.FoodMode.Edible);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<Food>() != null)
        {
            c.transform.SetParent(transform);
            c.GetComponent<Food>().SetMode(Food.FoodMode.Edible);
        }
    }

    public void SetMode(PhysicsMode mode)
    {
        if (mode == PhysicsMode.Throwable)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Collider>().isTrigger = false;
            }
            GetComponent<Rigidbody>().useGravity = true;

        }
        else if (mode == PhysicsMode.FixedTrigger)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
            }
        }
    }
}
