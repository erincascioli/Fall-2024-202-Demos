using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Sets the spawn mode to utilize different inputs.
/// </summary>
public enum SpawnMode
{
    Every_Frame,
    K_Key,
    Left_Mouse
}

/// <summary>
/// Sets the despawn mode to utilize different inputs.
/// </summary>
public enum DespawnMode
{
    R_Key,
    Right_Mouse
}



/// <summary>
/// Script that handles spawning of any prefab.
/// Contains methods to randomly change information about the spawned instance,
/// including its color, scale, and rotation.
/// Spawning happens upon chosen input using either the Input Manager
/// or the Input System Package.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    /// <summary>
    /// List of possible sprites to be spawned.
    /// Indices in the list:
    /// 0 - Circle
    /// 1 - Square
    /// 2 - Triangle
    /// </summary>
    public List<GameObject> sprites;

    /// <summary>
    /// References to each instantiated instance of a prefab.
    /// </summary>
    public List<GameObject> spawnedSprites;

    /// <summary>
    /// How should the spawning occur?
    /// </summary>
    public SpawnMode currentSpawnMode;

    /// <summary>
    /// How should despawning occur?
    /// </summary>
    public DespawnMode currentDespawnMode;


    // ------------------------------------------------------------------------
    // Auto-Called Methods
    // ------------------------------------------------------------------------

    void Start()
    {

    }

    void Update()
    {
        // ** Input Manager: **
        // Always check for user inputs!
        CheckForSpawn();
        CheckForDespawn();

        // ** Input System Package: **
        // No need to call a specific method if using the Input System Package,
        // as events are triggered automatically via the Unity engine.
    }


    // ------------------------------------------------------------------------
    // Instantiation Methods
    // ------------------------------------------------------------------------

    // ** Write methods here that spawn instances of a prefab. **
    // ** Or methods that handle despawning. **


    /// <summary>
    /// Chooses one of the prefabs for instantiation somewhere within the scene.
    /// Saves the reference in the list data structure.
    /// </summary>
    public void Spawn()
    {
        // Which prefab is going to be spawned?
        GameObject randomlyChosenPrefab = ChooseRandomPrefab();

        // Generate a random position within the screen boundaries
        float xPosition = UnityEngine.Random.Range(-10f, 10f);
        float yPosition = UnityEngine.Random.Range(-4f, 4f);

        // Instantiate the prefab and save a reference to it.
        GameObject shape =
            Instantiate(
                randomlyChosenPrefab,
                new Vector3(xPosition, yPosition, 0),
                Quaternion.identity);
        spawnedSprites.Add(shape);
    }


    /// <summary>
    /// Non-uniform random chance of spawning one of the three prefabs.
    /// </summary>
    /// <returns>Reference to the chosen prefab</returns>
    public GameObject ChooseRandomPrefab()
    {
        // Get a random number between 0 and 1.
        // Choose a non-uniform random value to determine which shape will appear.
        float chance = UnityEngine.Random.Range(0f, 1f);
        int index = 0;

        switch (chance)
        {
            case < 0.6f:
                index = 0;      // Circle
                break;
            case < 0.8f:
                index = 1;      // Square
                break;
            default:
                index = 2;      // Triangle
                break;
        }

        return sprites[index];
    }

    /// <summary>
    /// Removes all spawned instances from the scene, and clears list.
    /// </summary>
    public void Despawn()
    {
        // Destroy all GO's that were spawned in the scene
        for (int i = spawnedSprites.Count - 1; i >= 0; i--)
        {
            Destroy(spawnedSprites[i]);
        }

        // Then clear the list.
        spawnedSprites.Clear();
    }


    // ------------------------------------------------------------------------
    // Randomization Methods
    // ------------------------------------------------------------------------

    // ** Place all randomization methods here. **
    // ** Could write code to choose a random color, or a random
    //    rotation, or a random scale, etc. **


    // ------------------------------------------------------------------------
    // Input Methods
    // ------------------------------------------------------------------------

    // ** Write methods here that deal with user input. **
    // ** Can use either the old Input Manager system, or the
    //    new Input System Package. **


    // ****************************************************
    // Input using the old Input Manager system
    // ****************************************************

    /// <summary>
    /// Handles the inputs that cause a game object to be spawned.
    /// </summary>
    public void CheckForSpawn()
    {

        // Continual spawning of shapes
        if (currentSpawnMode == SpawnMode.Every_Frame)
        {
            Spawn();
        }
        // Press the K key? Spawn a shape!
        else if (currentSpawnMode == SpawnMode.K_Key)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Spawn();
            }
        }
        // Press the left mouse button? Spawn a shape!
        else if (currentSpawnMode == SpawnMode.Left_Mouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Spawn();
            }
        }
    }


    /// <summary>
    /// Handles the inputs that cause all game objects to be removed.
    /// </summary>
    public void CheckForDespawn()
    {
        // Press the R key? Remove all spawned instances!
        if (currentDespawnMode == DespawnMode.R_Key)
        {
            // Press the R key? Remove all spawned instances!
            if (Input.GetKeyDown(KeyCode.R))
            {
                Despawn();
            }
        }
        // Release the right mouse button? Remove 'em all!
        else if (currentDespawnMode == DespawnMode.Right_Mouse)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Despawn();
            }
        }
    }


    // ****************************************************
    // Input using the new Input System Package
    // ****************************************************
    // NOTE: To avoid double-spawning, the Input System is set with these spawn actions:
    // Spawn: left arrow key
    // Despawn: right arrow key


    /// <summary>
    /// Handles the inputs that cause a game object to be spawned.
    /// </summary>
    /// <param name="context"></param>
    public void OnSpawn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // This will happen on whatever action has been set in the
            //   InputAction component.
            Spawn();
        }
    }

    /// <summary>
    /// Handles the inputs that cause all game objects to be removed.
    /// </summary>
    /// <param name="context"></param>
    public void OnDespawn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // This will happen on whatever action has been set in the
            //   InputAction component.
            Despawn();
        }
    }
}
