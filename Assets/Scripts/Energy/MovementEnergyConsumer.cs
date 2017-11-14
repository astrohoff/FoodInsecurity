using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for GameObjects that consume player energy through movement.
public class MovementEnergyConsumer : EnergyConsumer {
    // Reference frame for movement calculation.
    // If this is set to null, global movement is used.
    // Otherwise movement will be relative to the given transform.
    public Transform referenceFrame;
    // Max speed value for energy consumption, above which consumtion will not change.
    public float maxSpeed = 1;
    // Energy consumption at max speed.
    public float maxEnergyConsumption = 1;

    // Position relative to reference frame.
    private Vector3 previousRelativePosition;
    private float currentSpeed;

    void Start()
    {
        // Initialize relative position.
        previousRelativePosition = GetRelativePosition();
    }
	
	void Update () {
        // Calculate speed based on change in position.
        Vector3 newRelativePosition = GetRelativePosition();
        float moveDistance = (newRelativePosition - previousRelativePosition).magnitude;
        currentSpeed = moveDistance / Time.deltaTime;

        // Update energy consumption.
        currentEnergyConsumption = maxEnergyConsumption * (currentSpeed / maxSpeed);
	}

    // Get position relative to reference frame.
    private Vector3 GetRelativePosition()
    {
        if(referenceFrame == null)
        {
            return transform.position;
        }
        return transform.position - referenceFrame.position;
    }
}
