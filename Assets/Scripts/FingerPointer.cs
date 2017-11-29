using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerPointer : MonoBehaviour {
    public string indexFingerEndNameId = "index_ignore";
    public GameObject lazerPrefab;
    public bool flipLazerDirection = false;

    private Transform indexFingerPoint;
    private GameObject lazer;
    private bool isLeft;

	void Start () {
        if (name.Contains("left"))
        {
            isLeft = true;
        }
        else if (name.Contains("right"))
        {
            isLeft = false;
        }
        else
        {
            Debug.Log("Neither left nor right found in name " + name);
        }
	}
	
	void Update () {
        // Get index finger transform (it's only created during runtime).
		if(indexFingerPoint == null)
        {
            indexFingerPoint = GetIndexFingerPoint();
            if(indexFingerPoint != null)
            {
                lazer = Instantiate(lazerPrefab, indexFingerPoint);
                if (flipLazerDirection)
                {
                    lazer.transform.forward = -indexFingerPoint.right;
                }
                else
                {
                    lazer.transform.forward = indexFingerPoint.right;
                }
            }
        }
        else
        {
            if (IsPointing())
            {
                lazer.SetActive(true);
                RaycastHit hitInfo;
                if(Physics.Raycast(lazer.transform.position, lazer.transform.forward, out hitInfo))
                {
                    float hitDistance = (indexFingerPoint.position - hitInfo.point).magnitude;
                    lazer.transform.localScale = new Vector3(1, 1, hitDistance);

                    Food hitFood = hitInfo.collider.GetComponent<Food>();
                    if(hitFood != null)
                    {
                        hitFood.RegisterPoint();
                    }
                }
                else
                {
                    lazer.transform.localScale = new Vector3(1, 1, 10);
                }
            }
            else
            {
                lazer.SetActive(false);
            }
        }
	}

    private bool IsPointing()
    {
        if (isLeft)
        {
            return OVRInput.Get(OVRInput.RawButton.LHandTrigger) && !OVRInput.Get(OVRInput.RawTouch.LIndexTrigger);
        }
        else
        {
            return OVRInput.Get(OVRInput.RawButton.RHandTrigger) && !OVRInput.Get(OVRInput.RawTouch.RIndexTrigger);
        }
    }

    private Transform GetIndexFingerPoint(Transform searchObj = null)
    {
        if(searchObj == null)
        {
            return GetIndexFingerPoint(transform);
        }

        if (searchObj.name.Contains(indexFingerEndNameId))
        {
            return searchObj;
        }

        Transform indexPoint;
        for(int i = 0; i < searchObj.childCount; i++)
        {
            indexPoint = GetIndexFingerPoint(searchObj.GetChild(i));
            if(indexPoint != null)
            {
                return indexPoint;
            }
        }
        return null;
    }
}
