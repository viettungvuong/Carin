using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    private bool walking = false;
    public Transform playerTrans;
    public float turnSmoothTime = 0.3f; 
    private float turnSmoothVelocity;  

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += playerTrans.forward * w_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement -= playerTrans.forward * wb_speed * Time.deltaTime;
        }

        playerRigid.MovePosition(playerRigid.position + movement);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walkback");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walkback");
            playerAnim.SetTrigger("idle");
        }

        if (Input.GetKey(KeyCode.A))
        {
            float targetRotation = playerTrans.eulerAngles.y - ro_speed;
            float smoothRotation = Mathf.SmoothDampAngle(playerTrans.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            playerTrans.rotation = Quaternion.Euler(0, smoothRotation, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            float targetRotation = playerTrans.eulerAngles.y + ro_speed;
            float smoothRotation = Mathf.SmoothDampAngle(playerTrans.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            playerTrans.rotation = Quaternion.Euler(0, smoothRotation, 0);
        }

        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                w_speed = w_speed + rn_speed;
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
    }
}