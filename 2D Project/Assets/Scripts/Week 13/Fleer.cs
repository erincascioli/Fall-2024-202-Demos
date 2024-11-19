using UnityEngine;

public class Fleer : Agent
{
    // Fields for Seeker
    [SerializeField] GameObject target;
    [SerializeField, Range(0, 1)] float fleeScalar;
    [SerializeField, Range(0, 1)] float boundsStrength = 1f;

    // Calculate the steering force
    protected override Vector3 CalcSteering()
    {
        return Flee(target) * fleeScalar +
                StayInBoundsForce() * boundsStrength;
    }

}
