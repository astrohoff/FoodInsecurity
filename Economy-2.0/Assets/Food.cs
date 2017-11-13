using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food {
    /* Attributes */
    // ======================================================================================================
	private AbstractFood.foodEnum name;
	private double price;

    // ======================================================================================================

    /* Constructor - Only called by FoodFactory */
    // ======================================================================================================
	public Food(AbstractFood.foodEnum food) {
		// Values initialized here are garbage values (except for name) - FoodFactory will set correct values.
        // That being said, its probably a good idea to still initialize the member values here,
        // so extend this method with new assignments for new attributes.
		name = food;
        price = 0.00;

    }
    // ======================================================================================================

    /* Accessors - Extend for new attributes */
    // ======================================================================================================
	public AbstractFood.foodEnum getName() { return this.name; }
	public double getPrice() { return this.price; }

    // ======================================================================================================


	/* Mutators - Extend for new attributes (name is read-only) */
    // ======================================================================================================
    public void setPrice(double val) { this.price = val; }

    // ======================================================================================================
}
