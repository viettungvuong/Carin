using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator playerAnim;
    public Transform rifle, muzzle;
    public float bulletForce;

    static bool shot = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !shot)
        {
            shot = true;
            playerAnim.SetTrigger("fire");
            StartCoroutine(DelayedShoot());
        }

        if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            shot = false;
        } 
    }

    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPool.SpawnFromPool("Bullet", muzzle.position);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(rifle.forward * bulletForce, ForceMode.Impulse);
    }
}