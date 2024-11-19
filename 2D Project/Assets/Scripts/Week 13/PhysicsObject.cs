using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicsObject : MonoBehaviour
{
    // Reference to RigidBody on this GameObject
    [SerializeField] protected Rigidbody2D rBody;

    // Fields for Speed & Force limits
    [SerializeField] protected float maxSpeed = 5f;

    // Need to track velocity across frames. Also make it publicly readable
    public Vector3 Velocity { get; protected set; }
    [SerializeField] protected bool randomInitialHeading = false;

    // Boundaries
    [SerializeField] Bounds bounds;
    [SerializeField] bool enforceBounds = false;

    // Debug help
    [SerializeField] protected Color gizmosColor = Color.cyan;

    public Bounds Bounds
    {
        get { return bounds; }
    }

    // If randomized, pick a starting heading with a magnitude of max speed
    private void Start()
    {
        if (randomInitialHeading)
        {
            Velocity = Random.insideUnitCircle.normalized * maxSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Setup
        Quaternion nextRotation = transform.rotation;
        Vector3 nextPosition = transform.position;

        // Start with acceleration based on the steering force
        Vector3 acceleration = CalcSteering();

        // Calc velocity based on accel scaled by time
        Velocity += acceleration * Time.fixedDeltaTime;

        // Clamp velocity to min/max speed
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);

        // Rotate to face the direction of travel (around the Z axis)
        nextRotation = Quaternion.LookRotation(Velocity, Vector3.back);

        //  Use velocity to calc next position
        nextPosition += (Velocity * Time.fixedDeltaTime);
        if (enforceBounds)
        {
            nextPosition = EnforceBounds(nextPosition);
        }


        //  Move the Vehicle
        rBody.MovePosition(nextPosition);
        rBody.MoveRotation(nextRotation);
    }

    // Given the current position & velocity, calculate the position we'll be at after time t
    public Vector3 FuturePosition(float t)
    {
        return transform.position + (Velocity * t);
    }

    // If the position given is out of bounds, return thr closest position within bounds (with bounds scaled
    // by the padding value)
    private Vector3 EnforceBounds(Vector3 position)
    {
        if (!bounds.Contains(position))
        {
            return bounds.ClosestPoint(position);
        }
        return position;
    }

    protected abstract Vector3 CalcSteering();

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        try
        {
            Gizmos.DrawRay(transform.position, Velocity);
        }
        catch { }
        Gizmos.DrawWireCube(Bounds.center, Bounds.size);
    }
}
