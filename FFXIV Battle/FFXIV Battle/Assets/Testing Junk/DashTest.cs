using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashTest : MonoBehaviour
{
    PlayerInputManager test;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Move()
    {
        Debug.Log("move");
    }

    public void Dash1(InputAction.CallbackContext context)
    {
        if(context.performed)
        Debug.Log("dash 1");
    }
    public void Dash2()
    {
        Debug.Log("dash 2");
    }
}
