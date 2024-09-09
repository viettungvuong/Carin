using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : GObject
{
    public int damage = 10;
    Animator animator;

    private new void Start() {
        base.Start();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision other) {
        if (!isDied()&&other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }

    }

    protected override void Die()
    {
        animator.SetTrigger("fall");
        // transform.position = new Vector3(transform.position.x, 4.2f);
    }
}
