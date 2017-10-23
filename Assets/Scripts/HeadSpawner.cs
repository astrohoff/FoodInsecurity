using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class HeadSpawner : MonoBehaviour {
    public GameObject nonVrHeadPrefab, vrHeadPrefab;
    public Transform vrSpawnPosition, nonVrSpawnPosition;

	// Use this for initialization
	void Awake () {
        GameObject head;
        if (UnityEngine.XR.XRDevice.isPresent)
        {
            head = Instantiate(vrHeadPrefab, transform);
            head.transform.position = vrSpawnPosition.position;
        }
        else
        {
            head = Instantiate(nonVrHeadPrefab, transform);
            head.transform.position = nonVrSpawnPosition.position;
        }
        GetComponent<Player>().playerHead = head.transform;
	}
}
