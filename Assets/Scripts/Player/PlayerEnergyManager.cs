using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyManager : MonoBehaviour {
    // Player energy bar.
    public EnergyBar energyBar;
    // Amount of energy player starts with.
    public float initialEnergy = 7;
    // Maximum amount of engergy the player can have.
    public float maxEnergy = 10;
    // Speed that enegery is lost in energy-units per exertion-unit per second.
    // E.g. if exertion is 1, depletion speed will be 0.5 energy-units per second.
    public float depletionSpeed = 0.5f;
    public float regenSpeed = 0.2f;
    public float maxRegenLevel = 6;
    public List<EnergyConsumer> energyConsumers;

    private float currentEnergy;


    void Start()
    {
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

        energyBar.SetNormalizedHealth(currentEnergy / maxEnergy);

    }

    private float GetTotalExertion()
    {
        if(energyConsumers.Count == 0)
        {
            return 0;
        }
        float totalExertion = 0;
        for(int i = 0; i < energyConsumers.Count; i++)
        {
            totalExertion += energyConsumers[i].currentEnergyConsumption;
        }
        return totalExertion;
    }
}
