using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// https://docs.google.com/document/d/1Igukg_XBW6FtwrM2sL0n3BkryQwu6pYfwAX64tpI40M/edit?tab=t.0#heading=h.w14grdblwqpa
// https://docs.google.com/document/d/1n0c7FayT74mB6qEMTrCZQlP-Z4Nez4H2jtH6zxWCZ8U/edit?tab=t.0#heading=h.w14grdblwqpa
public class Vehicle : MonoBehaviour
{
    // Although vehicle will move with us "taking over", we'll still be 
    //   manipulating the Rigidbody of the vehicle while it's kinematic.
    [SerializeField] Rigidbody rBody;

    // Fields for Speed
    [SerializeField] float maxSpeed;
    [SerializeField, Tooltip("Speed below which the vehicle will come to a full stop.")] float minSpeed;

    // Fields for Acceleration/Deceleration
    [SerializeField, Range(0, 1), Tooltip("The % of the max speed the vehicle can achieve in 1 second.")] float accelPercent;
    [SerializeField, Range(0, 1), Tooltip("The % by which the velocity is reduced during 1 second of 0 acceleration.")] float decelPercent;

    // Fields for Acceleration & Deceleration can also be in units per second
    // This is how we originally did it in class.
    /*
    [SerializeField] float accelerationRate;
    [SerializeField] float decelerationRate;
    */

    // Fields for Turning
    [SerializeField, Tooltip("The # of degrees/second a moving vehicle can turn.")] float turnRate;

    // ***
    // All remaining fields are private!
    // No other classes should have access to this data about the vehicle.
    // They could potentially break the logic of movement in this script.
    // ***

    // Fields for Input
    private Vector3 movementDirection;

    // Fields for Movement Vectors
    private Vector3 velocity;
    private Vector3 acceleration;

    // Fields for rotation
    private Quaternion rotDelta;

    // Fields/properties that may help for rotation to terrain
    // Track the vehicle forward too
    private Vector3 VehicleForward { get; set; }
    [SerializeField] Transform vehicleTransform;

    // for demo, turn raycasting on/off
    [SerializeField] bool isRaycasting = false;
    [SerializeField] float maxTerrainHeight = 101f;
    [SerializeField] LayerMask groundLayerMask;

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
        Vector3 nextPosition = transform.position;
        Quaternion nextRotation = transform.rotation;

        #region Raycasting
        Vector3 origin = nextPosition;
        origin.y = maxTerrainHeight;
        RaycastHit terrainHit;
        // if raycasting AND I hit something
        if (isRaycasting && Physics.Raycast(origin, Vector3.down, out terrainHit, maxTerrainHeight, groundLayerMask))
        {
            nextPosition.y = terrainHit.point.y;
            Vector3 normal = terrainHit.normal;
        }

        #endregion

        #region Velocity Calcs
        // IF the gas is on
        if (movementDirection.z != 0f)
        {
            // Movement "formula":
            //transform.forward = movementDirection;

            // accel based on units per second
            //acceleration = accelerationRate * movementDirection.z * transform.forward;

            // accel based on % of max speed
            acceleration = transform.forward * movementDirection.z * accelPercent * maxSpeed;

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
            // decel based on units per second
            // velocity *= 1f - (decelerationRate * Time.fixedDeltaTime);

            // decelerate based on % of velocity kept per second
            velocity *= 1 - (decelPercent * Time.fixedDeltaTime);

            // If it gets really small, just zero it out
            if (velocity.sqrMagnitude < minSpeed * minSpeed)
            {
                velocity = Vector3.zero;
            }
        }
        #endregion

        #region Rotation Calcs
        // How much to turn by
        float turnAmount = movementDirection.x * turnRate * Time.fixedDeltaTime;
        rotDelta = Quaternion.Euler(0f, turnAmount, 0f);
        #endregion

        // Move the vehicle!
        nextPosition += velocity * Time.fixedDeltaTime;
        nextRotation *= rotDelta;
        rBody.Move(nextPosition, nextRotation);
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        Vector2 inputDir = callbackContext.ReadValue<Vector2>();
        movementDirection.z = inputDir.y;
        movementDirection.x = inputDir.x;
    }
}
