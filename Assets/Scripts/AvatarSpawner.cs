using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AvatarSpawner : MonoBehaviour {
    public GameObject avatarPrefab;

	void Start () {
        if(XRDevice.isPresent){
            Instantiate(avatarPrefab, transform);
        }
	}
}
