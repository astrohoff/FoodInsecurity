using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    //public Transform headOrigin;
    public Transform remainingHealthOrigin;
    public float preferedDistance = 1.5f;
    public float preferedScale = 0.8f;
    public float minDistance = 0.5f;

    /*void Update()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(headOrigin.position, headOrigin.forward, out hitInfo))
        {
            float hitDistance = (hitInfo.point - headOrigin.position).magnitude;
            float interpolationVal = Mathf.Clamp(hitDistance, minDistance, preferedDistance) / preferedDistance;
            transform.position = Vector3.Lerp(headOrigin.position, headOrigin.position + headOrigin.forward * preferedDistance, interpolationVal);
            float interpScale = Mathf.Lerp(0, preferedScale, interpolationVal);
            transform.localScale = new Vector3(interpScale, interpScale, interpScale);

        }
    }*/

    public void SetNormalizedHealth(float health){
        Vector3 newRemainingHealthScale = remainingHealthOrigin.localScale;
        newRemainingHealthScale.x = health;
        remainingHealthOrigin.localScale = newRemainingHealthScale;
    }
}
