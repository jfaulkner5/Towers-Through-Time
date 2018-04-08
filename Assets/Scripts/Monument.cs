using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour {

    public int monumentHealth;
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

    public void TakeDamage(int damage)
    {
        monumentHealth -= damage;
        if(monumentHealth <= 0)
        {
            EventCore.Instance.levelLost.Invoke();
        }
    }



}
