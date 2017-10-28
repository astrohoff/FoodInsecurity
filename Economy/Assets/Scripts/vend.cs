using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vend : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Player") {
			BroadcastMessage ("vendProx", 0, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
