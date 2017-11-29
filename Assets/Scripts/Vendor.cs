using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Vendor : AbstractFood
{
    // Vendor knows 2 objects: FoodFactory and Player
    protected FoodFactory factory;
    protected PlayerScript player;
	protected Stock stock;

    // Use this for initialization - wares list populated in vendor implementations
    public void Init()
    {
        this.factory = GameObject.FindWithTag("GameController").GetComponent<FoodFactory>();
        this.player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
		this.stock = new Stock();
    }
		
	public void Vend(foodEnum food) {
		FoodObject newFood = factory.makeFood(food);
		Debug.Log ("Food created, attempting to sell to player...");
		player.BuyFood(newFood);
	}

	public foodEnum stockCycleUp() {
		stock.cycleUp();
		return stock.getCurrent();
	}
	public foodEnum stockCycleDown() {
		stock.cycleDown();
		return stock.getCurrent();
	}

    /* Abstract Methods */

}
