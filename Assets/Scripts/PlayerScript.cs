using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private Inventory inventory;

	// For testing purposes only
	// ------------------------------------------
	private VendorEx vendor;
	private AbstractFood.foodEnum cur;
	// ------------------------------------------


	// Use this for initialization
	void Start () {
        inventory = new Inventory();
		vendor = GameObject.FindWithTag("VendorTest").GetComponent<VendorEx>();
	}
	
	// Update is called once per frame
	void Update () {
		// For testing purposes only
		// ------------------------------------------
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			cur = vendor.stockCycleUp();
			Debug.Log(cur + " selected.");
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			cur = vendor.stockCycleDown();
			Debug.Log(cur + " selected.");
		} else if (Input.GetKeyDown(KeyCode.Return)) {
			Debug.Log("Buying " + cur + "...");
			vendor.Vend(cur);
		}
		// ------------------------------------------

	}

    public void BuyFood(FoodObject food) { inventory.BuyFood(food); }
}
