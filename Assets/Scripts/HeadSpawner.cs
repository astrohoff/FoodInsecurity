using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class HeadSpawner : MonoBehaviour {
    public GameObject nonVRHeadPrefab, VRHeadPrefab;
    public Transform headPosition;

	// Use this for initialization
	void Start () {
        if (UnityEngine.XR.XRDevice.isPresent)
        {
            GameObject head = Instantiate(VRHeadPrefab, transform);
            head.transform.position = headPosition.position;
        }
        else
        {
            GameObject head = Instantiate(nonVRHeadPrefab, transform);
            head.transform.position = headPosition.position;
        }
	}
}
