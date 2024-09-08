using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform rifle, muzzle;
    public Transform cameraTransform; // Reference to the Camera's transform
    public float bulletForce;
    public float range = 100f;  // Range of the gun
    public ParticleSystem muzzleFlash;
    public LayerMask targetLayer;
    
    public TextMeshProUGUI bulletText;

    public float mouseSensitivity = 100f; 
    private float xRotation = 0f;

    private int bullets = 10;
    private int bulletsInClip = 3;
    static bool shot = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // handle mouse input for camera aiming
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        rifle.Rotate(Vector3.up * mouseX);

        bulletText.text = bullets.ToString() + "/" + bulletsInClip.ToString();

        if (bullets > 0 && Input.GetKeyDown(KeyCode.Space) && !shot)
        {
            shot = true;
            StartCoroutine(DelayedShoot());
            bullets--;

            bulletText.text = bullets.ToString() + "/" + bulletsInClip.ToString();
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
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, range, targetLayer))
        {
            if (hit.collider.gameObject.CompareTag("Zombie"))
            {
                hit.collider.gameObject.GetComponent<Zombie>().TakeDamage(100);
            }

            Rigidbody hitRb = hit.collider.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                hitRb.AddForce(-hit.normal * bulletForce);
            }
        }

        // Bullet visual
        GameObject bullet = ObjectPool.SpawnFromPool("Bullet", muzzle.position);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(muzzle.forward * bulletForce, ForceMode.Impulse);
    }

    public void RefillBullets(int amount)
    {
        bullets += amount;
    }
}
