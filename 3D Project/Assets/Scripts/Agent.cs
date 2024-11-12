using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public abstract class Agent : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] Rigidbody rBody;
    public Vector3 velocity, acceleration; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // Setup
        Quaternion nextRotation = transform.rotation;
        Vector3 nextPosition = transform.position;

        // Start with acceleration based on the steering force
        acceleration = CalcSteering();

        // Calc velocity based on accel scaled by time
        velocity += acceleration * Time.fixedDeltaTime;

        // Clamp velocity to min/max speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Rotate to face the direction of travel
        nextRotation = Quaternion.LookRotation(velocity, Vector3.up);

        //  Use velocity to calc next position
        nextPosition += (velocity * Time.fixedDeltaTime);

        //  Move the agent
        rBody.Move(nextPosition, nextRotation);

        // Zero out acceleration (b/c it's only a field for debugging)
        acceleration = Vector3.zero;
    }

    protected abstract Vector3 CalcSteering();

    protected Vector3 Seek(Vector3 targetPosition) 
    {
        // Calculate desired velocity
        // Desired velocity --> a vector pointing from vehicle to its target
        Vector3 desiredVelocity = targetPosition - transform.position;

        // ** Let Agent FixedUpdate do this: Desired velocity must be scaled by maxSpeed

        // Calculate the resultant steering force required to change a current
        // velocity to the desired velocity
        // Return steering force
        return desiredVelocity - velocity;
    }

    protected Vector3 Seek(GameObject target)
    {
        // Call the other version of Seek 
        //   which returns the seeking steering force
        //  and then return that returned vector. 
        return Seek(target.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, velocity);
    }
}
