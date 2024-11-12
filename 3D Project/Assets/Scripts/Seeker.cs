using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{
    [SerializeField] GameObject target;
    [SerializeField, Range(0, 1)] float seekStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override Vector3 CalcSteering()
    {
        return Seek(target) * seekStrength;
    }
}
