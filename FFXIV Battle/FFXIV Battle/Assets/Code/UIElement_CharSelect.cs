using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement_CharSelect : UIElement
{
    public GameObject character;
    // Start is called before the first frame update
    public override void Select(UICursor cursor)
    {
        PlayerController pc = cursor.GetComponentInParent<PlayerController>();
        GameObject playerChar = Instantiate(character, pc.gameObject.transform);
        pc.characterController = playerChar.GetComponent<CharacterController>();
        pc.controllingUI = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
