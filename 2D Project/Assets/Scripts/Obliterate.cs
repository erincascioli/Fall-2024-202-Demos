using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys other GameObjects that collide with this GameObject.
/// This script is a component of the "Fire Floor" rectangle in the scene.
/// </summary>
public class Obliterate : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // Fields for the class
    // ------------------------------------------------------------------------

    // None here!


    // ------------------------------------------------------------------------
    // Built-in methods are automatically called by Unity
    // ------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        // Nothing happens!
    }


    // Update is called once per frame
    void Update()
    {
        // Nothing happens!        
    }

    // ------------------------------------------------------------------------
    // Collision handling methods are automatically called by Unity
    // ------------------------------------------------------------------------

    /// <summary>
    /// When a collision occurs, destroy the object that collided with this one.
    /// </summary>
    /// <param name="collision">Reference to the GameObject that collided.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // --- Debugging ---
        // Debugging to determine that a collision occurred with information
        //   about the specific game object that collided with the one this 
        //   script is on.
        UnityEngine.Debug.Log("Collision happened! " + collision.gameObject.name);

        // --- Removal of the colliding thing with no prior reference to it ---
        // Destroy the thing that collided with this thing.
        // More technically, destroy the GameObject that is saved in the reference
        //   "collision" (found as a parameter in this method)
        Destroy(collision.gameObject);

        // --- Removal of the colliding thing with a reference to it ---
        // If you have a reference to a specific game object, can use this
        //   instead of destroying the GameObject that collided with this GameObject.
        // This is helpful if there's only one thing - like a single Ball - 
        //   that needs to be removed from the game.  
        //Destroy(collidingObjectReference);
    }
}
