using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Initializes multiple instances of a prefab.
/// Saves references to each instance in a list of GameObjects.
/// This script is a component of the "Ball Spawner" empty GameObject in the scene.
/// </summary>
public class PrefabInstantiator : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // Fields for the class
    // ------------------------------------------------------------------------

    /// <summary>
    /// Prefab model on which to instantiate balls from in this script
    /// </summary>
    public GameObject ballPrefab;

    /// <summary>
    /// Holds 20 instances of the ball prefab
    /// </summary>
    [SerializeField]
    private List<GameObject> ballInstances = new List<GameObject>();


    // ------------------------------------------------------------------------
    // Built-in methods are automatically called by Unity
    // ------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate 20 balls at the start of the game
        SpawnBalls();
    }


    // Update is called once per frame
    void Update()
    {
        // Nothing to do here!
    }


    // ------------------------------------------------------------------------
    // Additional methods written by us
    // ------------------------------------------------------------------------

    /// <summary>
    /// Instantiates multiple instances of the ball prefab
    /// </summary>
    public void SpawnBalls()
    {
        // --- Instantiate 20 balls! ---
        for (int i = 0; i < 20; i++)
        {
            // --- Get a random location for each instance ---
            float xPosition = UnityEngine.Random.Range(-11f, 11f);
            float yPosition = UnityEngine.Random.Range(3f, 6f);

            // FYI: Random.Range overload:
            // Passing integers to the method results in a returned integer
            //int xPosition = UnityEngine.Random.Range(-11, 11);
            //int yPosition = UnityEngine.Random.Range(3, 6);

            // --- Instantiate (make a clone of) the ball prefab ---
            GameObject spawnedBall = Instantiate(
                ballPrefab,                                 // Which thing to clone?
                new Vector3(xPosition, yPosition, 0),       // Where is the clone positioned?
                Quaternion.identity);                       // Rotation of (0, 0, 0)

            // --- Set a new color for each instance of the ball ---
            // Gain access to the SpriteRenderer component of that instance
            //   using the GetComponent method.
            // Colors are on a scale of 0 to 1.
            spawnedBall.GetComponent<SpriteRenderer>().color =
                new Color(xPosition/10, yPosition/10, 0);

            ballInstances.Add(spawnedBall);
        }
    }
}
