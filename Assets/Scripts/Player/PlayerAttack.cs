using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator playerAnim;
    public Transform rifle, muzzle;
    public float bulletForce;

    bool set = false; // used for reset angle when not shooting
    static bool shot = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(muzzle.position);
        if (Input.GetKey(KeyCode.Space)&&!shot)
        {
            shot = true;
            playerAnim.SetTrigger("fire");
            // rifle.Rotate(50,0,0);
            set = false;
            Shoot();
        }

        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Fire")==false) // change rotation of rifle
        {
            shot = false;
            if (!set){
                // rifle.Rotate(-50,0,0);
                set = true;
            }
   
        } 
    }


    private void Shoot(){
        Debug.Log(muzzle.position);
        GameObject bullet = ObjectPool.SpawnFromPool("Bullet", muzzle.position);

        // bullet.SetActive(true);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.AddForce(rifle.forward * bulletForce, ForceMode.Impulse);
    }
}
