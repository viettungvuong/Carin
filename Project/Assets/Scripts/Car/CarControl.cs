using UnityEngine;

public class CarControl : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    public float acceleration = 500f;
    public float brakingForce = 300f;
    public float maxTurnAngle = 15f;
    public float maxSpeed = 50f;  // Maximum speed in meters per second

    private float currentAcceleration = 0f;
    private float currentBrakingForce = 0f;
    private float currentTurnAngle = 0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Mode.mode == TypeMode.WALKING)
        {
            currentAcceleration = 0;
            return;
        }

        float speed = rb.velocity.magnitude;  // current speed

        // If speed is below maxSpeed, apply acceleration; otherwise, stop accelerating
        if (speed < maxSpeed)
        {
            currentAcceleration = acceleration * Input.GetAxis("Vertical");
        }
        else
        {
            currentAcceleration = 0f;
        }

        if (Input.GetKey(KeyCode.Space))
            currentBrakingForce = brakingForce;
        else
            currentBrakingForce = 0f;

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBrakingForce;
        frontLeft.brakeTorque = currentBrakingForce;
        backLeft.brakeTorque = currentBrakingForce;
        backRight.brakeTorque = currentBrakingForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }

}
