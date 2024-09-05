using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator playerAnim;
    public Transform rifle, muzzle;
    public float bulletForce;
    public ParticleSystem muzzleFlash;

    private int bullets = 10;
    private int bulletsInClip = 3;
    public TextMeshProUGUI bulletText;

    static bool shot = false;

    void Update()
    {
        bulletText.text = bullets.ToString() + "/" + bulletsInClip.ToString();
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
        bulletRb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        muzzleFlash.Play();
    }

    public void RefillBullets(int amount){
        bullets += amount;
    }
}