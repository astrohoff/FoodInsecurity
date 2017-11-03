using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabControl : MonoBehaviour {
    public bool isLeftHand = true;

    private GameObject grabTarget;

    void Update(){
        

    }

    private void GrabUpdate(){
        if(isLeftHand && OVRInput.GetDown(OVRInput.RawButton.LHandTrigger) ||
            !isLeftHand && OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            if(grabTarget != null){
                grabTarget.transform.SetParent(transform);
            }
        }
    }

    void OnTriggerEnter(Collider collider){
        Debug.Log("Hand triggered");
        if(collider.GetComponent<Food>() != null){
            grabTarget = collider.gameObject;
        }
    }
}
