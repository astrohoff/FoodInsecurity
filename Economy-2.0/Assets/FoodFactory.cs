using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFactory : AbstractFood {
    public Database data;

	// Use this for initialization
	void Start () {
		data = new Database ();
		Debug.Log (data);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Food makeFood(foodEnum food) {
		Debug.Log("Making " + food + "...");
        Food newFood = new Food(food);
		Debug.Log (food + " made!");

        /* Assembler Methods - 'newfood.setX(data.getX(food);' - extend for new attributes */
        // ======================================================================================================
		Debug.Log("Setting food attributes...");
        newFood.setPrice(data.getFoodPrice(food));

        // ======================================================================================================


        return newFood;
    }

}
