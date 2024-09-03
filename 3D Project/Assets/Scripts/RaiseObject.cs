using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseObject : MonoBehaviour
{
    // A public variable's value can be set in the Inspector
    public float speed;

    // Reference to a different GO in the scene allows this script
    //   access to the components on that GO.
    public GameObject sphere;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame, update this GameObject's transform
        // Remember that Vector3's are structs and cannot be directly modified 
        //   outside of its initialization.
        Vector3 positionCopy = transform.position;
        positionCopy.y += speed;
        transform.position = positionCopy;

        // ALSO raise a DIFFERENT game object in the world.
        // Requires a reference to a different GO. (See the fields of this class)
        positionCopy = sphere.transform.position;
        positionCopy.y += speed;
        sphere.transform.position = positionCopy;
    }
}
