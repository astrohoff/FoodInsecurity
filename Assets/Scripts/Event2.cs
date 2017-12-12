using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour {
	//UniversalMessageController should have prompted already to give dialogue from Cashier's perspective.
	//The purpose of this script is to prevent the character from going through the line unless he returns the food
	public GameObject blocker1;
	public GameObject blocker2; //im too lazy and tired to figure out how to enable all 3 of them under a gameobject
	public GameObject blocker3;
	public GameObject Trigger1;

	void OnTriggerEnter (Collider c)
	{
		
		//Check tags or objects in the collider
		//Make sure it only triggers when it's just the player and any multitude of food objects
		if (c.tag == "Player") 
		{ //edit later to include food tags
			Debug.Log ("Player entered trigger #2.");
			(blocker1.GetComponent (typeof(Collider)) as Collider).enabled = false;
			(blocker2.GetComponent (typeof(Collider)) as Collider).enabled = false;
			(blocker3.GetComponent (typeof(Collider)) as Collider).enabled = false;
			Debug.Log ("End of line opened.");
			Trigger1.SetActive(false);
			Debug.Log ("Trigger #1 deactivated. You can now pass the line.");

		}
	}

	void OnTriggerStay(Collider c)
	{
		Debug.Log("User should've theoretically returned the items at this point in the line");
	}

	void OnTriggerExit (Collider c)
	{
		//Debug.Log ("OBJECT EXITED THE TRIGGER");
	}


}
