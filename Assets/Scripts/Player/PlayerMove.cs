using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    public GameObject horse;
    public float interactionDistance = 2.0f;

    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        const string shootAnimation = "Shoot";
        
        if (Vector3.Distance(transform.position, horse.transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("R key pressed near horse");
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetTrigger(shootAnimation);
        }
    }
}
