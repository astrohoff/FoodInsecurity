using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Database{
	
	// Dictionaries to store food attributes. Create new dictionaries here for new food attributes
	private Dictionary<AbstractFood.foodEnum, double> foodPrices;

	public Database() {
		// ==================================================================================================
		// -- This constructor intializes dictionaries and values of known food types for the database.
		// -- New dictionary constructors should be added under the constructor section for new attributes
		// -- New Add methods should be added under the sections for each food type for new attributes.
		// -- New foods should be added to the bottom the method.
		// -- If anyone knows an easy way to do this with JSON let me know.
		// ==================================================================================================

		/* Dictionary Constructors */
		// ==================================================================================================
		foodPrices = new Dictionary<AbstractFood.foodEnum, double>();

		// ==================================================================================================

		/* Food Attribute Specification */
		// ==================================================================================================
		// Soylent Red
		foodPrices.Add(AbstractFood.foodEnum.SOYLENTRED, 10.00);

		// Soylent Green
		foodPrices.Add(AbstractFood.foodEnum.SOYLENTGREEN, 100.00);

		// Soylent Blue
		foodPrices.Add(AbstractFood.foodEnum.SOYLENTBLUE, 10000.00);

		// ==================================================================================================
	}

	/* Accessors */
	// ======================================================================================================
	public double getFoodPrice(AbstractFood.foodEnum food) { return foodPrices[food]; }

	// ======================================================================================================

	/* Mutators */
	// ======================================================================================================
	public void setFoodPrice(AbstractFood.foodEnum food, double price) { foodPrices[food] = price; }

	// ======================================================================================================

}
