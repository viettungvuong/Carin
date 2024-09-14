using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public int damage=100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name=="Terrain"){
            gameObject.SetActive(false);
            return;
        }
        else if (other.gameObject.CompareTag("Zombie")){
            other.gameObject.GetComponent<Zombie>().TakeDamage(damage);
        }
    }
}
