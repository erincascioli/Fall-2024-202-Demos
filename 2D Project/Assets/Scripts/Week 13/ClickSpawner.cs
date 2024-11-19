using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    private ChildTracker childTracker = null;

    private void Start()
    {
        // Try to find a child tracker on this spawner and save a reference to it
        childTracker = GetComponent<ChildTracker>();
    }

    /// <summary>
    /// Spawn a new instance of the prefab as a child of the gameObject this
    /// component is attached to
    /// </summary>
    public void Spawn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Find the mouse position in the world
            Vector3 loc = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            loc.z = transform.position.z;

            // Create a new object at that location with this spawner as the parent
            GameObject newObject = Instantiate(prefab, loc, Quaternion.identity, transform);

            // If we have a child tracker & if this new child is trackable, tell it who it's parent it
            if (childTracker != null)
            {
                ITrackedChild trackedChild = newObject.GetComponent<ITrackedChild>();
                if (trackedChild != null)
                {
                    trackedChild.SetTracker(childTracker);
                }
            }
        }
    }

    public void DestroyAll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            for(int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
