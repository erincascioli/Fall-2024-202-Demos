using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// https://docs.google.com/document/d/1iRkAa3IrwVgIjxIqMLruvyYj1dlIxbOFB5uQwFwfBtM/edit?tab=t.0
public abstract class Agent : PhysicsObject
{
    /// <summary>
    /// Given a target position, calculate the steering force needed to seek it at max speed
    /// </summary>
    protected Vector3 Seek(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - transform.position;

        // Opt 0 - Keep desired velocity effectively scaled by our distance to the target

        // Opt 1 - Make the desired velocity ALWAYS have a magnitude == maxSpeed
        // This is the "classic" seek behavior: "go as fast as I'm allowed to go!"
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Opt 2 - Clamp desired velocity to maxSpeed so that it scales if we're close,
        // but otherwise can't get above max
        // This is pseudo-arrival behavior: maxSpeed becomes the arrival radius.
        // desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);

        // Calculate & return seek steering force
        return desiredVelocity - Velocity;
    }

    /// <summary>
    /// Given a target GameObject, calculate the steering force needed to seek its position at max speed
    /// </summary>
    protected Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    /// <summary>
    /// Given a target position, calculate the steering force needed to flee from it at max speed
    /// </summary>
    protected Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVelocity = transform.position - targetPos;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        return desiredVelocity - Velocity;
    }

    /// <summary>
    /// Given a target GameObject, calculate the steering force needed to flee 
    /// from its position at max speed
    /// </summary>
    protected Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }

    /// <summary>
    /// Calculate the steering force needed to get back into bounds if we've left.
    /// (Uses the bounds defined on the base PhysicsObject)
    /// </summary>
    protected Vector3 StayInBoundsForce()
    {
        if (!Bounds.Contains(transform.position))
        {
            return Seek(Bounds.center);
        }
        return Vector3.zero;
    }
}
