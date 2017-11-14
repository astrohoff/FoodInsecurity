using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public float health = 2;

    void OnTriggerEnter(Collider c){
        Player player = c.gameObject.GetComponent<Player>();
        if (player != null){
            player.ChangeHealth(health);
            Destroy(gameObject);
        }
    }
}
