using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : GObject
{
    public int damage = 10;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

    protected override void Die()
    {
        
    }
}
