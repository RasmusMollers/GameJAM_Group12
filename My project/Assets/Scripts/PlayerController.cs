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
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    //Progression Parameters
    //private int xp = 0;

   // private float emissionMeter = 100f;



    //Wheels Reference
    List<WheelCollider> Wheels = new List<WheelCollider>();
    List<GameObject> Pickups = new List<GameObject>();

    private CarInputActions carControls; // Reference to the new input system

    void Awake()
    {
        carControls = new CarInputActions(); // Initialize Input Actions
    }
    void OnEnable()
    {
        carControls.Enable();
    }

    void OnDisable()
    {
        carControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Input.enable();
        GameObject car = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        car.transform.position = new Vector3(0, 10, 0);
        Renderer rend = car.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Specular"));
        rb = GetComponent<Rigidbody>();
        foreach (WheelCollider wheel in this.GetComponentsInChildren<WheelCollider>())
        {
            Wheels.Add(wheel);
        }

        // Adjust center of mass to improve stability and prevent rolling
        Vector3 centerOfMass = rb.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rb.centerOfMass = centerOfMass;
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
        
        // Get player input for acceleration and steering
        //float vInput = Input.GetAxisRaw("Vertical"); // Forward/backward input
        //float hInput = Input.GetAxisRaw("Horizontal"); // Steering input
        
        // Get player input for acceleration and steering
        //float vInput = moveAction.action.ReadValue<Vector2>().y; // Forward/backward input
        //float hInput = moveAction.action.ReadValue<Vector2>().x; // Steering input
        //Debug.Log("vInput? " + vInput + "\n" + "hInput " + hInput);


        // Read the Vector2 input from the new Input System
        Vector2 inputVector = carControls.Car.Movement.ReadValue<Vector2>();

        // Get player input for acceleration and steering
        float vInput = inputVector.y; // Forward/backward input
        float hInput = inputVector.x; // Steering input


        // Calculate current speed along the car's forward axis
        float forwardSpeed = Vector3.Dot(transform.forward, rb.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); // Normalized speed factor

        // Reduce motor torque and steering at high speeds for better handling
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor+1);

        // Determine if the player is accelerating or trying to reverse
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);
        //Debug.Log("Accelerating? " + isAccelerating +"\n"+ "Speed " + forwardSpeed);



        foreach (WheelCollider wheel in Wheels)
        {
            if (wheel.CompareTag("Steering"))
            {
                wheel.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                wheel.brakeTorque = 0f;

                // Apply torque to motorized wheels
                if (wheel.CompareTag("Driving"))
                {
                    wheel.motorTorque = vInput * currentMotorTorque;
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

    private void OnTriggerEnter(Collider other)
    {
        GameObject parentOfCollider = other.gameObject;

        if (parentOfCollider.CompareTag("Pickup"))
        {
            Debug.Log("PICKUP");
            Debug.Log("parentOfCollider " + parentOfCollider.name);

            updatePickupList(parentOfCollider);

        }
        if (parentOfCollider.CompareTag("Delivery"))
        {
            var tmpObject = parentOfCollider.GetComponent<Delivery>().GetWantedDelivery();
            Debug.Log("tmpObject " + tmpObject.name);

            updatePickupList(tmpObject);
            if (!Pickups.Contains(tmpObject))
            {

                Destroy(parentOfCollider.GetComponent<Delivery>().GetWantedDelivery());
                Destroy(parentOfCollider);
                Debug.Log("DELIVERY");

            }
            else
            {
                Debug.Log("NO NO DELIVERY");

            }

        }
        updateCenterOfGravityOffset();
    }

    private void updateCenterOfGravityOffset()
    {
        centreOfGravityOffset = -1f+ Pickups.Count;
    }

    private void updatePickupList(GameObject pickup)
    {
        if (Pickups.Contains(pickup))
        {
            Pickups.Remove(pickup);
        }
        else
        {
            Pickups.Add(pickup);
        }
    }
}
