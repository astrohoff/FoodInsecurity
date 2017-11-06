using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public float health = 2;

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Player"){
            c.gameObject.GetComponent<Player>().ChangeHealth(health);
            Destroy(gameObject);
        }
    }
}
