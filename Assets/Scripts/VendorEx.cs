using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorEx : Vendor {

	// Use this for initialization
	void Start () {
        // Initialize Abstract members
        base.Init();

        // Push available food enums to stock 
        this.stock.pushFood(foodEnum.SOYLENTRED);
		this.stock.pushFood(foodEnum.SOYLENTGREEN);
		this.stock.pushFood(foodEnum.SOYLENTBLUE);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
