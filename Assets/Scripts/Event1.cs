using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour {
	//UniversalMessageController should have prompted already to give dialogue from Cashier's perspective.
	//The purpose of this script is to prevent the character from going through the line unless he returns the food

	public GameObject blocker1;
	public GameObject blocker2; //im too lazy and tired to figure out how to enable all 3 of them under a gameobject
	public GameObject blocker3;
	public GameObject Trigger2;
    public GameObject[] additionalBlockers;

	void OnTriggerEnter (Collider c)
	{
		
		//Check tags or objects in the collider
		//Make sure it only triggers when it's just the player and any multitude of food objects
		if (c.tag == "Player") 
		{ //edit later to include food tags
			Debug.Log ("Player entered trigger #1.");
			(blocker1.GetComponent (typeof(Collider)) as Collider).enabled = true;
			(blocker2.GetComponent (typeof(Collider)) as Collider).enabled = true;
			(blocker3.GetComponent (typeof(Collider)) as Collider).enabled = true;
            for(int i = 0; i < additionalBlockers.Length; i++){
                (additionalBlockers[i].GetComponent (typeof(Collider)) as Collider).enabled = true;
            }
			Debug.Log ("Player is now blocked from leaving the line.");
			Trigger2.SetActive(true);
			Debug.Log ("Trigger #2 activated.");
		}
			
	}

	void OnTriggerStay(Collider c)
	{
		//logic to check if tray is empty if time?
		Debug.Log("Player should go back and return his food items (there's a trigger at the end of the line to catch)");
	
	}

	void OnTriggerExit (Collider c)
	{Debug.Log ("OBJECT EXITED THE TRIGGER");}


}
