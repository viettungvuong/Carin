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
    public float walkingSpeed = 4f;   
    public float runningSpeed = 7f;   
    public float backwardSpeed = 3f;
    public float idleSpeed = 2f;
    public float turnSpeed = 1f;

    private bool walking = false;
    private bool running = false;

    public Transform playerTrans;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Direction walkDirection = Direction.STILL;
    public float turnMultiplier = 2.0f;
    private Player player;

    public float energyRefillAmount = 5f; // Amount of energy added each refill
    public float maxEnergy = 100f;
    public float energyDrainRate = 0.01f;

    private float refillInterval = 2f; // Time between refills
    private float refillTimer = 0f;    // Timer to track time for refilling

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;
        float currentSpeed = running ? runningSpeed : walkingSpeed;

        if (walkDirection == Direction.FORWARD)
        {
            movement += playerTrans.forward * currentSpeed * Time.deltaTime;
        }
        else if (walkDirection == Direction.BACKWARD)
        {
            movement -= playerTrans.forward * backwardSpeed * Time.deltaTime;
        }

        playerTrans.position += movement;
    }

    void FixedUpdate()
    {
        if (Mode.mode != TypeMode.WALKING)
        {
            return;
        }
        Move();
    }

    void Update()
    {
        if (Mode.mode != TypeMode.WALKING)
        {
            return;
        }

        HandleMovementInput();

        // Handle energy refilling and draining
        if (running)
        {
            if (player.energy > 0)
            {
                DrainEnergy();
            }
            else
            {
                running = false;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
        else if (walking)
        {
            refillTimer += Time.deltaTime;
            if (refillTimer >= refillInterval)
            {
                RefillEnergy();
                refillTimer = 0f; // Reset timer
            }
        }

        // Round energy to 0 decimal points
        player.energy = Mathf.Floor(player.energy);
    }

    void HandleMovementInput()
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

        // Rotation inputs
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-turnSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(turnSpeed);
        }

        // Run input
        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (player.energy > 0)
                {
                    running = true;
                    playerAnim.SetTrigger("run");
                    playerAnim.ResetTrigger("walk");
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                running = false;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
    }

    void RefillEnergy()
    {
        player.energy = Mathf.Min(player.energy + energyRefillAmount, maxEnergy);
    }

    void DrainEnergy()
    {
        player.energy = Mathf.Max(player.energy - energyDrainRate * Time.deltaTime, 0);
    }

    void ApplyRotation(float rotationAmount)
    {
        float targetRotation = playerTrans.eulerAngles.y + (rotationAmount * turnMultiplier);
        float smoothRotation = Mathf.SmoothDampAngle(playerTrans.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        playerTrans.rotation = Quaternion.Euler(0, smoothRotation, 0);
    }
}
