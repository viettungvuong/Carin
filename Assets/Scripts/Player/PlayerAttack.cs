using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator playerAnim;
    public Transform rifle, muzzle;
    public float bulletForce;
    public float range = 100f;  // Range of the gun
    public ParticleSystem muzzleFlash;
    public LayerMask targetLayer;
    
    private int bullets = 10;
    private int bulletsInClip = 3;
    public TextMeshProUGUI bulletText;
    
    static bool shot = false;


    void Update()
    {

        bulletText.text = bullets.ToString() + "/" + bulletsInClip.ToString();
        if (bullets > 0 && Input.GetKeyDown(KeyCode.Space) && !shot)
        {


            shot = true;
            playerAnim.SetTrigger("fire");
            StartCoroutine(DelayedShoot());
            bullets--;

            bulletText.text = bullets.ToString() + "/" + bulletsInClip.ToString();


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
        muzzleFlash.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, transform.forward, out hit, range, targetLayer))
        {
            if (hit.collider.gameObject.CompareTag("Zombie")){
                hit.collider.gameObject.GetComponent<Zombie>().TakeDamage(100);
            }

            Rigidbody hitRb = hit.collider.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                hitRb.AddForce(-hit.normal * bulletForce);
            }
        }

        // bullet visual
        GameObject bullet = ObjectPool.SpawnFromPool("Bullet", muzzle.position);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(muzzle.forward * bulletForce, ForceMode.Impulse);
    }

    public void RefillBullets(int amount)
    {
        bullets += amount;
    }
}
