using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem vfx;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float speed = 10f;
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        Instantiate(vfx, transform.position, Quaternion.identity);

        if (other.gameObject.CompareTag("Zombie")){
            other.gameObject.GetComponent<Zombie>().TakeDamage(100);
        }
        Destroy(gameObject);
    }
}
