using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour {
    public UniversalMessageController message;

    void OnTriggerEnter(Collider c){
        if(c.tag == "Player"){
            message.ShowMessage();
        }
    }
}
