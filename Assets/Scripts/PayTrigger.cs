using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayTrigger : MonoBehaviour {
    public UniversalMessageController message;
    public Collider[] blockers;
    public GameObject money;

    void OnTriggerEnter(Collider c){
        if(c.tag == "Player"){
            message.ShowMessage();
            for(int i = 0; i < blockers.Length; i++){
                blockers[i].enabled = true;
                money.SetActive(true);
            }
        }
    }
}
