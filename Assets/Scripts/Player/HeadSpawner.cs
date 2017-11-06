using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityStandardAssets.ImageEffects;

public class HeadSpawner : MonoBehaviour {
    public GameObject nonVrHeadPrefab, vrHeadPrefab;
    public Transform vrSpawnPosition, nonVrSpawnPosition;
    public Transform healthBar;

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
        //GetComponent<Player>().playerHead = Camera.main.transform;
        healthBar.parent = Camera.main.transform;
        Vector3 adjustedPos = healthBar.localPosition;
        adjustedPos.y = 0.5f;
        healthBar.localPosition = adjustedPos;
        //GetComponent<Player>().blur = Camera.main.GetComponent<BlurOptimized>();
	}
}
