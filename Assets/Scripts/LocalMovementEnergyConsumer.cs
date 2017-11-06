using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMovementEnergyConsumer : MonoBehaviour, IEnergyConsumer {
    public float maxSpeed = 1.5f;
    public float weight = 1;

    private Vector3 previousPosition;
    private float currentExertion = 0;

    void Start()
    {
        previousPosition = transform.localPosition;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyManager>().RegisterEnergyConsumer(this);
    }

    void Update()
    {
        Vector3 movement = transform.localPosition - previousPosition;
        float speed = movement.magnitude / Time.deltaTime;
        currentExertion = Mathf.Clamp01(speed / maxSpeed);
    }

    public float GetCurrentExertion()
    {
        return currentExertion;
    }

    public float GetWeight()
    {
        return weight;
    }
}
