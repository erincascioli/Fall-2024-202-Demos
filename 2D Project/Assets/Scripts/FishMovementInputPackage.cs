using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishMovementInputPackage : MonoBehaviour
{
    // Reference to the fish
    public GameObject fishGO;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    // Method must be public and requires a specific signature
    // (Because it's added to an EVENT!)
    public void OnSwim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UnityEngine.Debug.Log("swim!");
        }

        if (context.phase == InputActionPhase.Started)      // On first frame press/click
        {
            UnityEngine.Debug.Log("Swimming?");
            //fishGO.GetComponent<FishMovement>().SwimRight();

            fishGO.GetComponent<FishMovement>().Swim( some vector2 );
        }

        if (context.phase == InputActionPhase.Canceled)     // Canceled = Release
        {
            UnityEngine.Debug.Log("end swimming!");
        }
    }

}
