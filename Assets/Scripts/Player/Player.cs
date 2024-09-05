using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : GObject
{
    [HideInInspector] public int money, energy;
    public TextMeshProUGUI healthText, bulletText, energyText;

    void Start()
    {
        money = 1000;
        energy = 100;
    }

    

    public bool UseMoney(int amount){
        if (amount>money){
            return false;
        }
        else{
            money -= amount;
            return true;
        }
    }

    public void AddMoney(int amount){
        money += amount;
    }

    public void RefillHealth(int amount){
        health = Math.Min(100, health + amount);
    }

    public void RefillEnergy(int amount){
        energy = Math.Min(100, energy + amount);
    }

}
