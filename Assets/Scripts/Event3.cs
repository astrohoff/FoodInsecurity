using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3 : MonoBehaviour {
	//UniversalMessageController should have prompted already to give dialogue from Cashier's perspective.
	//The purpose of this script is to prevent the character from going through the line unless he returns the food
	public GameObject Trigger4;

	void OnTriggerEnter (Collider c)
	{
		
		//Check tags or objects in the collider
		//Make sure it only triggers when it's just the player and any multitude of food objects
		if (c.tag == "Player") 
		{ //edit later to include food tags
			Debug.Log ("Player entered trigger #3.");

			Trigger4.SetActive(true);
			Debug.Log ("Trigger #4 activated. Go sit with the people who are calling for you.");

		}
	}
        

	void OnTriggerExit (Collider c)
	{
		//Debug.Log ("OBJECT EXITED THE TRIGGER");
	}


}
