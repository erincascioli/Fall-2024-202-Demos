using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SmartSeeker : Agent, ITrackedChild
{
    [SerializeField] ChildTracker targetTracker;
    [SerializeField, Range(0, 1)] float seekScalar = 1f;
    [SerializeField, Range(0, 1)] float boundsStrength = 1f;
    [SerializeField] SeekMode mode = SeekMode.Average;

    public enum SeekMode
    {
        Average,
        Closest,
        Furthest
    }

    // Calculate the steering force
    protected override Vector3 CalcSteering()
    {
        Vector3 targetPos = targetTracker.AveragePosition;
//        Vector3 targetPos = targetTracker.FindClosestChild(transform.position).transform.position;
        return Seek(targetPos) * seekScalar +
                StayInBoundsForce() * boundsStrength;
    }

    public void SetTracker(ChildTracker tracker)
    { 
        targetTracker = tracker;
    }

}
