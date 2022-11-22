using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement_GameStart : UIElement
{
    public override void Select(UICursor cursor)
    {
        FindObjectOfType<GameController>().ChangeScreen(nextScreen);
        FindObjectOfType<GameController>().EnableJoining();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
