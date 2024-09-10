using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : GObject
{
    public int damage = 10;
    Animator animator;
    public ParticleSystem vfxHit;

    private new void Start() {
        base.Start();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision other) {
        if (!isDied()&&other.gameObject.CompareTag("Player")){
            Instantiate(vfxHit, other.gameObject.transform.position, Quaternion.identity);
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }

    }

    protected override void Die()
    {
        animator.SetTrigger("fall");
        StartCoroutine(DeactivateAfterDelay(2f));
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
