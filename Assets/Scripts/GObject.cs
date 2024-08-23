using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GObject : MonoBehaviour
{
    public int health;

    void Start()
    {
        health = 100;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Player is dead.");
        }
    }

}
