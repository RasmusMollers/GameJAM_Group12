using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    float accForce = 1000f;
    float steeringAngle = 0f;
    float steeringRate = 15f;

    [Header("Car Properties")]
    public float motorTorque = 20000000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;


    //Wheels Reference
    List<WheelCollider> Wheels = new List<WheelCollider>();

    [Header("Input Actions")]
    public InputActionReference moveAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject car = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        car.transform.position = new Vector3(0, 10, 0);
        Renderer rend = car.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Specular"));
        rb = GetComponent<Rigidbody>();
        foreach (WheelCollider wheel in this.GetComponentsInChildren<WheelCollider>())
        {
            Wheels.Add(wheel);
        }
        foreach (WheelCollider wheel in Wheels)
        {
            Debug.Log("Wheel : " + wheel);

        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // FixedUpdate is called at a fixed time interval 
    void FixedUpdate()
    {
        updateSpeedAndSteering();
    }

    void updateSpeedAndSteering()
    {
        rb.AddForce(transform.forward * motorTorque);
        /*
        // Get player input for acceleration and steering
        float vInput = Input.GetAxisRaw("Vertical"); // Forward/backward input
        float hInput = Input.GetAxisRaw("Horizontal"); // Steering input
        */
        // Get player input for acceleration and steering
        float vInput = moveAction.action.ReadValue<Vector2>().y; // Forward/backward input
        float hInput = moveAction.action.ReadValue<Vector2>().x; // Steering input
        Debug.Log("vInput? " + vInput + "\n" + "hInput " + hInput);


        // Calculate current speed along the car's forward axis
        float forwardSpeed = Vector3.Dot(transform.forward, rb.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); // Normalized speed factor

        // Reduce motor torque and steering at high speeds for better handling
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor+1);

        // Determine if the player is accelerating or trying to reverse
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);
        Debug.Log("Accelerating? " + isAccelerating +"\n"+ "Speed " + forwardSpeed);



        foreach (WheelCollider wheel in Wheels)
        {
            if (wheel.CompareTag("Steering"))
            {
                wheel.steerAngle = hInput * steeringRange;
            }

            if (isAccelerating)
            {
                wheel.brakeTorque = 0f;

                // Apply torque to motorized wheels
                if (wheel.CompareTag("Driving"))
                {
                    wheel.motorTorque = vInput * motorTorque;
                }
                // Release brakes when accelerating
            }
            else
            {
                // Apply brakes when reversing direction
                wheel.motorTorque = 0f;
                wheel.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
        }

    }
}
