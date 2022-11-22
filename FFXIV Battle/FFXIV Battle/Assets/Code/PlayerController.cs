using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//The main reciever of Inputs from the controller. It is the abstraction of the player themself, and as such
//is the parent on the prefab that is created when a new character joins. All inputs recieved here call functions
//on either CharacterController or UICursor.
public class PlayerController : MonoBehaviour
{
    public UICursor uicursor;
    public CharacterController characterController;
    public bool controllingUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DPad(InputAction.CallbackContext context)
    {
   
        Vector2 direction = context.ReadValue<Vector2>();
        if (controllingUI && context.performed)
        {
            Debug.Log(context.ReadValue<Vector2>());
            if (direction.y > .5) uicursor.Up();
            if (direction.y < -.5) uicursor.Down();
            if (direction.x > .5) uicursor.Right();
            if (direction.x < -.5) uicursor.Left();
        }
        else
        {
            if (direction.y > .5) characterController.Up();
            if (direction.y < -.5) characterController.Down();
            if (direction.x > .5) characterController.Right();
            if (direction.x < -.5) characterController.Left();
        }

    } 
    
    public void AButton(InputAction.CallbackContext context)
    {
        if (controllingUI && context.performed)
        {
            Debug.Log("Clicked A");
            uicursor.Select();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
