using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class destroy : MonoBehaviour
{
    public InputActionAsset actionMap;
    // Start is called before the first frame update
    private void OnEnable()
    {
        actionMap.Disable();   
    }

    public void OnDisable()
    {
        actionMap.Enable();   
    }

    public void ReenableActionMap()
    {
        actionMap.Enable();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
