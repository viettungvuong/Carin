using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    public float interactionDistance = 2.0f;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

//     void Update()
//     {
//         const string shootAnimation = "fire";
        

//         if (Input.GetKey(KeyCode.Q))
//         {
//             animator.SetTrigger(shootAnimation);
//         }
//     }
}
