using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    public float interactionDistance = 2.0f;

    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        const string shootAnimation = "Shoot";
        

        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetTrigger(shootAnimation);
        }
    }
}
