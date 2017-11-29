using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class contains item functionality for vendors.
public class Stock {
	private List<AbstractFood.foodEnum> wares;
	private int index;

	/* Constructor */
	public Stock() {
		wares = new List<AbstractFood.foodEnum>();
		index = 0;
	}

	/* Accessors */
	public void cycleUp() { index = ((index < wares.Count-1) ? index + 1 : 0); Debug.Log(index);}
	public void cycleDown() { index = ((index > 0) ? index - 1 : wares.Count-1); Debug.Log(index);}
	public AbstractFood.foodEnum getCurrent() { return wares[index]; }

	/* Mutators */
	public void pushFood(AbstractFood.foodEnum food) { wares.Add(food); }
	public void popFood(AbstractFood.foodEnum food) { wares.Remove(food); }
	public bool inStock(AbstractFood.foodEnum food) { return wares.Contains(food); }
}
