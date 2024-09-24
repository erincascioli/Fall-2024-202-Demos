using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permits any GameObject with a Rigidbody to jump in an upward direction
/// </summary>
public class Jumper : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // Fields for the class 
    // ------------------------------------------------------------------------
    // Whats needed for jumping?
    // A direction and a force to jump!

    /// <summary>
    /// Direction the object will jump in
    /// </summary>
    public Vector3 jumpDirection;

    /// <summary>
    /// Scalar for direction, how far the object jumps
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// Reference to a Rigidbody component on THIS game object
    /// </summary>
    [SerializeField]
    private Rigidbody2D rbody;

    /// <summary>
    /// How often the ball will jump, in seconds
    /// </summary>
    public int timeToJump;

    // Don't want to see this in the Inspector or modify it
    private float timer = 0f;


    // ------------------------------------------------------------------------
    // Auto-Called methods in Unity
    // ------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //rbody = GetComponent<Rigidbody2D>();
        //Jumping();
    }

    // Update is called once per frame
    void Update()
    {
        // Every 5 seconds the ball will jump

        // Add time elapsed to the timer variable
        // When it's passed timetoJump, the ball will jump
        //   and the timer will be "reset"
        timer += Time.deltaTime;

        if(timer > timeToJump)
        {
            Jumping();

            timer -= timeToJump;
        }
    }


    // ------------------------------------------------------------------------
    // Additional Methods for specified behavior
    // ------------------------------------------------------------------------

    /// <summary>
    /// Applies force to this GO to jump in an upward motion
    /// </summary>
    public void Jumping()
    {
        // Locally declared vector for jumping velocity
        Vector3 jumpVector = jumpDirection * jumpForce;

        // Get the RB component on this game object, then add a force in a specific direction
        // We'll shorten this with "shortcuts" next class
        //gameObject.GetComponent<Rigidbody2D>().AddForce(jumpVector);
        //GetComponent<Rigidbody2D>().AddForce(jumpVector);
        rbody.AddForce(jumpVector);
    }
}
