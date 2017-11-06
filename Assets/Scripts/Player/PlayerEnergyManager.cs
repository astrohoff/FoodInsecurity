using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyManager : MonoBehaviour {
    // Healthbar script attached to the player.
    public HealthBar healthBar;

    public float initialEnergy = 7;
    public float maxEnergy = 10;
    public float depletionSpeed = 0.5f;
    public float regenSpeed = 0.2f;
    public float maxRegenLevel = 6;

    private Vector3 previousPosition;
    private List<IEnergyConsumer> energyConsumers;
    private float currentEnergy;

    void Awake()
    {
        energyConsumers = new List<IEnergyConsumer>();
    }

    void Start()
    {
        previousPosition = Camera.main.transform.position;
        currentEnergy = initialEnergy;
    }

    void Update()
    {
        float exertion = GetTotalExertion();
        currentEnergy -=  exertion * depletionSpeed * Time.deltaTime;
        if(currentEnergy < maxRegenLevel)
        {
            currentEnergy += (1 - exertion) * regenSpeed * Time.deltaTime;
        }

        healthBar.SetNormalizedHealth(currentEnergy / maxEnergy);

    }

    public void RegisterEnergyConsumer(IEnergyConsumer energyConsumer)
    {
        energyConsumers.Add(energyConsumer);
    }

    private float GetTotalExertion()
    {
        if(energyConsumers.Count == 0)
        {
            return 0;
        }
        float totalExertion = 0;
        float totalWeight = 0;
        for(int i = 0; i < energyConsumers.Count; i++)
        {
            totalExertion += energyConsumers[i].GetCurrentExertion() * energyConsumers[i].GetWeight();
            totalWeight += energyConsumers[i].GetWeight();
        }
        return totalExertion / totalWeight;
    }
}
