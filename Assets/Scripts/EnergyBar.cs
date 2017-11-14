using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour {
    public Transform remainingHealthOrigin;
    public void SetNormalizedHealth(float health){
        Vector3 newRemainingHealthScale = remainingHealthOrigin.localScale;
        newRemainingHealthScale.x = health;
        remainingHealthOrigin.localScale = newRemainingHealthScale;
    }	
}
