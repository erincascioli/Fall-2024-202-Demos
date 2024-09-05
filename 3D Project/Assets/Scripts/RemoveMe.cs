using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script removes a specific GameObject after time has passed. 
/// </summary>
public class RemoveMe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Nothing needs to happen here!
    }

    // Update is called once per frame
    void Update()
    {
        // ERROR:  
        // The "this" keyword refers to THIS SCRIPT. 
        // It does not refer to the GAME OBJECT this script is on.
        //Destroy(this, 3);

        // Need to reference a specific game object instance? 
        // The "gameObject" keyword (lowercase g) means the GameObject this script is on.
        Destroy(gameObject, 3);
    }
}
