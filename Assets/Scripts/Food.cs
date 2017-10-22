using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public float health = 2;

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Player>().ChangeHealth(health);
            Destroy(gameObject);
        }
    }
}
