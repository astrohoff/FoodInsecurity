using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour {
    public enum PhysicsMode { Throwable, FixedTrigger };

    private List<Food> containedFood;

    void Start()
    {
        SetMode(PhysicsMode.FixedTrigger);
        containedFood = new List<Food>();
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.collider.GetComponent<Food>() != null)
        {
            c.collider.transform.SetParent(transform);
            Food food = c.collider.GetComponent<Food>();
            food.SetMode(Food.FoodMode.Edible);
            if(!containedFood.Contains((food))){
                containedFood.Add((food));
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<Food>() != null)
        {
            c.transform.SetParent(transform);
            Food food = c.GetComponent<Food>();
            food.SetMode(Food.FoodMode.Edible);
            if(!containedFood.Contains((food))){
                containedFood.Add((food));
            }
        }
    }

    public bool GetContainsFood(){
        for(int i = 0; i < containedFood.Count; i++){
            if (containedFood[i] != null)
                return true;
        }
        return false;
    } 

    public float GetContainedFoodCost(){
        float cost = 0;
        for(int i = 0; i < containedFood.Count; i++){
            if(containedFood[i] != null){
                cost += containedFood[i].cost;
            }
        }
        return cost;
    }

    public void SetMode(PhysicsMode mode)
    {
        if (mode == PhysicsMode.Throwable)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Collider>().isTrigger = false;
            }
            GetComponent<Rigidbody>().useGravity = true;

        }
        else if (mode == PhysicsMode.FixedTrigger)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
            }
        }
    }
}
