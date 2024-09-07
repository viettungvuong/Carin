using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GObject : MonoBehaviour
{
    [HideInInspector] public int health = 100;



    protected abstract void Die();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;

            // die
            // player
            Die();
        }
    }

}
