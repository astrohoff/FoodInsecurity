using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineInteractionSpawner : MonoBehaviour {
    public GameObject buttonPresserPrefab;
    public float interactionStartDistance = 3f;
    public float interactionEndDistance = 4f;

    private float previousPlayerDistance = float.MaxValue;
    private GameObject playerObj;
    private List<GameObject> instantiatedInteractionObjects;

	void Start()
    {
        instantiatedInteractionObjects = new List<GameObject>();
        playerObj = GameObject.FindWithTag("Player");
        if(playerObj == null)
        {
            throw new System.Exception("Couldn't find a GameObject tagged \"Player\"");
        }
        
    }

    void Update()
    {
        float currentPlayerDistance = (Camera.main.transform.position - transform.position).magnitude;
        if(currentPlayerDistance < interactionStartDistance)
        {
            if(previousPlayerDistance > interactionStartDistance)
            {
                OnPlayerApproach();
            }
        }
        else if(currentPlayerDistance > interactionEndDistance)
        {
            if(previousPlayerDistance < interactionEndDistance)
            {
                OnPlayerLeave();
            }
        }

        previousPlayerDistance = currentPlayerDistance;
    }

    private void OnPlayerApproach()
    {
        Debug.Log("Player approaching");
        GameObject leftHand = GameObject.Find("hands:b_l_index_ignore");
        GameObject rightHand = GameObject.Find("hands:b_r_index_ignore");
        if (leftHand != null && rightHand != null)
        {
            instantiatedInteractionObjects.Add(Instantiate(buttonPresserPrefab, leftHand.transform));
            instantiatedInteractionObjects.Add(Instantiate(buttonPresserPrefab, rightHand.transform));
        }
        else
        {
            Debug.Log("No VR hands found");
        }
    }

    private void OnPlayerLeave()
    {
        Debug.Log("Player leaving");
        for(int i = 0; i < instantiatedInteractionObjects.Count; i++)
        {
            Destroy(instantiatedInteractionObjects[i]);
        }
        instantiatedInteractionObjects.Clear();
    }
}
