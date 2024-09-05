using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : GObject
{
    [HideInInspector] public int money, energy;
    public TextMeshProUGUI healthText;

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
        health += amount;
    }

    public void RefillEnergy(int amount){
        energy += amount;
    }

}
