using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    /// <summary>
    /// Object that this script moves with WASD or arrow keys
    /// </summary>
    public GameObject fish;

    /// <summary>
    /// Unit-based movement per second for the fish
    /// </summary>
    public float speed;

    // Used for rotation
    public Quaternion fishRotation = Quaternion.identity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Start by moving fish independently on X or Y axis using per-frame movement.
        //MoveFish();
    }


    /// <summary>
    /// Use Unity's Input Manager system to move the fish in 4 cardinal directions
    ///    with WASD.
    /// </summary>
    public void MoveFish(Vector2 inputDirection)
    {
        // Grab the fish's current position as a locally-declared struct
        // such that it is modifiable.
        Vector3 fishPosition = fish.transform.position;

        // Local vector that represents direction
        Vector3 fishDirection = Vector3.zero;

        // Get direction from the Value of the Vector2 based on what information is stored
        //   within the ContextCallback Input Action


        // Move the jellyfish!
        // When the A key is pressed, move fish to the left
        if (Input.GetKey(KeyCode.A))				            // Left
        {
            // Prove to ourselves that the key presses are working!
            UnityEngine.Debug.Log("Pressing A");

            // Move the object a tiny unit left
            fishDirection = new Vector3(-1, 0, 0);
            fishRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKey(KeyCode.D))                       // Right
        {
            fishDirection = new Vector3(1, 0, 0);
            fishRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.W))                       // Up
        {
            fishDirection = new Vector3(0, 1, 0);
            fishRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.S))                       // Down
        {
            fishDirection = new Vector3(0, -1, 0);
            fishRotation = Quaternion.Euler(0, 0, 270);
        }

        // Local velocity vector derived from direction * speed,
        // accounting for time instead of per-frame movement
        Vector3 velocity = fishDirection * speed;
        velocity *= Time.deltaTime;
        fishPosition += velocity;

        // Transport the fish to that position
        fish.transform.position = fishPosition;

        // Rotate to face the direction it's moving
        fish.transform.rotation = fishRotation;
    }

    public void SwimRight()
    {
        // Grab the fish's current position as a locally-declared struct
        // such that it is modifiable.
        Vector3 fishPosition = fish.transform.position;

        // Local vector that represents direction
        Vector3 fishDirection = Vector3.zero;

        // Move the object a tiny unit right
        fishDirection = new Vector3(1, 0, 0);
        fishRotation = Quaternion.Euler(0, 0, 180);

        // Local velocity vector derived from direction * speed,
        // accounting for time instead of per-frame movement
        Vector3 velocity = fishDirection * speed;
        velocity *= Time.deltaTime;
        fishPosition += velocity;

        // Transport the fish to that position
        fish.transform.position = fishPosition;

        // Rotate to face the direction it's moving
        fish.transform.rotation = fishRotation;
    }
}
