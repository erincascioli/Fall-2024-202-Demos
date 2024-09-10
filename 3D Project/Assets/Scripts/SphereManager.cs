
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single mode: Only one instance exists within the scene
/// Multiple mode: Many instances are spawned over time
/// </summary>
public enum GameMode
{
    Single,
    Multiple
}


/// <summary>
/// Instantiates a new instance of a prefab when none exists.
/// Alternately, instantiates multiple instances of a prefab in multiple mode.
/// This is attached to an empty GameObject in the scene. No rendered geometry is needed for this script.
/// </summary>
public class SphereManager : MonoBehaviour
{
    // --- Needed references for prefab instantiation ---
    public GameObject prefabRef;
    public GameObject prefabInstance;

    // --- Which mode am I in? ---
    public GameMode currentGameMode;

    // --- Data for spawning multiple instances over time ---
    public int frameCounter = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        // Nothing needed here.
        // Nothing occurs at the start of the game.
    }

    // Update is called once per frame
    // High FPS? This method is called a LOT!
    void Update()
    {
        if(currentGameMode == GameMode.Single)
        {
            // There is no sphere yet...
            if (prefabInstance == null)
            {
                // Make one single instance of the prefab!
                // This clones the prefab and places it in the hierarchy.
                // Returns a reference to the cloned game object.
                prefabInstance = Instantiate(
                    prefabRef,                                              // Which prefab?
                    new Vector3(UnityEngine.Random.Range(-8f, 8f), 0, 0),   // Where is it spawned?
                    Quaternion.identity);                                   // Rotation
            }
        }

        else if(currentGameMode == GameMode.Multiple)
        {
            // Increase frameCounter by 1 every frame
            frameCounter++;

            // After 300 frames...
            if (frameCounter > 300)
            {
                // Make a single instance of the green sphere at a random X location with no rotation.
                Instantiate(
                    prefabRef,                                              // Which prefab?
                    new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, 0),   // Where is it spawned?
                    Quaternion.identity);                                   // Rotation

                // Reset the frameCounter
                frameCounter = 0;
            }
        }
    }
}
