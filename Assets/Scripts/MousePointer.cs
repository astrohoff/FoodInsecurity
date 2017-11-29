using System;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private GameObject focus;

    public void Update(){
        UpdateFocus();
        TryFoodClick();
    }

    private void UpdateFocus(){
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(pointRay, out hitInfo))
        {
            focus = hitInfo.collider.gameObject;
        }
        else
        {
            focus = null;
        }
    }

    private void TryFoodClick(){
        if(Input.GetMouseButtonDown(0)){
            if(focus != null && focus.GetComponent<Food>() != null){
                GetComponent<Player>().ChangeHealth(focus.GetComponent<Food>().health);
                Destroy(focus);
                focus = null;
            }
        }
    }
}