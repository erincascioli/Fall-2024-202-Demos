using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys a different GameObject when that GO collides with the
/// GO this script is on.
/// </summary>
public class DestroyObject : MonoBehaviour
{
    // Reference to a different game object in the script
    public GameObject collidingObjectRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This method runs when the collider on this game object is entered
    /// by a different game object.
    /// </summary>
    /// <param name="collision">Reference to the specific collision event</param>
    void OnTriggerEnter(Collider collision)
    {
        // --- Debugging ---
        // Debugging to determine that a collision occurred
        //   with information about the specific game object that collided
        //   with this one.
        Debug.Log("Collision happened! " + collision.gameObject);
        
        // --- Removal of the colliding thing ---
        // If you have a reference to a specific game object, can use this:
        //Destroy(collidingObjectRef);

        // If the reference isn't stored within this script, access the 
        //   game object that collided with this.
        Destroy(collision.gameObject);
    }
}
