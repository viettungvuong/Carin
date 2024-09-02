using UnityEngine;

enum Direction
{
    FORWARD,
    BACKWARD,
    STILL
}

public class PlayerControl : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float walkSpeed = 5f;
    public float backwardSpeed = 3f;
    public float idleSpeed = 2f;
    public float runSpeedIncrease = 2f;
    public float turnSpeed = 1f;
    private bool walking = false;
    public Transform playerTrans;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Direction walkDirection = Direction.STILL;
    public float turnMultiplier = 2.0f;

    private void Move()
    {
        Vector3 movement = Vector3.zero;
        
        if (walkDirection == Direction.FORWARD)
        {
            movement += playerTrans.forward * walkSpeed * Time.deltaTime;
        }
        else if (walkDirection == Direction.BACKWARD)
        {
            movement -= playerTrans.forward * backwardSpeed * Time.deltaTime;
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
            HandleRotation(-turnSpeed); 
        }
        if (walking && Input.GetKey(KeyCode.D))
        {
            HandleRotation(turnSpeed);   
        }

        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed += runSpeedIncrease;
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walkSpeed -= runSpeedIncrease;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
    }

    void HandleRotation(float rotationAmount)
    {
        float targetRotation = playerTrans.eulerAngles.y + (rotationAmount * turnMultiplier);
        float smoothRotation = Mathf.SmoothDampAngle(playerTrans.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        playerTrans.rotation = Quaternion.Euler(0, smoothRotation, 0);
    }
}
