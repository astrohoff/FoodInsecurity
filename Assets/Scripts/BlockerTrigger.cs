using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerTrigger : MonoBehaviour {
    public Collider[] blockers;
	
    void OnTriggerEnter(Collider c){
        if(c.tag == "Player"){
            for(int i = 0; i < blockers.Length; i++){
                blockers[i].enabled = true;
            }
        }
    }
}
