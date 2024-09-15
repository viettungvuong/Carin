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

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(vfx, transform.position, Quaternion.identity);

        if (collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<Zombie>().TakeDamage(100);
            PlayerLevel.Instance.AddKill(); 
        }
        Destroy(gameObject);
    }


}
