using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Raises the position of a GameObject along the Y axis.
/// Also raises a separate GameObject that is referenced within this script.
/// </summary>
public class FlyMe : MonoBehaviour
{
    // Fields of the script act like fields did previously
    // BUT if they are public they are editable in the Inspector

    // How fast the cube moves up in the world.
    // This value can be overridden in the Inspector.
    [SerializeField]
    private float speed = 0.005f;

    // A reference to another GameObject somewhere in the scene.
    public GameObject greenSphere;


    // Start is called before the first frame update
    void Start()
    {
        // --- Debugging with Debug.Log ---
        // How can you debug your script? Debug.Log() can be used for textual debugging.
        //Debug.Log("First frame only");
    }

    // Update is called once per frame
    void Update()
    {
        // --- Debugging with Debug.Log ---
        // How can you debug your script? Debug.Log() can be used for textual debugging.
        // This will show up in the Console widndow every frame!
        //Debug.Log("Every frame");

        // --- Raising a GameObject on the Y axis ---

        // I want to "fly" my cube up in the air BUT I can't do this:
        //transform.position.y += 0.05;  <-- ERROR!
        // Why?  The position is a struct and I cannot directly modify the Y component
        //   as it's being referenced from the Transform component.
        // What do we do instead?  Copy-Alter-Replace!

        // Handle the game object this script is on:
        Vector3 newPosition = transform.position;
        newPosition.y += speed;
        transform.position = newPosition;

        // --- Raising A DIFFERENT GameObject on the Y axis ---

        // First check if the other game object exists to reduce null reference errors.
        if (greenSphere != null)
        {
            // It does exist! 
            // Need access via a reference to the Transform component of the green sphere
            //   to modify it.  
            newPosition = greenSphere.transform.position;
            newPosition.y += speed;
            greenSphere.transform.position = newPosition;
        }
    }
}
