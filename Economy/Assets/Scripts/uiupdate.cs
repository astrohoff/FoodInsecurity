using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiupdate : MonoBehaviour {
	private GameObject player;
	private cash wallet;
	private Text walletText;
	private Text message;

	public bool vending;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		wallet = player.GetComponent<cash>();
		walletText = GameObject.FindWithTag("amount").GetComponent<Text>();
		message = GameObject.FindWithTag("message").GetComponent<Text>();
		walletText.text = "$" + wallet.money.ToString();
		vending = false;
	}
	
	// Update is called once per frame
	void Update () {
		walletText.text = "$" + wallet.money.ToString();
		if (vending && wallet.money >= 100.00)
			message.text = "Press Enter to buy food.";
		else if (vending && wallet.money < 100.00)
			message.text = "You cannot afford this food.";
		else
			message.text = "";
	}
}
