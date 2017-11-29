using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    private double wallet;
    private List<FoodObject> lunchbox;

	// Use this for initialization
	public Inventory () {
        wallet = 1000.00;
        lunchbox = new List<FoodObject>();
	}

    /* Wallet manipulation internal functions */
    private void SpendMoney(double cost) { wallet -= cost; }
    private void EarnMoney(double pay) { wallet += pay; }

    /* Accessors*/
    public double getWallet() { return this.wallet; }
    public List<FoodObject> getLunchbox() { return this.lunchbox; }

    /* Behavioral Methods */
    public void BuyFood(FoodObject food) {
		double price = food.getPrice ();
        if (price < wallet) {
			this.SpendMoney(price);
            lunchbox.Add(food);
            Debug.Log("Food purchased! Current balance: " + wallet);
			Debug.Log (lunchbox);
        }
        else {
            // TODO: Handle situation when player cannot afford food
			Debug.Log("Can't afford this food. Current balance: " + wallet);
        }
    }
}
