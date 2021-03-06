﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour {
    public ParticleSystem monumentHealthPickup_PS;
    public ParticleSystem monumentDamage_PS;
    public int maxMonumentHealth;
    int currentMonumentHealth;
    public static Monument instance;
    

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
            Debug.LogError("Mutliple Monuments in scene.");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        EventCore.Instance.healthPickup.AddListener(ReturnHealth);
        currentMonumentHealth = maxMonumentHealth;
    }

    public void TakeDamage(int damage)
    {
        monumentDamage_PS.Play();
        currentMonumentHealth -= damage;
        if(currentMonumentHealth <= 0)
        {
            print("YOU LOSE");
            EventCore.Instance.levelLost.Invoke();
        }
    }

   public void ReturnHealth()
    {

        
            monumentHealthPickup_PS.Play();    
            currentMonumentHealth++;
        //play particle effect or do visual gain for monument health
        Debug.Log("health has been picked up. new health is: " + currentMonumentHealth);

    }

}
