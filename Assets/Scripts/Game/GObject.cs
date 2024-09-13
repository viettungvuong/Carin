using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GObject : MonoBehaviour
{
    protected int health;

    public ParticleSystem bloodVfx;

    public AudioClip dieClip;
    protected AudioSource audioSource;

    protected void Start() {
        health = 100;
        audioSource = GetComponent<AudioSource>();
    }

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

        ParticleSystem blood = Instantiate(bloodVfx, transform.position + transform.forward * 1.0f + new Vector3(0, 1, 0), Quaternion.identity);
        blood.transform.SetParent(transform);
    
    }

    public bool isDied(){
        return health <= 0;
    }

}
