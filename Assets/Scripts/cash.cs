using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cash : MonoBehaviour {
	public double money;

	// Use this for initialization
	void Start () {
		initMoney ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initMoney () {
		this.money = 1000.00;
	}

	void vendProx() {
		Debug.Log ("Vending Proximity");
	}
}
