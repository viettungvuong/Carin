using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : GObject
{
    [HideInInspector] public int money, energy;
    public TextMeshProUGUI healthText, energyText, moneyText;


    private void Start() {
        energy = 100;
        health = 100;
        money = 100;
    }
    private void Update() {
        healthText.text = health.ToString();
        energyText.text = energy.ToString();
        moneyText.text = money.ToString();
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

    protected override void Die()
    {
        
    }

    // player attack using gun only
    // private void OnCollisionEnter(Collision other) {

    // }

}
