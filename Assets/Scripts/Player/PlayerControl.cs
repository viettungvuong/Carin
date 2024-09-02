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
    public float torqueAmount = 10f; 
    private bool walking = false;
    public Transform playerTrans;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Direction walkDirection = Direction.STILL;
    public float turnMultiplier = 2.0f;
    private Vector3 targetRotation; 
    private bool rotating = false;

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
        if (rotating)
        {
            ApplyRotationTorque();
        }
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

        if (walking)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotating = true;
                targetRotation = playerTrans.eulerAngles + new Vector3(0, -turnSpeed * turnMultiplier, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotating = true;
                targetRotation = playerTrans.eulerAngles + new Vector3(0, turnSpeed * turnMultiplier, 0);
            }
            else
            {
                rotating = false;
                StopRotation();
            }

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

    void ApplyRotationTorque()
    {
        // calculate the desired rotation and apply torque to achieve it
        Quaternion targetRotationQ = Quaternion.Euler(targetRotation);
        Quaternion currentRotation = playerRigid.rotation;
        Quaternion deltaRotation = targetRotationQ * Quaternion.Inverse(currentRotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 0.1f)
        {
            playerRigid.AddTorque(axis * angle * torqueAmount);
        }
    }

    void StopRotation()
    {
        // gradually reduce angular velocity to stop rotation
        playerRigid.angularVelocity = Vector3.Lerp(playerRigid.angularVelocity, Vector3.zero, Time.deltaTime * 5f); 
    }
}
