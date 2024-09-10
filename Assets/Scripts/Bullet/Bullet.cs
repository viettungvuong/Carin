using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float speed = 10f;
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
