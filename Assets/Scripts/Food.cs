using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public float health = 2;
    public Color pointHighlight, pressHighlight, selectedHighlight;

    private Material foodMat;
    private int framesWithoutPoint = 2;
    private SelectState selectState = SelectState.None;
    private SelectState previousSelectState = SelectState.None;
    //private OVRInput.RawButton ovrClickedButton = OVRInput.RawButton.None;

    private enum SelectState { None, Point, Press, Selected, Deselected};
    public enum FoodMode { Throwable, Edible };

    void Start()
    {
        foodMat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if(framesWithoutPoint <= 1)
        {
            if(selectState == SelectState.None)
            {
                selectState = SelectState.Point;
            }
            else if (selectState == SelectState.Press && GetClickUp())
            {
                selectState = SelectState.Selected;
            }
            else if (selectState == SelectState.Point && GetClick())
            {
                selectState = SelectState.Press;
            }
            else if (selectState == SelectState.Selected && GetClickDown())
            {
                selectState = SelectState.Deselected;
            }
            else if(selectState == SelectState.Deselected && GetClickUp())
            {
                selectState = SelectState.Point;
            }

            framesWithoutPoint++;
        }
        else
        {
            if(selectState != SelectState.Selected)
            {
                selectState = SelectState.None;
            }            
        }
        

        if(previousSelectState != selectState)
        {
            UpdateStateAppearence();
            previousSelectState = selectState;
        }
    }

    private void UpdateStateAppearence()
    {
        if (selectState == SelectState.Selected)
        {
            foodMat.SetColor("_EmissionColor", selectedHighlight);
        }
        else if (selectState == SelectState.Point || selectState == SelectState.Deselected)
        {
            foodMat.SetColor("_EmissionColor", pointHighlight);
        }
        else if (selectState == SelectState.Press)
        {
            foodMat.SetColor("_EmissionColor", pressHighlight);
        }
        else
        {
            foodMat.SetColor("_EmissionColor", Color.black);
        }
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Player"){
            c.gameObject.GetComponent<Player>().ChangeHealth(health);
            Destroy(gameObject);
        }
    }

    public void RegisterPoint()
    {
        framesWithoutPoint = 0;
    }

    public void RegisterClick(OVRInput.RawButton button)
    {
        //ovrClickedButton = button;
    }

    private bool GetClickDown()
    {
        return OVRInput.GetDown(OVRInput.RawButton.A | OVRInput.RawButton.X);
    }
    
    private bool GetClick()
    {
        return OVRInput.Get(OVRInput.RawButton.A | OVRInput.RawButton.X);
    }

    private bool GetClickUp()
    {
        return OVRInput.GetUp(OVRInput.RawButton.A | OVRInput.RawButton.X);
    }

    public void SetMode(FoodMode mode)
    {
        if(mode == FoodMode.Throwable)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().useGravity = true;

        }
        else if(mode == FoodMode.Edible)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
        }
    }
}
