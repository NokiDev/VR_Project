﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsData : MonoBehaviour {

    public float maxEnergy = 100;

    public delegate void FloatEmitter(float value);
    public delegate void BoolEmitter(bool value);
    public delegate void IntegerEmitter(int value);
    public delegate void UIntegerEmitter(uint value);
    public delegate void EmptyEmitter();



    public event FloatEmitter energyChanged;
    public event EmptyEmitter outOfEnergy;

    public float CurrentEnergy
    {
        get
        {
            return currentEnergy;
        }

        set
        { 
            currentEnergy = value;
            if(currentEnergy < 0) 
            {
                currentEnergy = 0;
                if(outOfEnergy!=null) outOfEnergy();
            } 
            if(currentEnergy > maxEnergy) currentEnergy = maxEnergy;

            if(energyChanged != null) energyChanged(currentEnergy);
        }
    }
    private float currentEnergy; // Energy percent; 

    // Use this for initialization
    void Start () {
        CurrentEnergy = maxEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseEnergy(float energyUsed)
    {
        CurrentEnergy -= energyUsed;

    }

    public void ReloadEnergy(float energyReloaded)
    {
        CurrentEnergy += energyReloaded;
    }

}
