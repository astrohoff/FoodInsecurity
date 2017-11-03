using System;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private GameObject focus;

    public void Update(){
        
    }

    private void UpdateFocus(){
        Camera.main.ScreenPointToRay(Input.mousePosition);

    }
}