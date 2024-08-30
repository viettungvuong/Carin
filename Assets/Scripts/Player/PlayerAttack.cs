using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator playerAnim;
    public Transform rifle;
    private Vector3 originalRifleAngle;
    bool set = false;
    void Start()
    {
        originalRifleAngle = rifle.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerAnim.SetTrigger("fire");
            rifle.Rotate(50,0,0);
            set = false;
        }

        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Fire")==false) // change rotation of rifle
        {
            if (!set){
                rifle.Rotate(-50,0,0);
                set = true;  
            }
   
        }
    }
}
