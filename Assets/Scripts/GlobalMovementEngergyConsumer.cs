using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMovementEngergyConsumer : MonoBehaviour, IEnergyConsumer {
    public float maxSpeed = 1.5f;
    public float weight = 1;

    private Vector3 previousPosition;
    private float currentExertion = 0;

    void Start()
    {
        previousPosition = transform.position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyManager>().RegisterEnergyConsumer(this);
    }

    void Update()
    {
        Vector3 movement = transform.position - previousPosition;
        float speed = movement.magnitude / Time.deltaTime;
        currentExertion = Mathf.Clamp01(speed / maxSpeed);
        previousPosition = transform.position;
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
