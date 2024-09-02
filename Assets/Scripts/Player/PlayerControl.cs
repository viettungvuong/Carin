using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum Direction{
    FORWARD,
    BACKWARD,
    STILL
}
public class PlayerControl : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    private bool walking = false;
    public Transform playerTrans;
    public float turnSmoothTime = 0.1f; // reduced for sharper turning
    private float turnSmoothVelocity;
    private Direction walkDirection = Direction.STILL;
    public float turnMultiplier = 2.0f; // increase for sharper GTA-like turns

    private void Move(){
        Vector3 movement = Vector3.zero;
        Debug.Log(walkDirection);
        if (walkDirection == Direction.FORWARD)
        {
            movement += playerTrans.forward * w_speed * Time.deltaTime;
        }
        else if (walkDirection == Direction.BACKWARD)
        {
            movement -= playerTrans.forward * wb_speed * Time.deltaTime;
        }

        playerRigid.MovePosition(playerRigid.position + movement);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
            walkDirection = Direction.FORWARD;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
            walkDirection = Direction.STILL;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walkback");
            playerAnim.ResetTrigger("idle");
            walking = true;
            walkDirection = Direction.BACKWARD;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walkback");
            playerAnim.SetTrigger("idle");
            walking = false;
            walkDirection = Direction.STILL;
        }

        if (walking && Input.GetKey(KeyCode.A))
        {
            HandleRotation(-ro_speed); 
        }
        if (walking && Input.GetKey(KeyCode.D))
        {
            HandleRotation(ro_speed);   
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

    void HandleRotation(float rotationAmount)
    {
        Move();
        float targetRotation = playerTrans.eulerAngles.y + (rotationAmount * turnMultiplier);
        float smoothRotation = Mathf.SmoothDampAngle(playerTrans.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        playerTrans.rotation = Quaternion.Euler(0, smoothRotation, 0);

    }
}