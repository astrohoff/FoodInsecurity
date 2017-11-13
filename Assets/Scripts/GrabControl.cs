using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabControl : MonoBehaviour {
    public bool isLeftHand = true;
    public Transform trackingSpace;

    private GameObject grabTarget;

    void Update()
    {
        if (isLeftHand && OVRInput.GetDown(OVRInput.RawButton.LHandTrigger) ||
            !isLeftHand && OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            if (grabTarget != null)
            {
                grabTarget.transform.SetParent(transform);
                grabTarget.GetComponent<Food>().SetMode(Food.FoodMode.Edible);
            }
        }
        if (isLeftHand && OVRInput.GetUp(OVRInput.RawButton.LHandTrigger) ||
            !isLeftHand && OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            if (grabTarget != null)
            {
                grabTarget.GetComponent<Food>().SetMode(Food.FoodMode.Throwable);
                Vector3 controllerVel = Vector3.zero;
                if (isLeftHand)
                {
                    controllerVel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
                }
                else
                {
                    controllerVel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
                }
                controllerVel = trackingSpace.TransformVector(controllerVel);
                grabTarget.GetComponent<Rigidbody>().velocity = controllerVel;
                Debug.Log("Throwing with velocity " + controllerVel.ToString());
                grabTarget.transform.SetParent(null);
                grabTarget = null;
            }
        }
    }

    void OnTriggerEnter(Collider collider){
        if(collider.GetComponent<Food>() != null){
            grabTarget = collider.gameObject;
        }
    }
}
