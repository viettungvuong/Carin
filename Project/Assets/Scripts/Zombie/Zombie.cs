using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : GObject
{
    public int damage = 10;
    public float maxDistance = 20f; 
    private Transform player; 
    Animator animator;
    private bool canDealDamage = true;

    private new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    private void Update()
    {
        if (!isDied())
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > maxDistance)
            {
                health = 0;
                Die();
            }

        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!isDied() && other.gameObject.CompareTag("Player") && canDealDamage)
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            StartCoroutine(DamageCooldown(2f));
        }
    }

    private IEnumerator DamageCooldown(float cooldown)
    {
        canDealDamage = false;
        yield return new WaitForSeconds(cooldown);
        canDealDamage = true;
    }

    public override void Die()
    {
        animator.SetTrigger("fall");
        audioSource.clip = dieClip;
        audioSource.Play();
        StartCoroutine(DeactivateAfterDelay(3f));
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
