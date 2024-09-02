using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public int damage=10;
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
        GObject gObject = other.gameObject.GetComponent<GObject>();
        if (gObject!=null){
            gObject.TakeDamage(damage);
        }
    }
}
