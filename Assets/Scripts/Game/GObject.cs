using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GObject : MonoBehaviour
{
    protected int health;
    protected abstract void Die();

    protected void Start() {
        health = 100;
    }

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

    public bool isDied(){
        return health <= 0;
    }

}
