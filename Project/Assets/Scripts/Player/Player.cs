using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : GObject
{
    [HideInInspector] public int money;
    [HideInInspector] public float energy;
    public TextMeshProUGUI healthText, energyText, moneyText;
    Animator animator;

    [SerializeField] private GameObject diePanel;
    [SerializeField] private TextMeshProUGUI survivedText;

    private new void Start() {
        base.Start();
        energy = 100;
        money = 100;

        animator = GetComponent<Animator>();
    }
    private void Update() {
        healthText.text = health.ToString();
        energyText.text = energy.ToString();
        moneyText.text = money.ToString();
    }

    public bool EnoughMoney(int amount)
    {
        return amount >= money;
    }

    public bool UseMoney(int amount){
        if (EnoughMoney(amount))
        {
            money -= amount;
            return true;
        }
        else
        {
            return false;
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

    public override void Die()
    {
        healthText.text = health.ToString();
        animator.SetTrigger("fall");
        audioSource.clip = dieClip;
        audioSource.Play();
        // transform.position = new Vector3(transform.position.x, 4f);
        // die

        diePanel.SetActive(true);
        string time = TimeManager.Instance.SurviveTime();
        survivedText.text = "YOU HAVE SURVIVED FOR " + time;
    }

    // player attack using gun only
    // private void OnCollisionEnter(Collision other) {

    // }

}
