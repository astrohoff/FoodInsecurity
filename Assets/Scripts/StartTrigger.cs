using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour {

	public UniversalMessageController dialogue;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("ENTERED");
		dialogue.ShowMessage();
		//This is for Fungus
		//flowchart.ExecuteBlock ("Start");
		if(dialogue != null){
			dialogue.ShowMessage();
		}
	}
}
