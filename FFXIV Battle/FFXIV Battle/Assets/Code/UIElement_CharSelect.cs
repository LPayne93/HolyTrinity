using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIElement_CharSelect : UIElement
{
    public GameObject character;
    // Start is called before the first frame update
    public override void Select(UICursor cursor)
    {
        PlayerController pc = cursor.GetComponentInParent<PlayerController>();
        GameController.Instance.GiveCharacterToPlayer(pc, character);

        SoundManager.Instance.Play(1);
        GameController.StartGame(nextScreen);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
