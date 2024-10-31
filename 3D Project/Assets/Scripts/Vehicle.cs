using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    // Although vehicle will move with us "taking over", we'll still be 
    //   manipulating the Rigidbody of the vehicle while it's kinematic.
    public Rigidbody rBody;

    // Fields for Speed (per second)
    public float maxSpeed;
    public float minSpeed;

    // Fields for Acceleration & Deceleration
    public float accelerationRate;
    public float decelerationRate;

    // Fields for Turning
    public float turnSpeed;

    // ***
    // All remaining fields are private!
    // No other classes should have access to this data about the vehicle.
    // They could potentially break the logic of movement in this script.
    // ***

    // Fields for Input
    public Vector3 movementDirection;

    // Fields for Movement Vectors
    [SerializeField]
    private Vector3 velocity;
    private Vector3 acceleration;

    // Fields for Quaternions
    private Quaternion turning;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // IF the gas is on
        if (movementDirection.z != 0f)
        {
            // Movement "formula":
            //transform.forward = movementDirection;

            // accel 
            acceleration = accelerationRate * movementDirection.z * transform.forward;

            // Velocity is speed * direction - not scaled to a per-frame basis.
            // It's on a per-second basis.
            // instant GO
            // velocity = maxSpeed * movementDirection.normalized;
            velocity += acceleration * Time.fixedDeltaTime;

            // limit velocity based on maxSpeed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else // DECEL
        {
            velocity *= 1f - (decelerationRate * Time.fixedDeltaTime);

            if(velocity.sqrMagnitude < minSpeed * minSpeed)
            {
                velocity = Vector3.zero;
            }
        }

        // Move the vehicle!
        rBody.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        Vector2 inputDir = callbackContext.ReadValue<Vector2>();
        movementDirection.z = inputDir.y;
        movementDirection.x = inputDir.x;
    }
}
