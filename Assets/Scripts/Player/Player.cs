using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GObject
{
    [HideInInspector] public int money;
    void Start()
    {
        money = 1000;
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


}
