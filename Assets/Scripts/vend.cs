using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vend : MonoBehaviour {
	private uiupdate ui;
	private cash wallet;

	// Use this for initialization
	void Start () {
		ui = GameObject.FindWithTag ("hud").GetComponent<uiupdate> ();
		wallet = GameObject.FindWithTag("Player").GetComponent<cash>();
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			ui.vending = true;
		}
	}
	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "Player") {
			ui.vending = true;
			if (Input.GetKeyDown ("return") && wallet.money >= 100.00) {
				wallet.money -= 100.00;
			}
		}
	}
	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			ui.vending = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
