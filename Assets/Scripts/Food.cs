using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public float health = 2;
    public Color pointHighlight, pressHighlight, selectedHighlight;

    private Material foodMat;
    private int framesWithoutPoint = 0;
    private SelectState selectState = SelectState.None;
    private SelectState previousSelectState = SelectState.None;
    //private OVRInput.RawButton ovrClickedButton = OVRInput.RawButton.None;

    private enum SelectState { None, Point, Press, Selected};

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
        }
        else
        {
            selectState = SelectState.None;
        }
        

        if(previousSelectState != selectState)
        {
            UpdateStateAppearence();
            previousSelectState = selectState;
        }
        framesWithoutPoint++;
    }

    private void UpdateStateAppearence()
    {
        if (selectState == SelectState.Selected)
        {
            foodMat.SetColor("_EmissionColor", selectedHighlight);
        }
        else if (selectState == SelectState.Point)
        {
            foodMat.SetColor("_EmissionColor", pointHighlight);
        }
        else if (selectState == SelectState.Press)
        {
            foodMat.SetColor("_EmissionColor", pressHighlight);
        }
        else
        {
            if(selectState != SelectState.Selected)
            {
                selectState = SelectState.None;
            }
        }
    }

    void OnTriggerEnter(Collider c){
        Debug.Log("Triggered");
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

}
