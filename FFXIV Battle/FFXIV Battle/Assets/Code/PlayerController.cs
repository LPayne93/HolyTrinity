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
    public PlayerCharacterController characterController;
    public bool controllingUI;
    public int playerNumber;

    private void Awake()
    {
        GameController.AddPlayer(this.gameObject);
        playerNumber = GameController.players.Count;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void DPad(InputAction.CallbackContext context)
    {   
        Vector2 direction = context.ReadValue<Vector2>();
        if (controllingUI && context.performed)
        {
            if (direction.y > .5) uicursor.Up();
            if (direction.y < -.5) uicursor.Down();
            if (direction.x > .5) uicursor.Right();
            if (direction.x < -.5) uicursor.Left();
        }
        else if (!controllingUI)
        {
            characterController.DPad(direction);
            if (direction.y > .5) characterController.Up();
            if (direction.y < -.5) characterController.Down();
            //if (direction.x > .5) characterController.Right();
            //if (direction.x < -.5) characterController.Left();
        }

    } 
    
    public void AButton(InputAction.CallbackContext context)
    {
        if (controllingUI && context.performed)
        {
            uicursor.Select();
        }
        else if (context.performed)
        {
            characterController.FastAttack();
        }
    }
    
    public void BButton(InputAction.CallbackContext context)
    {
        if(!controllingUI && context.performed)
        {
            characterController.StrongAttack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
