using UnityEngine;

public class Seeker : Agent
{
    // Fields for Seeker
    [SerializeField] GameObject target;
    [SerializeField, Range(0,1)] float seekScalar = 1f;
    [SerializeField, Range(0, 1)] float boundsStrength = 1f;

    // Calculate the steering force
    protected override Vector3 CalcSteering()
    {
        return Seek(target) * seekScalar + 
                StayInBoundsForce() * boundsStrength;
    }

}
